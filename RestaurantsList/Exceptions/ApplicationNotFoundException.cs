namespace RestaurantsList.Exceptions
{
    public class ApplicationNotFoundException : ApplicationException
    {
        public ApplicationNotFoundException(string message)
            : base(message)
        { }
    }
}
