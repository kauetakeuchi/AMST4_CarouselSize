namespace AMST4_Carousel2.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Createby { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}

