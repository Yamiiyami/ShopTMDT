using ShopTMDT.Data;

namespace ShopTMDT.ViewModel
{
    public class StatusTransportVM
    {

        public string? StatusName { get; set; }

        public string? Mota { get; set; }

    }
    public class StatusTransportMD : StatusTransportVM
    {
        public string IdVanTruyen { get; set; } = null!;
        public virtual ICollection<XuatHangHoa> XuatHangHoas { get; set; } = new List<XuatHangHoa>();


    }

    public class StatusTransportResponse
    {
        public string IdVanTruyen { get; set; } = null!;
        public string? StatusName { get; set; }
        public string? Mota { get; set; }

    }
}
