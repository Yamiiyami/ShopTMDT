using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTMDT.Data;
using ShopTMDT.ViewModel;


namespace SpQuanAo.services
{
    public interface ITrangThaiThanhToanRepository
    {
        public Task<List<StatusPaymentMD>> GetAll();
        public Task<JsonResult> Create(StatusPaymentRespoonse trangthai);
        public Task<JsonResult> Edit(StatusPaymentRespoonse trangthai);
        public Task<JsonResult> Delete(string id);

    }
    public class TrangThaiThanhToanRepository : ITrangThaiThanhToanRepository
    {
        private readonly StmdtContext _dbcontext;
        public TrangThaiThanhToanRepository(StmdtContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<JsonResult> Create(StatusPaymentRespoonse trangthai)
        {
            if(trangthai == null)
            {
                return new JsonResult("du lieu ko hop le")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            try
            {
                var status = await _dbcontext.TrangThaiThanhToans.SingleOrDefaultAsync(u => u.Name == trangthai.Name);
                if (status != null)
                {
                    return new JsonResult("đã tồn tại")
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }
                var Status = new TrangThaiThanhToan
                {
                    IdThanhToan = trangthai.IdThanhToan,
                    Name = trangthai.Name,
                    Mota = trangthai.Mota,

                };

                await _dbcontext.AddRangeAsync(Status);
                await _dbcontext.SaveChangesAsync();
                return new JsonResult("Tạo thành công")
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch (Exception ex)
            {
                return new JsonResult("tạoo thất bại")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            
        }

        public async Task<JsonResult> Delete(string id)
        {
            try
            {
                var status = await _dbcontext.TrangThaiThanhToans.SingleOrDefaultAsync(t => t.IdThanhToan == id);
                if (status == null)
                {
                    return new JsonResult("Không tìm thấy")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                _dbcontext.Remove(status);
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

        public async Task<JsonResult> Edit(StatusPaymentRespoonse trangthai)
        {
            try
            {
                var status = await _dbcontext.TrangThaiThanhToans.SingleOrDefaultAsync(t => t.IdThanhToan == trangthai.IdThanhToan);
                if (status == null)
                {
                    return new JsonResult("Không tìm thấy")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                status.Name = trangthai.Name;
                status.Mota = trangthai.Mota;
                await _dbcontext.SaveChangesAsync();
                return new JsonResult("Sửa thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch
            {
                return new JsonResult("Sửa thất bại")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }

        public async Task<List<StatusPaymentMD>> GetAll()
        {
            var dsStatus =await _dbcontext.TrangThaiThanhToans.Select(t => new StatusPaymentMD
            {
                IdThanhToan = t.IdThanhToan,
                Name = t.Name,
                Mota = t.Mota,
                XuatHangHoas = t.XuatHangHoas,
            }).ToListAsync();
            return dsStatus;
        }
    }
}
