using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi_App.Models
{
    public class HangHoaVM
    {
        public string TenHangHoa { get; set; }
        public Double DonGia { get; set; }
    }
    
    public class HangHoa : HangHoaVM
    {
        public Guid MaHangHoa { get; set; }
    }
}
