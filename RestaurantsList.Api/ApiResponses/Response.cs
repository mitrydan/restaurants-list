namespace RestaurantsList.Api.ApiResponses
{
    public class Response<T>
    {
        public T Data { get; private set; }

        public Response(T data)
        {
            Data = data;
        }
    }
}
