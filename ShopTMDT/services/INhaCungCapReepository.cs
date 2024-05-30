using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using ShopTMDT.Data;
using ShopTMDT.ViewModel;


namespace ShopTMDT.services
{
    public interface INhaCungCapReepository
    {
        JsonResult AddNhaCungCap(int id, NhaCungCapVM nhaCungCapVM);
        JsonResult EditNhaCungCap(int id, NhaCungCapVM nhaCungCapVM);
        JsonResult DeleteNhaCungCap(int id);
        List<NhaCungCapMD> GetAll();
    }
    public class NhaCungCapReepository : INhaCungCapReepository
    {
        private readonly StmdtContext _context;

        public NhaCungCapReepository(StmdtContext context)
        {
            _context = context;
        }

        public JsonResult AddNhaCungCap(int id, NhaCungCapVM nhaCungCapVM)
        {
            var check = _context.NhaCungCaps.SingleOrDefault(o => o.IdNhaCungCap == id || o.Ten == nhaCungCapVM.Ten);
            if (check == null)
            {
                var nha = new NhaCungCap
                {
                    Ten = nhaCungCapVM.Ten,
                    SoDienThoai = nhaCungCapVM.SoDienThoai,
                    DiaChi = nhaCungCapVM.DiaChi,
                    Email = nhaCungCapVM.Email,
                    NgayHopTac = nhaCungCapVM.NgayHopTac
                };
                _context.NhaCungCaps.Add(nha);
                _context.SaveChanges();
                return new JsonResult("Nhà cung cấp đã thêm thành công")
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            else
            {
                return new JsonResult("Nhà cung cấp đã tồn tại")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }

        public JsonResult DeleteNhaCungCap(int id)
        {
            var nha = _context.NhaCungCaps.SingleOrDefault(o => o.IdNhaCungCap == id);
            if (nha == null)
            {
                return new JsonResult("Nhà cung cấp không tồn tại")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                _context.NhaCungCaps.Remove(nha);
                _context.SaveChanges();
                return new JsonResult("Nhà cung cấp đã xoá thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public JsonResult EditNhaCungCap(int id, NhaCungCapVM nhaCungCapVM)
        {
            var nha = _context.NhaCungCaps.SingleOrDefault(o => o.IdNhaCungCap == id);
            if (nha == null)
            {
                return new JsonResult("Nhà cung cấp không tồn tại")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                nha.Ten = nhaCungCapVM.Ten;
                nha.SoDienThoai = nhaCungCapVM.SoDienThoai;
                nha.DiaChi = nhaCungCapVM.DiaChi;
                nha.Email = nhaCungCapVM.Email;
                nha.NgayHopTac = nhaCungCapVM.NgayHopTac;
                _context.SaveChanges();
                return new JsonResult("Nhà cung cấp đã sửa thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }


        public List<NhaCungCapMD> GetAll()
        {
            var nha = _context.NhaCungCaps.Select(o => new NhaCungCapMD
            {
                IdNhaCungCap = o.IdNhaCungCap,
                Ten = o.Ten,
                SoDienThoai = o.SoDienThoai,
                DiaChi = o.DiaChi,
                Email = o.Email,
                NgayHopTac = o.NgayHopTac
            }).ToList();
            return nha;
        }
    }
}
