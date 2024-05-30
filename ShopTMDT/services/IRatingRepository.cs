using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTMDT.Data;
using ShopTMDT.ViewModel;
using System.Runtime.InteropServices;

namespace ShopTMDT.services
{
    public interface IRatingRepository
    {
        public List<RatingMD> GetAll();

        public List<RatingVM> GetByiD(int idhanghoa);

        public JsonResult Add(RatingVM slideMD);

        public JsonResult Edit(string idUser, RatingRepuest slideMD);

        public JsonResult Delete(int id, string idUser);
    }
    public class RatingRepository : IRatingRepository
    {
        private readonly StmdtContext _dbcontext;
        public RatingRepository(StmdtContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public JsonResult Add(RatingVM ratingVM)
        {
            try
            {
                var danhgia =  _dbcontext.Ratings.SingleOrDefault(r => r.IdUser == ratingVM.IdUser && r.IdHangHoa == ratingVM.IdHangHoa);
                if(danhgia != null)
                {
                    return new JsonResult("Bạn đã đánh giá rồi")
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                var rating = new Rating()
                {
                  DanhGia = ratingVM.DanhGia,
                  IdHangHoa = ratingVM.IdHangHoa,
                  IdUser = ratingVM.IdUser,
                  SoSao = ratingVM.SoSao,
                };

                _dbcontext.Add(rating);
                _dbcontext.SaveChanges();
                return new JsonResult("Thêm thành công")
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch
            {
                return new JsonResult("Thêm thất bại")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }

        public JsonResult Delete(int id, string idUser)
        {
            try
            {

                var rating = _dbcontext.Ratings.SingleOrDefault(s => s.IdRating == id );
                if(rating.IdUser != idUser)
                {
                    return new JsonResult("Bạn không có quyền chỉnh sửa đánh giá này")
                    {
                        StatusCode = StatusCodes.Status403Forbidden
                    };
                }


                if (rating == null)
                {
                    return new JsonResult("Không tìm thấy")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                _dbcontext.Remove(rating);
                _dbcontext.SaveChanges();
                return new JsonResult("Xoá thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };

            }
            catch
            {
                return new JsonResult("Xoá thất bại")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public JsonResult Edit(string idUser, RatingRepuest ratingrq)
        {

            try
            {
                var rating = _dbcontext.Ratings.SingleOrDefault(s => s.IdRating == ratingrq.IdRating);
                if (rating == null)
                {
                    return new JsonResult("Không tìm thấy")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                if (rating.IdUser != idUser)
                {
                    return new JsonResult("Bạn không thể chỉnh sửa đánh giá này")
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                rating.SoSao = ratingrq.SoSao;
                rating.DanhGia = ratingrq.DanhGia;

                _dbcontext.SaveChanges();
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

        public List<RatingMD> GetAll()
        {
            var rating = _dbcontext.Ratings.Select(o => new RatingMD
            {
                DanhGia = o.DanhGia,
                SoSao = o.SoSao,
                IdHangHoa = o.IdHangHoa,
                IdRating = o.IdRating,
                IdUser = o.IdUser
                
            }).ToList();
            return rating;
        }

        public List<RatingVM> GetByiD( int idhanghoa)
        {
            var rating = (from r in _dbcontext.Ratings where r.IdHangHoa == idhanghoa select new RatingVM
            {
                SoSao = r.SoSao,
                DanhGia = r.DanhGia,
                IdHangHoa = r.IdHangHoa,
                IdUser = r.IdUser,
            }).ToList();
            if(rating is null)
            {
                return null;
            }
            return rating;
        }
    }
}
