using System.Collections.Generic;

namespace RestaurantsList.DataAccess.MSSQL.Entities
{
    public class Restaurant : BaseEntity
    {
        public string Name { get; set; }

        #region Nav props

        public IEnumerable<CityRestaurantJunction> Restaurants { get; set; }

        #endregion
    }
}
