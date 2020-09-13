namespace Global_Intern.Util
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool Paginated { get; set; }
        public bool TokenRequired { get; set; }

        public Response(T response)
        {
            Data = response;
        }

        public Response(T response, bool tokenRequired, bool paginated)
        {
            Data = response;
            Paginated = paginated;
            TokenRequired = tokenRequired;
        }
    }
}
