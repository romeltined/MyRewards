using MyRewards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyRewards.Entities;
using Newtonsoft.Json;
using System.Data.Entity;

namespace MyRewards.Services
{
    public class VoucherOderService
    { 
        private ApplicationDbContext db;
        private NopCommerceContext ndb;

        public VoucherOderService()
        {
            db = new ApplicationDbContext();
            ndb = new NopCommerceContext();
        }

        public void VoucherOrderFlow()
        {
            //AddVoucherOrders();
            //UpdateUnpaidOrders();
            //AddVoucherOrderItems();
            CreateVouchers();
        }

        public void AddVoucherOrders()
        {
            int maxOrderId = 0;
            if(db.VoucherOrderLogs.Count()>0)
            {
                maxOrderId= db.VoucherOrderLogs.Max(v => v.Order_Id);
            }

            var orders = ndb.Database.SqlQuery<VoucherOrderLog>("SELECT Id, Id AS Order_Id, CustomerId AS Customer_Id, '' AS Content, 0 AS Status_Id, PaidDateUtc , GETDATE() AS CreatedOn, GETDATE() AS UpdatedOn FROM [Order] WHERE Id >" + maxOrderId).ToList();
            if(orders.Count()>0)
            {
                foreach(var order in orders)
                {
                    db.VoucherOrderLogs.Add(order);
                    db.SaveChanges();
                }
            }
        }

        public void UpdateUnpaidOrders()
        {
            var unpaid = db.VoucherOrderLogs.Where(v => v.PaidDateUtc == null && v.Status_Id==0).ToList();
            if(unpaid.Count()>0)
            {
                foreach(var p in unpaid)
                {
                    var w = ndb.Database.SqlQuery<VoucherOrderLog>("SELECT Id, Id AS Order_Id, CustomerId AS Customer_Id, '' AS Content, 0 AS Status_Id, PaidDateUtc , GETDATE() AS CreatedOn, GETDATE() AS UpdatedOn FROM [Order] WHERE Id = " + p.Order_Id).First();
                    if(w.PaidDateUtc!=null)
                    {
                        p.PaidDateUtc = w.PaidDateUtc;
                        db.Entry(p).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
        }

        public void AddVoucherOrderItems()
        {
            var paidOrders = db.VoucherOrderLogs.Where(v => v.PaidDateUtc != null && v.Status_Id == 0).ToList();
            if (paidOrders.Count() > 0)
            {
                foreach (var order in paidOrders)
                {
                    string content = "";
                    List<VoucherOderItem> list = new List<VoucherOderItem>();
                    var orderItems = ndb.Database.SqlQuery<VoucherOderItem>("SELECT Id AS OrderItem_Id, OrderId AS Order_Id, ProductId AS Product_Id, Quantity FROM [OrderItem] WHERE OrderId=" + order.Order_Id).ToList();
                    foreach (var item in orderItems)
                    {
                        list.Add(item);
                    }
                    content = JsonConvert.SerializeObject(list);
                    order.Content = content;
                    order.Status_Id = 1;
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

        }

        public void CreateVouchers()
        {
            var orders = db.VoucherOrderLogs.Where(v => v.Status_Id == 1).ToList();
            if(orders.Count()>0)
            {
                foreach(var order in orders)
                {
                    List<VoucherOderItem> list = JsonConvert.DeserializeObject<List<VoucherOderItem>>(order.Content);
                    foreach(var item in list)
                    {
                        VoucherType voucherType = new VoucherType();
                        voucherType = db.VoucherTypes.Where(v=>v.Product_Id==item.Product_Id).FirstOrDefault();
                        if(voucherType!=null)
                        { 
                            for (int i = 1; i <= item.Quantity; i++)
                            {                 
                                Guid guid = Guid.NewGuid();
                                var voucher = new Voucher
                                {
                                    VoucherType = voucherType,
                                    Guid = guid.ToString(),
                                    Sender_Id = 0,
                                    Receiver_Id = order.Customer_Id,
                                    ActionType_Id = 0,
                                    SpendFlag = false,
                                    Order_Id = order.Order_Id,
                                    VoucherOrder_Id = order.Id,
                                    CreatedOn = DateTime.UtcNow,
                                    UpdatedOn = DateTime.UtcNow
                                };
                                db.Vouchers.Add(voucher);
                                //db.SaveChanges();
                            }
                        }
                    }
                    order.Status_Id = 2;
                    db.Entry(order).State = EntityState.Modified;

                    db.SaveChanges();

                };

            }

        }
    }
}