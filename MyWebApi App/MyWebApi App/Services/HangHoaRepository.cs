using MyWebApi_App.Data;
using MyWebApi_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HangHoa = MyWebApi_App.Data.HangHoa;

namespace MyWebApi_App.Services
{
    public class HangHoaRepository : IHangHoaRepository
    {
        private readonly MyDbContext _context;
        public static int PAGE_SIZE { get; set; } = 100;
        public HangHoaRepository (MyDbContext context)
        {
            _context = context;
        }
        public List<HangHoaModel> GetAll(string search, double? from, double? to, string sortBy, int page = 1)
        {
            var allProducts = _context.HangHoas.AsQueryable();
            #region Filtering
            if (!String.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(hh => hh.TenHh.Contains(search));
            }
            if (from.HasValue)
            {
                allProducts = allProducts.Where(hh => hh.DonGia >= from);
            }
            if (to.HasValue)
            {
                allProducts = allProducts.Where(hh => hh.DonGia <= to);
            }
            #endregion


            #region Sorting
            //default sort by name (TenHh)

            allProducts = allProducts.OrderBy(hh => hh.TenHh);
            if (!String.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "tenhh_desc": allProducts = allProducts.OrderByDescending(hh => hh.TenHh); break;
                    case "gia_asc": allProducts = allProducts.OrderBy(hh => hh.DonGia); break;
                    case "gia_desc": allProducts = allProducts.OrderByDescending(hh => hh.DonGia); break;
                }
            }

            #endregion


            #region Paging
            allProducts = allProducts.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            #endregion
            var result = allProducts.Select(hh => new HangHoaModel
            {
                MaHangHoa = hh.MaHh,
                TenHangHoa = hh.TenHh,
                DonGia = hh.DonGia,
                TenLoai = hh.Loai.TenLoai,
                MoTa = hh.MoTa,
                GiamGia = hh.GiamGia
            });
            return result.ToList();
        }

        public HangHoaModel Add(HangHoaModel hanghoa)
        {
            var _hanghoa = new HangHoa
            {
                MaHh = hanghoa.MaHangHoa,
                TenHh = hanghoa.TenHangHoa,
                DonGia = hanghoa.DonGia,
                MoTa = hanghoa.MoTa,
                GiamGia = hanghoa.GiamGia
            };
            _context.Add(_hanghoa);
            _context.SaveChanges();
            return new HangHoaModel
            {
                MaHangHoa = _hanghoa.MaHh,
                TenHangHoa = _hanghoa.TenHh,
                DonGia = _hanghoa.DonGia,
                MoTa = _hanghoa.MoTa,
                GiamGia = _hanghoa.GiamGia
            };

        }
        
        public void Delete(Guid id)
        {
            var hanghoa = _context.HangHoas.SingleOrDefault(ha => ha.MaHh == id);
            if (hanghoa != null)
            {
                _context.Remove(hanghoa);
                _context.SaveChanges();
            }
        }

        public void Update(HangHoaModel hanghoa)
        {
            var _hanghoa = _context.HangHoas.SingleOrDefault(ha => ha.MaHh == hanghoa.MaHangHoa);
            if (_hanghoa != null)
            {
                _hanghoa.MaHh = hanghoa.MaHangHoa;
                _hanghoa.TenHh = hanghoa.TenHangHoa;
                _hanghoa.DonGia = hanghoa.DonGia;
                _hanghoa.GiamGia = hanghoa.GiamGia;
                _hanghoa.MoTa = hanghoa.MoTa;
                _context.SaveChanges();
            }
        }
    }
}
