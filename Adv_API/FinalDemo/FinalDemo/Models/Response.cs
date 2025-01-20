using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalDemo.Models
{
    public class Response
    {
        // is error field bydefault false 
        public bool IsError { get; set; } = false;
        // message field 
        public string Message { get; set; }

        // data and this type is dynamic
        public dynamic Data { get; set; }
    }
}