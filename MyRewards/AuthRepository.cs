using System;
using MyRewards.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using MyRewards.Services;
using MyRewards.Entities;
using System.Text;
using System.Data.Entity;

namespace MyRewards
{
    public class AuthRepository :   IDisposable
    {
        private ApplicationDbContext _ctx;
        private SmartStoreContext _sctx;
        private NopCommerceContext _nctx;

        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public AuthRepository()
        {
            _ctx = new ApplicationDbContext();
            _sctx = new SmartStoreContext();
            _nctx = new NopCommerceContext();
            _userManager = new ApplicationUserManager(new CustomUserStore(_ctx));
            _roleManager = new ApplicationRoleManager(new CustomRoleStore(_ctx));
        }


        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public async Task<ApplicationRole> FindRole(int Id)
        {
            ApplicationRole role = await (_roleManager.FindByIdAsync(Id)) as ApplicationRole;
            return role;
        }

        public Client FindClient(string clientId)
        {
            var client = _ctx.Clients.Find(clientId);

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

           var existingToken = _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            _ctx.RefreshTokens.Add(token);

            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                _ctx.RefreshTokens.Remove(refreshToken);
                return await _ctx.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _ctx.RefreshTokens.Remove(refreshToken);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _ctx.RefreshTokens.ToList();
        }

        public SmartStoreCustomer FindSmartStoreUser(string userName, string password)
        {
            try
            {
                SmartStoreCustomer user = _sctx.Database.SqlQuery<SmartStoreCustomer>("SELECT Id AS Customer_Id, Username, Email, Password, PasswordSalt FROM Customer WHERE Username = '" + userName + "'").First();
                var pwd = CryptoService.CreatePasswordHash(password, user.PasswordSalt, "SHA1");
                if (pwd == user.Password)
                {
                    user.Password = "";
                    user.PasswordSalt = "";
                    return user;
                }
            }
            catch
            {
            }
            return null;
        }

        public NopCommerceUser FindNopCommerceUser(string userName, string password)
        {
            try
            {
                NopCommerceUser user = _nctx.Database.SqlQuery<NopCommerceUser>("SELECT CU.Id AS Customer_Id, Username, Email, Password, PasswordSalt FROM [Customer] CU INNER JOIN [CustomerPassword] CP ON CU.Id=CP.CustomerId WHERE CU.Username = '" + userName + "'").First();
                var pwd = CryptoService.CreatePasswordHash(password, user.PasswordSalt, "SHA1");
                if (pwd == user.Password)
                {
                    user.Password = "";
                    user.PasswordSalt = "";

                    List<string> roles = _nctx.Database.SqlQuery<string>("SELECT CR.Name AS RoleName FROM [Customer_CustomerRole_Mapping] CU INNER JOIN [CustomerRole] CR ON CU.CustomerRole_Id=CR.Id WHERE CU.Customer_Id=" + user.Customer_Id).ToList();
                    user.Roles = roles; 

                    return user;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public string FindNopCommerceUserRoles(string userName)
        {
            string result = String.Empty;
            try
            {
                List<string> roles = _nctx.Database.SqlQuery<string>("SELECT CR.Name AS RoleName FROM [Customer] CS INNER JOIN [Customer_CustomerRole_Mapping] CU ON CS.Id=CU.Customer_Id INNER JOIN [CustomerRole] CR ON CU.CustomerRole_Id=CR.Id WHERE CS.Username='" + userName +"'").ToList();
                foreach (string role in roles)
                {
                    result += role + ";";
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }


        public string SessionGuidManager(string userName)
        {
            Guid guid = Guid.NewGuid();
            var sessionGuid = _ctx.SessionGuids.Where(s => s.UserName == userName).FirstOrDefault();
            if(sessionGuid == null)
            {
                sessionGuid = new SessionGuid { UserName = userName, Guid = guid.ToString() };
                _ctx.SessionGuids.Add(sessionGuid);
                _ctx.SaveChanges();
            }
            else
            {
                sessionGuid.Guid = guid.ToString();
                _ctx.Entry(sessionGuid).State = EntityState.Modified;
                _ctx.SaveChanges();
            }

            return sessionGuid.Guid;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}