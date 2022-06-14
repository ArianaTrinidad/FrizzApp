using System.Collections.Generic;

namespace FirzzApp.Business.Auth
{
    public class AuthFailureResponse
    {
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
