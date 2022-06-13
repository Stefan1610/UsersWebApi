using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IAuthService
    {
        public UserData Login(LoginModel login);
        public UserData Register(RegistrationModel model);
    }
}
