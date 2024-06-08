using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ShopTMDT.Data;
using ShopTMDT.ViewModel;
using SpQuanAo.services;

namespace ShopTMDT.services
{
    public interface IHangHoaRepository
    {
        public Task<IEnumerable<HangHoaMD>> Getall();
        public Task<IEnumerable<HangHoaMD>> GetByIdCate(int id);
        public Task<HangHoaVM> GetById(int id);
        public Task<List<HangHoaMD>> GetPage(int page, int pageSize);
        public Task<JsonResult> Edit(int id,HangHoaRequest hanghoa);
        public Task<JsonResult> Create(HangHoaRequest hanghoa,List<IFormFile> files);
        public Task<JsonResult> Remove(int id);
    }
    public class HangHoaRepository : IHangHoaRepository
    {
        private readonly StmdtContext _dbcontext;
        private readonly IWriteFileRepository _writeFileRepository;
        public HangHoaRepository(StmdtContext dbContext, IWriteFileRepository writeFileRepository)
        {
            _writeFileRepository = writeFileRepository;
            _dbcontext = dbContext;
        }
        public async Task<JsonResult> Create(HangHoaRequest hanghoa,List<IFormFile> files)
        {
            try
            {
                var images = await _writeFileRepository.WriteFileAsync(files, "Users");
                var hh = new HangHoa();
                hh.TenHangHoa = hanghoa.TenHangHoa;
                hh.HinhAnh = images[0];
                hh.MauSac = hanghoa.MauSac;
                hh.Size = hanghoa.Size;
                hh.SoLuong = hanghoa.SoLuong;
                hh.MoTa = hanghoa.MoTa;
                hh.Gia = hanghoa.Gia;
                hh.IdKhuyenMai = hanghoa.IdKhuyenMai;
                hh.IdLoaiHangHoa = hanghoa.IdLoaiHangHoa;
                await _dbcontext.AddAsync(hh);
                await _dbcontext.SaveChangesAsync();

                var id = await _dbcontext.HangHoas.MaxAsync(h => h.IdHangHoa);
                for (int i =1; i < images.Count; i++)
                {
                    var hinhanh = new HinhAnh();
                    hinhanh.HinhAnh1 = images[i];
                    hinhanh.IdHangHoa = id;
                    await _dbcontext.AddAsync(hinhanh);
                }
                await _dbcontext.SaveChangesAsync();
                return new JsonResult("tạo thành công")
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch
            {
                return new JsonResult("tạo thất bại")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
           
            
        }

        public async Task<IEnumerable<HangHoaMD>> Getall()
        {
            var dsSanpham = await _dbcontext.HangHoas.Select(h => new HangHoaMD
            {
                IdHangHoa = h.IdHangHoa,
                TenHangHoa = h.TenHangHoa,
                HinhAnh = h.HinhAnh,
                MauSac = h.MauSac,
                Size = h.Size,
                SoLuong = h.SoLuong,
                MoTa = h.MoTa,
                HinhAnhs = h.HinhAnhs,
                Gia = h.Gia,
                IdKhuyenMai = h.IdKhuyenMai,
                ThongTinDonNhaps = h.ThongTinDonNhaps,
                ThongTinXuats = h.ThongTinXuats,
                TongRating = h.TongRating,
                TongSao = h.TongSao,
                Ratings = h.Ratings,
                IdLoaiHangHoa = h.IdLoaiHangHoa,
                IdKhuyenMaiNavigation = h.IdKhuyenMaiNavigation,
                IdLoaiHangHoaNavigation = h.IdLoaiHangHoaNavigation,

            }).ToListAsync();
            return dsSanpham;
        }

        public async Task<HangHoaVM> GetById(int id)
        {
            var dsSanpham = await _dbcontext.HangHoas.SingleOrDefaultAsync(h => h.IdHangHoa == id);
            if (dsSanpham is null)
                return null;
            return new HangHoaVM
            {

                TenHangHoa = dsSanpham.TenHangHoa,
                HinhAnh = dsSanpham.HinhAnh,
                MauSac = dsSanpham.MauSac,
                Size = dsSanpham.Size,
                SoLuong = dsSanpham.SoLuong,
                MoTa = dsSanpham.MoTa,
                Gia = dsSanpham.Gia,
                IdKhuyenMai = dsSanpham.IdKhuyenMai,
                TongRating = dsSanpham.TongRating,
                TongSao = dsSanpham.TongSao,
                IdLoaiHangHoa = dsSanpham.IdLoaiHangHoa,
                IdKhuyenMaiNavigation = dsSanpham.IdKhuyenMaiNavigation,
                IdLoaiHangHoaNavigation = dsSanpham.IdLoaiHangHoaNavigation,
            };
        }

        public async Task<List<HangHoaMD>> GetPage(int page , int pageSize )
        {
            
            var totalCount = await _dbcontext.HangHoas.CountAsync();
            var hanghoa = await _dbcontext.HangHoas
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(h => new HangHoaMD
                {
                    IdHangHoa = h.IdHangHoa,
                    TenHangHoa = h.TenHangHoa,
                    HinhAnh = h.HinhAnh,
                    MauSac = h.MauSac,
                    Size = h.Size,
                    SoLuong = h.SoLuong,
                    MoTa = h.MoTa,
                    HinhAnhs = h.HinhAnhs,
                    Gia = h.Gia,
                    IdKhuyenMai = h.IdKhuyenMai,
                    ThongTinDonNhaps = h.ThongTinDonNhaps,
                    ThongTinXuats = h.ThongTinXuats,
                    TongRating = h.TongRating,
                    TongSao = h.TongSao,
                    Ratings = h.Ratings,
                    IdLoaiHangHoa = h.IdLoaiHangHoa,
                    IdKhuyenMaiNavigation = h.IdKhuyenMaiNavigation,
                    IdLoaiHangHoaNavigation = h.IdLoaiHangHoaNavigation,
                })
                .ToListAsync();
            return hanghoa;
        }

        public async Task<IEnumerable<HangHoaMD>> GetByIdCate(int id)
        {
            var sanpham = await (from s in _dbcontext.HangHoas
                                 where s.IdLoaiHangHoa == id
                                 select new HangHoaMD
                                 {
                                     IdHangHoa = s.IdHangHoa,
                                     TenHangHoa = s.TenHangHoa,
                                     HinhAnh = s.HinhAnh,
                                     MauSac = s.MauSac,
                                     Size = s.Size,
                                     SoLuong = s.SoLuong,
                                     MoTa = s.MoTa,
                                     Gia = s.Gia,
                                     IdKhuyenMai = s.IdKhuyenMai,
                                     TongRating = s.TongRating,
                                     TongSao = s.TongSao,
                                     IdLoaiHangHoa = s.IdLoaiHangHoa,
                                     IdKhuyenMaiNavigation = s.IdKhuyenMaiNavigation,
                                     IdLoaiHangHoaNavigation = s.IdLoaiHangHoaNavigation,
                                     HinhAnhs = s.HinhAnhs,
                                     Ratings = s.Ratings,
                                     ThongTinDonNhaps = s.ThongTinDonNhaps,
                                     ThongTinXuats = s.ThongTinXuats,

                                 }).ToListAsync();
            return sanpham;

        }

        public async Task<JsonResult> Remove(int id)
        {
            try
            {
                var sanpham = await _dbcontext.HangHoas.SingleOrDefaultAsync(h => h.IdHangHoa == id);
                if (sanpham is null)
                {
                    return new JsonResult("Không tìm thấy")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                _dbcontext.Remove(sanpham);
                await _dbcontext.SaveChangesAsync();
                return new JsonResult("xoá thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch
            {
                return new JsonResult("xoá thất bại")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            
        }

        public async Task<JsonResult> Edit(int id,HangHoaRequest hanghoa)
        {
            try
            {
                var sanpham = await _dbcontext.HangHoas.SingleOrDefaultAsync(h => h.IdHangHoa == id);
                if (sanpham is null)
                {
                    return new JsonResult("không tìm thấy ")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                sanpham.TenHangHoa = hanghoa.TenHangHoa;
                sanpham.HinhAnh = hanghoa.HinhAnh;
                sanpham.MauSac = hanghoa.MauSac;
                sanpham.Size = hanghoa.Size;
                sanpham.SoLuong = hanghoa.SoLuong;
                sanpham.MoTa = hanghoa.MoTa;
                sanpham.Gia = hanghoa.Gia;
                sanpham.IdKhuyenMai = hanghoa.IdKhuyenMai;
                sanpham.IdLoaiHangHoa = hanghoa.IdLoaiHangHoa;
                await _dbcontext.SaveChangesAsync();
                return new JsonResult("xửa thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch
            {
                return new JsonResult("xửa thất bại")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

        }

    }
}
