using ShopTMDT.Data;
using System.ComponentModel.DataAnnotations;

namespace ShopTMDT.ViewModel
{
    public class RatingVM
    {

        public string? DanhGia { get; set; }

        public string? IdUser { get; set; }

        public int? IdHangHoa { get; set; }

        public int? SoSao { get; set; }


    }
    public class RatingMD : RatingVM
    {
        public int IdRating { get; set; }
        public virtual HangHoa? IdHangHoaNavigation { get; set; }

        public virtual User? IdUserNavigation { get; set; }

    }
    public class RatingRepuest
    {
        public int IdRating { get; set; }
        public string? DanhGia { get; set; }
        [Required]
        public int? SoSao { get; set; }

    }
}
