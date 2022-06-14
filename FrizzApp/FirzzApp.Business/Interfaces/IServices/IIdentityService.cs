using FirzzApp.Business.Auth;
using FirzzApp.Business.Dtos.RequestDto;
using System.Threading.Tasks;

namespace FirzzApp.Business.Interfaces.IServices
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(UserRegisterDto dto);
        Task<AuthenticationResult> LoginAsync(UserLoginDto dto);
    }
}
