using ShopTMDT.Data;

namespace ShopTMDT.ViewModel
{
    public class StatusPaymentVM
    {

        public string? Name { get; set; }

        public string? Mota { get; set; }

    }
    public class StatusPaymentMD : StatusPaymentVM
    {
        public string IdThanhToan { get; set; } = null!;

        public virtual ICollection<XuatHangHoa> XuatHangHoas { get; set; } = new List<XuatHangHoa>();

    }

    public class StatusPaymentRespoonse
    {
        public string IdThanhToan { get; set; } = null!;
        public string? Name { get; set; }
        public string? Mota { get; set; }

    }
}
