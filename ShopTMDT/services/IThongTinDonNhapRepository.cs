using Microsoft.AspNetCore.Mvc;
using ShopTMDT.Data;
using ShopTMDT.ViewModel;


namespace ShopTMDT.services
{
    public interface IThongTinDonNhapRepository
    {
        JsonResult AddThongTinDonNhap(ThongTinDonNhapVM thongTinDonNhapVM);
        JsonResult DeleteThongTinDonNhap(int id);
        JsonResult EditThongTinDonNhap(int id, ThongTinDonNhapVM thongTinDonNhapVM);
        List<ThongTinDonNhapMD> GetAll();   
    }
    public class ThongTinDonNhapRepository : IThongTinDonNhapRepository
    {
        private readonly StmdtContext _context;

        public ThongTinDonNhapRepository(StmdtContext context)
        {
            _context = context;
        }

        public JsonResult AddThongTinDonNhap(ThongTinDonNhapVM thongTinDonNhapVM)
        {
            try
            {
                var thong = new ThongTinDonNhap
                {
                    IdNhapHangHoa = thongTinDonNhapVM.IdNhapHangHoa,
                    IdHangHoa = thongTinDonNhapVM.IdHangHoa,
                    SoLuong = thongTinDonNhapVM.SoLuong,
                    Gia = thongTinDonNhapVM.Gia,
                    TongGia = thongTinDonNhapVM.TongGia
                };
                _context.ThongTinDonNhaps.Add(thong);
                _context.SaveChanges();
                return new JsonResult("Thông tin đơn nhập đã thêm thành công")
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch
            {
                return new JsonResult("Thông tin đơn nhập đã thêm thất bại")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };

            }
        }

        public JsonResult DeleteThongTinDonNhap(int id)
        {
            try
            {
                var thong = _context.ThongTinDonNhaps.SingleOrDefault(o => o.IdThongTinDonNhap == id);
                if (thong == null)
                {
                    return new JsonResult("Thông tin đơn nhập không tồn tại")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                else
                {
                    _context.ThongTinDonNhaps.Remove(thong);
                    _context.SaveChanges();
                    return new JsonResult("Thông tin đơn nhập đã xoá thành công")
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                }
            }
            catch
            {
                return new JsonResult("Thông tin đơn nhập đã xoá thất bại")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }

        public JsonResult EditThongTinDonNhap(int id, ThongTinDonNhapVM thongTinDonNhapVM)
        {
            try
            {
                var thong = _context.ThongTinDonNhaps.SingleOrDefault(o => o.IdThongTinDonNhap == id);
                if (thong == null)
                {
                    return new JsonResult("Thông tin đơn nhập không tồn tại")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                else
                {
                    thong.IdNhapHangHoa = thongTinDonNhapVM.IdNhapHangHoa;
                    thong.IdHangHoa = thongTinDonNhapVM.IdHangHoa;
                    thong.SoLuong = thongTinDonNhapVM.SoLuong;
                    thong.Gia = thongTinDonNhapVM.Gia;
                    thong.TongGia = thongTinDonNhapVM.TongGia;
                    _context.SaveChanges();
                    return new JsonResult("Thông tin đơn nhập đã sửa thành công")
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                }
            }
            catch
            {
                return new JsonResult("Thông tin đơn nhập đã sửa thất bại")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }

        public List<ThongTinDonNhapMD> GetAll()
        {
            var thong = _context.ThongTinDonNhaps.Select(o => new ThongTinDonNhapMD
            {
                IdThongTinDonNhap = o.IdThongTinDonNhap,
                IdNhapHangHoa = o.IdNhapHangHoa,
                IdHangHoa = o.IdHangHoa,
                SoLuong = o.SoLuong,
                Gia = o.Gia,
                TongGia = o.TongGia
            }).ToList();
            return thong;
        }
    }
}
