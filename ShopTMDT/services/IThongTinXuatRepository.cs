using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ShopTMDT.Data;
using ShopTMDT.ViewModel;


namespace SpQuanAo.services
{
    public interface IThongTinXuatRepository
    {
        public Task<List<ThongTinXuatMD>> GetByIdHoaDon(string id);
        public Task<JsonResult> Create(ThongTinXuatResponse thongtinxuat);

    }
    public class ThongTinXuatRepository : IThongTinXuatRepository
    {
        private readonly StmdtContext _dbcontext;
        public ThongTinXuatRepository(StmdtContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<JsonResult> Create(ThongTinXuatResponse thongtinxuat)
        {
            if(thongtinxuat == null)
            {
                return new JsonResult("du lieu ko hop le")
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            var ThongTinHd = new ThongTinXuat
            {
                Gia = thongtinxuat.Gia,
                SoLuong = thongtinxuat.SoLuong,
                TongGia = thongtinxuat.TongGia,
                IdHangHoa = thongtinxuat.IdHangHoa,
                IdKhuyenMai = thongtinxuat.IdKhuyenMai,
                IdXuatHangHoa = thongtinxuat.IdXuatHangHoa
            };
            await _dbcontext.AddAsync(ThongTinHd);
            await _dbcontext.SaveChangesAsync();
            return new JsonResult("đã thêm thành công")
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        public async Task<List<ThongTinXuatMD>> GetByIdHoaDon(string id)
        {
            var ThongTinHd = await (from h in _dbcontext.ThongTinXuats where h.IdXuatHangHoa == id select 
                                    new ThongTinXuatMD
                                    {
                                        IdThongTinXuat = h.IdThongTinXuat,
                                        IdXuatHangHoa = h.IdXuatHangHoa,
                                        IdHangHoa = h.IdHangHoa,
                                        SoLuong = h.SoLuong,
                                        Gia = h.Gia,
                                        TongGia = h.TongGia,
                                        IdHangHoaNavigation = h.IdHangHoaNavigation,
                                        IdKhuyenMai = h.IdKhuyenMai,
                                        IdKhuyenMaiNavigation = h.IdKhuyenMaiNavigation,
                                        IdXuatHangHoaNavigation = h.IdXuatHangHoaNavigation,

                                    }).ToListAsync();
            return ThongTinHd;
        }
    }
}
