using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTMDT.Data;
using ShopTMDT.ViewModel;


namespace ShopTMDT.services
{
    public interface INhapHangHoaRepository
    {
        JsonResult AddNhapHangHoa(NhaphanghoaReposet nhapHangHoaVM);
        JsonResult DeleteNhapHangHoa(int id);
        JsonResult EditNhapHangHoa(int id, NhapHangHoaVM nhapHangHoaVM);
        List<NhapHangHoaMD> GetAll();
    }
    public class NhapHangHoaRepository : INhapHangHoaRepository
    {
        private readonly StmdtContext _context;

        public NhapHangHoaRepository(StmdtContext context)
        {
            _context = context;
        }

        public JsonResult AddNhapHangHoa(NhaphanghoaReposet nhapHangHoa)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == nhapHangHoa.IdUser);
                if(user == null)
                {
                    return new JsonResult("khong tim thay nguoi dung")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                var nhap = new NhapHangHoa
                {
                    IdNhapHangHoa = nhapHangHoa.IdNhapHangHoa,
                    IdUser = nhapHangHoa.IdUser,
                    NgayTao = DateTime.Now,
                    IdNhaCungCap = nhapHangHoa.IdNhaCungCap
                };

                _context.Add(nhap);
                _context.SaveChanges();
                return new JsonResult("Nhập hàng hoá đã thêm thành công")
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch
            {
                return new JsonResult("Nhập hàng hoá đã thêm thất bại")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
           
        }

        public JsonResult DeleteNhapHangHoa(int id)
        {
            var nhap = _context.NhapHangHoas.SingleOrDefault(o => o.IdNhaCungCap == id);

            if (nhap == null)
            {
                return new JsonResult("Nhập hàng hoá không tồn tại")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                _context.NhapHangHoas.Remove(nhap);
                _context.SaveChanges();
                return new JsonResult("Nhập hàng hoá đã xoá thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public JsonResult EditNhapHangHoa(int id, NhapHangHoaVM nhapHangHoaVM)
        {
            var nhap = _context.NhapHangHoas.SingleOrDefault(o => o.IdNhaCungCap == id);
            if (nhap == null)
            {
                return new JsonResult("Nhập hàng hoá không tồn tại")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                nhap.IdUser = nhapHangHoaVM.IdUser;
                nhap.NgayTao = nhapHangHoaVM.NgayTao;
                nhap.IdNhaCungCap = nhapHangHoaVM.IdNhaCungCap;
                _context.SaveChanges();
                return new JsonResult("Nhập hàng hoá đã sửa thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public List<NhapHangHoaMD> GetAll()
        {
            var nhap = _context.NhapHangHoas.Select(o => new NhapHangHoaMD
            {
                IdNhapHangHoa = o.IdNhapHangHoa,
                IdUser = o.IdUser,
                NgayTao = o.NgayTao,
                IdNhaCungCap = o.IdNhaCungCap,
                IdNhaCungCapNavigation = o.IdNhaCungCapNavigation,
            }).ToList();
            return nhap;
        }
    }
}
