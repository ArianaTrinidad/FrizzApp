namespace FirzzApp.Business.Wrappers
{
    public sealed class Result<T> : Result
    {
        public override bool IsSuccess { get; set; }

        public override string Message { get; set; }



        new public static Result<T> Success(string message = "")
        {
            return new Result<T>()
            {
                IsSuccess = true,
                Message = message
            };
        }


        new public static Result<T> Fail(string message)
        {
            return new Result<T>()
            {
                IsSuccess = false,
                Message = message
            };
        }
    }


    public class Result
    {
        public virtual bool IsSuccess { get; set; }

        public virtual string Message { get; set; }



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
