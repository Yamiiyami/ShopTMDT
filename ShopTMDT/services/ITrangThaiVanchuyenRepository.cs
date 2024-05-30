using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTMDT.Data;
using ShopTMDT.ViewModel;


namespace SpQuanAo.services
{
    public interface ITrangThaiVanchuyenRepository
    {
        public Task<List<StatusTransportMD>> GetAll();
        public Task<JsonResult> Create(StatusTransportResponse addtrangthai);
        public Task<JsonResult> Edit(StatusTransportResponse addtrangthai);
        public Task<JsonResult> Delete(string id);
    }
    public class TrangThaiVanchuyenRepository : ITrangThaiVanchuyenRepository
    {
        private readonly StmdtContext _dbcontext;
        public TrangThaiVanchuyenRepository(StmdtContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<JsonResult> Create(StatusTransportResponse trangthai)
        {
            try
            {
                var status = await _dbcontext.TrangThaiVanTruyens.SingleOrDefaultAsync(u => u.StatusName == trangthai.StatusName);
                if (status != null)
                {
                    return new JsonResult("đã tồn tại")
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }
                var statusvt = new TrangThaiVanTruyen
                {
                    IdVanTruyen = trangthai.IdVanTruyen,
                    StatusName = trangthai.StatusName,
                    Mota = trangthai.Mota,
                };

                await _dbcontext.AddRangeAsync(statusvt);
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

        public async Task<JsonResult> Delete(string id)
        {
            try
            {
                var status = await _dbcontext.TrangThaiVanTruyens.SingleOrDefaultAsync(t => t.IdVanTruyen == id);
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

        public async Task<JsonResult> Edit(StatusTransportResponse trangthai)
        {
            try
            {
                var status = await _dbcontext.TrangThaiVanTruyens.SingleOrDefaultAsync(t => t.IdVanTruyen == trangthai.IdVanTruyen);
                if (status == null)
                {
                    return new JsonResult("Không tìm thấy")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                status.StatusName = trangthai.StatusName;
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

        public async Task<List<StatusTransportMD>> GetAll()
        {
            var dsStatus = await _dbcontext.TrangThaiVanTruyens.Select(t => new StatusTransportMD
            {
                IdVanTruyen = t.IdVanTruyen,
                StatusName = t.StatusName,
                Mota = t.Mota,
                XuatHangHoas = t.XuatHangHoas,
            }).ToListAsync();
            return dsStatus;
        }

    }
}
