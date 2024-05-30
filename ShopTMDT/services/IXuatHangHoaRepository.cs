using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTMDT.Data;
using ShopTMDT.ViewModel;


namespace SpQuanAo.services
{
    public interface IXuatHangHoaRepository
    {
        public Task<List<HoaDonXuatMD>> GetAll();
        public Task<List<HoaDonXuatMD>> GetByIdUser(string id);
        public Task<HoaDonXuatVM> GetById(string id);
        public Task<JsonResult> Create(HoaDonRequest hoadon);
        public Task<JsonResult> Edit(string iduser, HoaDonEdit hoadonrq);
    }
    public class XuatHangHoaRepository: IXuatHangHoaRepository
    {
        private readonly StmdtContext _dbcontext;
        public XuatHangHoaRepository(StmdtContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<JsonResult> Create(HoaDonRequest hoadonrq)
        {
            try
            {
                if (hoadonrq == null)
                    return new JsonResult("dữ liệu không hợp lệ")
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                var hoadon = new XuatHangHoa
                {
                    IdHoaDon = hoadonrq.IdHoaDon,
                    NgayXuat = DateTime.Now,
                    DiaChi = hoadonrq.DiaChi,
                    GhiChu = hoadonrq.GhiChu,
                    Phone = hoadonrq.Phone,
                    IdUser = hoadonrq.IdUser,
                    IdVanChuyen = "a4b28caf-46bd-4b23-a008-7cacc7074214",
                    IdThanhToan = "efcfa30d-f308-454f-a5f5-30af0981cee0",
                };


                await _dbcontext.AddAsync(hoadon);
                await _dbcontext.SaveChangesAsync();
                return new JsonResult("Thêm hoá đơn thành công")
                {
                    StatusCode = StatusCodes.Status201Created,

                };
            }
            catch (Exception ex)
            {
                return new JsonResult("Thêm hoá đơn thất bại")
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
        }

        public async Task<JsonResult> Edit(string iduser, HoaDonEdit hoadonrq)
        {
            try
            {
                var hoadon = await _dbcontext.XuatHangHoas.SingleOrDefaultAsync(h => h.IdHoaDon ==  hoadonrq.IdHoaDon);
                if (hoadon == null)
                {
                    return new JsonResult("Không tìm thấy")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                if (hoadon.IdUser != iduser)
                {
                    return new JsonResult("Không thể sửa hoá đơn này")
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }


                hoadon.DiaChi = hoadonrq.DiaChi;
                hoadon.GhiChu = hoadonrq.GhiChu;
                hoadon.Phone = hoadonrq.Phone;
                await _dbcontext.SaveChangesAsync();
                return new JsonResult("sửa thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch
            {
                return new JsonResult("sửa thất bại")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }

        public async Task<List<HoaDonXuatMD>> GetAll()
        {
            var hoadonxuat = await _dbcontext.XuatHangHoas.Select(h => new HoaDonXuatMD
            {
                IdHoaDon = h.IdHoaDon,
                IdUser = h.IdUser,
                DiaChi = h.DiaChi,
                GhiChu = h.GhiChu,
                Phone = h.Phone,
                IdUserNavigation = h.IdUserNavigation,
                IdThanhToanNavigation = h.IdThanhToanNavigation,
                IdVanChuyenNavigation = h.IdVanChuyenNavigation,
                IdThanhToan = h.IdThanhToan,
                IdVanChuyen = h.IdVanChuyen,
                NgayXuat = h.NgayXuat,
                ThongTinXuats = h.ThongTinXuats,
                
            }).ToListAsync();
            return hoadonxuat;
        }

        public async Task<HoaDonXuatVM> GetById(string id)
        {
            var hoadonxuat = await _dbcontext.XuatHangHoas.FirstOrDefaultAsync(h => h.IdHoaDon == id);
                return new HoaDonXuatVM
                {
                    IdUserNavigation = hoadonxuat.IdUserNavigation,
                    DiaChi = hoadonxuat.DiaChi,
                    GhiChu = hoadonxuat.GhiChu,
                    IdUser = hoadonxuat.IdUser,
                    Phone = hoadonxuat.Phone,
                    NgayXuat = hoadonxuat.NgayXuat,
                    IdVanChuyen = hoadonxuat.IdVanChuyen,
                    IdThanhToan = hoadonxuat.IdThanhToan,
                    IdVanChuyenNavigation = hoadonxuat.IdVanChuyenNavigation,
                    IdThanhToanNavigation = hoadonxuat.IdThanhToanNavigation,
                
                };
        }

        public async Task<List<HoaDonXuatMD>> GetByIdUser(string id)
        {
            var hoadonxuat = await (from h in _dbcontext.XuatHangHoas
                                    where h.IdUser == id
                                    select new HoaDonXuatMD
                                    {
                                        IdHoaDon = h.IdHoaDon,
                                        DiaChi = h.DiaChi,
                                        GhiChu = h.GhiChu,
                                        IdUser = h.IdUser,
                                        Phone = h.Phone,
                                        NgayXuat = h.NgayXuat,
                                        IdVanChuyen = h.IdVanChuyen,
                                        IdThanhToan = h.IdThanhToan,
                                        IdThanhToanNavigation = h.IdThanhToanNavigation,
                                        IdVanChuyenNavigation = h.IdVanChuyenNavigation,
                                        ThongTinXuats = h.ThongTinXuats,
                                        IdUserNavigation = h.IdUserNavigation,
                                    }).ToListAsync();

            return hoadonxuat;
        }
    }
}
