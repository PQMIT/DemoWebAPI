using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi_App.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public String Message { get; set; }
        public Object Data { get; set; }
    }
}
