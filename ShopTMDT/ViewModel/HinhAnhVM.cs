
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ShopTMDT.Data;

namespace ShopTMDT.ViewModel
{
    public class HinhAnhVM
    {
        public string? HinhAnh1 { get; set; }

        public string? TenHinhAnh { get; set; }

        public int? IdHangHoa { get; set; }
    }
    public class HinhAnhMD : HinhAnhVM
    {
        public int IdHinhAnh { get; set; }

        public virtual HangHoa? IdHangHoaNavigation { get; set; }
    }
    public class UpLoadHinhAnh
    {
        public List<IFormFile> files { get; set; }
        public string? TenHinhAnh { get; set; }
        public int? IdHangHoa { get; set; }
    }
}
