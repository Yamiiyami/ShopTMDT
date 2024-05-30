using Microsoft.AspNetCore.Mvc;
using ShopTMDT.Data;
using ShopTMDT.ViewModel;
using SpQuanAo.services;
using System.ComponentModel;

namespace ShopTMDT.services
{
    public interface IHinhAnhRepository
    {
        Task<JsonResult> AddHinhAnhAsync(UpLoadHinhAnh upLoadHinhAnh);
        JsonResult DeleteHinhAnh(int id);
        JsonResult EditHinhAnh(int id, HinhAnhVM hinhAnhVM);
        List<HinhAnhMD> GetAll();
    }
    public class HinhAnhRepository : IHinhAnhRepository
    {
        private readonly StmdtContext _context;
        private readonly IWriteFileRepository _writeFileRepository;
        public HinhAnhRepository(StmdtContext context , IWriteFileRepository writeFileRepository)
        {
            _writeFileRepository = writeFileRepository;
            _context = context;
        }

        public async Task<JsonResult> AddHinhAnhAsync(UpLoadHinhAnh upLoadHinhAnh)
        {
            try
            {
                var images = await _writeFileRepository.WriteFileAsync(upLoadHinhAnh.files, "Users");
                if (images == null)
                {
                    return new JsonResult("thêm không thành công")
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                foreach (var item in images)
                {
                    var hinh = new HinhAnh
                    {
                        HinhAnh1 = item,
                        TenHinhAnh = upLoadHinhAnh.TenHinhAnh,
                        IdHangHoa = upLoadHinhAnh.IdHangHoa
                    };
                    await _context.HinhAnhs.AddAsync(hinh);
                }
                await _context.SaveChangesAsync();
                return new JsonResult("Hình ảnh đã thêm thành công")
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch (Exception ex)
            {
                return new JsonResult("Hình ảnh thêm không thành công")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }

        public JsonResult DeleteHinhAnh(int id)
        {
            var hinh = _context.HinhAnhs.SingleOrDefault(o => o.IdHinhAnh == id);
            if (hinh == null)
            {
                return new JsonResult("Hình ảnh không tồn tại")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                _context.HinhAnhs.Remove(hinh);
                _context.SaveChanges();
                return new JsonResult("Hình ảnh đã xoá thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public JsonResult EditHinhAnh(int id, HinhAnhVM hinhAnhVM)
        {
            var hinh = _context.HinhAnhs.SingleOrDefault(o => o.IdHinhAnh == id);
            if (hinh == null)
            {
                return new JsonResult("Hình ảnh không tồn tại")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                hinh.HinhAnh1 = hinhAnhVM.HinhAnh1;
                hinh.TenHinhAnh = hinhAnhVM.TenHinhAnh;
                hinh.IdHangHoa = hinhAnhVM.IdHangHoa;
                _context.SaveChanges();
                return new JsonResult("Hình ảnh đã sửa thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public List<HinhAnhMD> GetAll()
        {
            var hinh = _context.HinhAnhs.Select(o => new HinhAnhMD
            {
                IdHinhAnh = o.IdHinhAnh,
                HinhAnh1 = o.HinhAnh1,
                TenHinhAnh = o.TenHinhAnh,
                IdHangHoa = o.IdHangHoa
            }).ToList();
            return hinh;
        }
    }
}
