namespace FirzzApp.Business.Wrappers
{
    public sealed class Result<T>
    {
        public bool IsSuccess { get; private set; }

        public string Message { get; private set; }



        public static Result<T> Success(string message = "")
        {
            return new Result<T>()
            {
                IsSuccess = true,
                Message = $"{typeof(T).Name} {message} was created succesfully"
            };
        }


        public static Result<T> Fail(string message)
        {
            return new Result<T>()
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
