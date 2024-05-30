using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ShopTMDT.Data;
using ShopTMDT.ViewModel;
using System.Threading.Tasks.Dataflow;

namespace ShopTMDT.services
{
    public interface ILoaiHangHoaRepository
    {
        public Task<IEnumerable<LoaiHangHoaMD>> GetAll();
        public Task<LoaiHangHoa> GetById(int id);
        public Task<JsonResult> Create(LoaihangHoaVM loaihangHoa);
        public Task<JsonResult> Remove(int id);
        public Task<JsonResult> Edit(LoaiHangHoaResponse loaihangHoa);
    }
    public class LoaiHangHoaRepository : ILoaiHangHoaRepository
    {
        private readonly StmdtContext _dbcontext; 
        public LoaiHangHoaRepository(StmdtContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<LoaiHangHoaMD>> GetAll()
        {
            var lhh = await (from l in _dbcontext.LoaiHangHoas select new LoaiHangHoaMD
            { 
                IdLoaiHangHoa = l.IdLoaiHangHoa, 
                TenLoai = l.TenLoai, IdKhuyenMai = l.IdKhuyenMai, 
                HangHoas = l.HangHoas, 
                IdKhuyenMaiNavigation = l.IdKhuyenMaiNavigation 
            }).ToListAsync();

            return lhh;
        }
        public async Task<LoaiHangHoa> GetById(int id)
        {
            var lhh= await _dbcontext.LoaiHangHoas.SingleOrDefaultAsync(l => l.IdLoaiHangHoa == id);
            return lhh;
        }

        public async Task<JsonResult> Create(LoaihangHoaVM loaihangHoa)
        {
            try
            {
                var lhh = await _dbcontext.LoaiHangHoas.SingleOrDefaultAsync(l => l.TenLoai == loaihangHoa.TenLoai);
                if (lhh != null)
                {
                    return new JsonResult("Đã tồn tại")
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                var loaihh = new LoaiHangHoa
                {
                    TenLoai = loaihangHoa.TenLoai,
                    IdKhuyenMai = loaihangHoa.IdKhuyenMai
                };
                await _dbcontext.AddAsync(loaihh);
                await _dbcontext.SaveChangesAsync();

                return new JsonResult("Đã thêm thành công")
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch
            {
                return new JsonResult("Đã thêm thất bại ")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }

        public async Task<JsonResult> Remove(int id)
        {
            try
            {
                var lhh = await _dbcontext.LoaiHangHoas.SingleOrDefaultAsync(l => l.IdLoaiHangHoa == id);
                if(lhh is null)
                {
                    return new JsonResult("không tìm thấy")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                _dbcontext.Remove(lhh);
                await _dbcontext.SaveChangesAsync();
                return new JsonResult("Xoá thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
                
            }
            catch
            {
                return new JsonResult("Xoá thất bại")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }

        public async Task<JsonResult> Edit(LoaiHangHoaResponse loaihangHoa)
        {
            try
            {
                var lhh = await _dbcontext.LoaiHangHoas.SingleOrDefaultAsync(l => l.IdLoaiHangHoa == loaihangHoa.IdLoaiHangHoa);
                if (lhh == null)
                {
                    return new JsonResult("không tìm thấy")
                    {
                        StatusCode = StatusCodes.Status404NotFound,

                    };
                }
                lhh.TenLoai = loaihangHoa.TenLoai;
                lhh.IdKhuyenMai = loaihangHoa.IdKhuyenMai;
                await _dbcontext.SaveChangesAsync();
                return new JsonResult("sửa thành công")
                {
                    StatusCode = StatusCodes.Status200OK,

                };
            }
            catch
            {
                return new JsonResult("sửa thất bại")
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

        }


    }
}
