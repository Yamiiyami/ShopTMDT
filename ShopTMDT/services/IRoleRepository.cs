using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ShopTMDT.Data;
using ShopTMDT.ViewModel;

namespace ShopTMDT.services
{
    public interface IRoleRepository
    {
        public Task<List<RoleResponse>> GetAll();
        public Task<JsonResult> CreateRole(RoleVM role);
        public Task<JsonResult> DeleteRole(string id);


    }
    public class RoleRepository : IRoleRepository
    {
        private readonly StmdtContext _dbcontext;
        public RoleRepository(StmdtContext dbcontext) 
        {
            _dbcontext = dbcontext;
        }
        public async Task<JsonResult> CreateRole(RoleVM rolevm)
        {
            try
            {
                var checkrole = await _dbcontext.Roles.SingleOrDefaultAsync(r => r.Name == rolevm.name);
                if (checkrole != null)
                {
                    return new JsonResult(rolevm.name + " đã tồn tại")
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                var role = new Role()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = rolevm.name,
                    NormalizedName = rolevm.name.ToUpper(),
                };

                await _dbcontext.AddAsync(role);
                await _dbcontext.SaveChangesAsync();
                return new JsonResult("tạo thành công")
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch (Exception ex)
            {
                return new JsonResult("tạo không thành công")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }

        public async Task<JsonResult> DeleteRole(string id)
        {
            try
            {
                var checkrole = await _dbcontext.Roles.SingleOrDefaultAsync(r => r.Id == id);
                if(checkrole == null)
                {
                    return new JsonResult("không tìm thấy")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                _dbcontext.Roles.Remove(checkrole!);
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

        public async Task<List<RoleResponse>> GetAll()
        {
             return await (from r in _dbcontext.Roles select new RoleResponse {
                Id = r.Id ,
                Name = r.Name ,
            }).ToListAsync();
        }
    }
}
