using Microsoft.AspNetCore.Mvc;
using ShopTMDT.Data;
using ShopTMDT.ViewModel;


namespace ShopTMDT.services
{
    public interface IKhuyenMaiRepository
    {
        JsonResult AddKhuyenMai(KhuyenMaiVM khuyenMaiVM);
        JsonResult DeleteKhuyenMai(int id);
        JsonResult EditKhuyenMai(int id, KhuyenMaiVM khuyenMaiVM);
        List<KhuyenMaiMD> GetAll();
    }
    public class KhuyenMaiRepository : IKhuyenMaiRepository
    {
        private readonly StmdtContext _context;

        public KhuyenMaiRepository(StmdtContext context)
        {
            _context = context;
        }

        public JsonResult AddKhuyenMai(KhuyenMaiVM khuyenMaiVM)
        {
            var khuyen = new KhuyenMai
            {
                TenKhuyenMai = khuyenMaiVM.TenKhuyenMai,
                GiaKhuyenMai = khuyenMaiVM.GiaKhuyenMai,
                MoTa = khuyenMaiVM.MoTa
            };
            _context.KhuyenMais.Add(khuyen);
            _context.SaveChanges();
            return new JsonResult("Khuyến mãi đã thêm thành công")
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        public JsonResult DeleteKhuyenMai(int id)
        {
            var khuyen = _context.KhuyenMais.SingleOrDefault(o => o.IdKhuyenMai == id);
            if (khuyen == null)
            {
                return new JsonResult("Khuyến mãi không tồn tại")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                _context.KhuyenMais.Remove(khuyen);
                _context.SaveChanges();
                return new JsonResult("Khuyến mãi đã xoá thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public JsonResult EditKhuyenMai(int id, KhuyenMaiVM khuyenMaiVM)
        {
            var khuyen = _context.KhuyenMais.SingleOrDefault(o => o.IdKhuyenMai == id);
            if (khuyen == null)
            {
                return new JsonResult("Khuyến mãi không tồn tại")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                khuyen.TenKhuyenMai = khuyenMaiVM.TenKhuyenMai;
                khuyen.GiaKhuyenMai = khuyenMaiVM.GiaKhuyenMai;
                khuyen.MoTa = khuyenMaiVM.MoTa;
                _context.SaveChanges();
                return new JsonResult("Khuyến mãi đã sửa thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public List<KhuyenMaiMD> GetAll()
        {
            var khuyen = _context.KhuyenMais.Select(o => new KhuyenMaiMD
            {
                IdKhuyenMai = o.IdKhuyenMai,
                TenKhuyenMai = o.TenKhuyenMai,
                GiaKhuyenMai = o.GiaKhuyenMai,
                MoTa = o.MoTa
            }).ToList();
            return khuyen;
        }
    }
}
