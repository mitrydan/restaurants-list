namespace RestaurantsList.Models
{
    public sealed class CityRestaurantJunction : BaseModel
    {
        public long CityId { get; set; }

        public long RestaurantId { get; set; }
    }
}
