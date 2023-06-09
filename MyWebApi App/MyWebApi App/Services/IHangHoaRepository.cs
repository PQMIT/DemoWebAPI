using MyWebApi_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi_App.Services
{
    public interface IHangHoaRepository
    {
        List<HangHoaModel> GetAll(string search, double? from, double? to, string sortBy, int page =1);
        HangHoaModel Add(HangHoaModel hanghoa);
        void Delete(Guid id);
        void Update(HangHoaModel hanghoa);
    }
}
