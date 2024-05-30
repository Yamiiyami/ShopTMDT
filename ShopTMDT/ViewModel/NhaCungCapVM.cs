

using ShopTMDT.Data;

namespace ShopTMDT.ViewModel
{
    public class NhaCungCapVM
    {
        public string? Ten { get; set; }

        public string? SoDienThoai { get; set; }

        public string? DiaChi { get; set; }

        public string? Email { get; set; }

        public DateTime? NgayHopTac { get; set; }
    }
    public class NhaCungCapMD : NhaCungCapVM
    {
        public int IdNhaCungCap { get; set; }

        public virtual ICollection<NhapHangHoa> NhapHangHoas { get; set; } = new List<NhapHangHoa>();
    }
}
