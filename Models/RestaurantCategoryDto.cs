namespace ReserveBite.Api.Models
{
    public class RestaurantCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Rating { get; set; }
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}
