using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRewards.Entities
{
    public enum ApplicationTypes
    {
        JavaScript = 0,
        NativeConfidential = 1
    };
    
    public enum ResultTypes
    {
        Success=0,
        Failed=1,
        Concurrency=2
    };
}