namespace FirzzApp.Business.Wrappers
{
    public sealed class Result
    {

        public bool IsSuccess { get; private set; }

        public string Message { get; private set; }



        public static Result Success(string message = "")
        {
            return new Result()
            {
                IsSuccess = true,
                Message = message
            };
        }

        public static Result Fail(string message)
        {
            return new Result()
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
