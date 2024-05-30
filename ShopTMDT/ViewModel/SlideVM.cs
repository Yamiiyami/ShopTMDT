namespace ShopTMDT.ViewModel
{
    public class SlideVM
    {

        public string? Ten { get; set; }

        public string? Anh { get; set; }

        public int? Status { get; set; }

        public string? Link { get; set; }
    }
    public class SlideMD : SlideVM
    {
        public int IdSlide { get; set; }

    }
}
