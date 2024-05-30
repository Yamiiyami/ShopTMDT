using Microsoft.AspNetCore.Mvc;
using ShopTMDT.Data;
using ShopTMDT.ViewModel;

namespace ShopTMDT.services
{
    

    public interface ISlideRepository
    {
        public List<SlideMD> GetAll();

        public SlideVM GetByiD(int id);

        public JsonResult Add(SlideVM slideMD);

        public JsonResult Edit(SlideMD slideMD);

        public JsonResult Delete(int id);

    }

    public class SlideRepository : ISlideRepository
    {

        private readonly StmdtContext _dbcontext;

        public SlideRepository(StmdtContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public JsonResult Add(SlideVM slideMD)
        {

            try
            {
                var Slide = new Slide()
                {
                    Anh = slideMD.Anh,
                    Link = slideMD.Link,
                    Status = slideMD.Status,
                    Ten = slideMD.Ten
                };

                _dbcontext.Add(Slide);
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

        public JsonResult Delete(int id)
        {
            try
            {
                var slide = _dbcontext.Slides.SingleOrDefault(s => s.IdSlide == id);
                if (slide == null)
                {
                    return new JsonResult("Không tìm thấy")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                _dbcontext.Remove(slide);
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

        public JsonResult Edit(SlideMD slideMD)
        {
            try
            {
                var slide = _dbcontext.Slides.SingleOrDefault(s => s.IdSlide == slideMD.IdSlide);
                if (slide == null)
                {
                    return new JsonResult("Không tìm thấy")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                slide.Status = slideMD.Status;
                slide.Ten = slideMD.Ten;
                slide.Link = slideMD.Link;

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

        

        public List<SlideMD> GetAll()
        {
       
            var Slides = _dbcontext.Slides.Select( o => new SlideMD
            {
                Anh = o.Anh,
                IdSlide = o.IdSlide,
                Link = o.Link,
                Status = o.Status,
                Ten = o.Ten,
            }).ToList();
            return Slides;
        }

        public SlideVM GetByiD(int id)
        {
            var Slide = _dbcontext.Slides.FirstOrDefault(o => o.IdSlide == id);
            return new SlideVM
            {
                Anh = Slide.Anh,
                Link = Slide.Link,
                Status = Slide.Status,
                Ten = Slide.Ten,
            };
        }


    }
}
