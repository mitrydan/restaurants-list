namespace RestaurantsList.DataAccess.MSSQL.Entities
{
    public class CityRestaurantJunction : BaseEntity
    {
        public long CityId { get; set; }

        public long RestaurantId { get; set; }

        #region Nav props

        public City City { get; set; }

        public Restaurant Restaurant { get; set; }

        #endregion
    }
}
