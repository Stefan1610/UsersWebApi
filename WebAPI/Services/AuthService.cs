using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Helpers;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Repository;
    
namespace WebAPI.Services
{   //Login and Registration services.
    public class AuthService : IAuthService
    {
        private UserRepository userRep;
        private string jwtSecret;
        public AuthService(UserRepository userRep, string jwtSecret)
        {
            this.userRep = userRep;
            this.jwtSecret = jwtSecret;
        }
        //login method which gives back errors if anything is not good
        public UserData Login(LoginModel model)
        {
            UserModel user = userRep.FindOne(us => us.UserName == model.Username);
            if (user == null)
            {
                return new UserData { Errors = new[] { "Username not found." } };
            }
            if (!VerifyPassword(model.Password, user.Password))
            {
                return new UserData { Errors = new[] { "Wrong password." } };
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),

                }),
                Expires = DateTime.Now.AddYears(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return new UserData
            {
                Token = token,
                User = user
            };
        }
        // Registration model, you cant register Username that already exists
        public UserData Register(RegistrationModel model)
        {
            if( userRep.FindOne(us => us.UserName == model.Username) != null)
            {
                return new UserData() { Errors = new[] { "User already exist" } };
            }
            UserModel user = new UserModel()
            {
                UserID = 0,
                UserName = model.Username,
                Password = model.Password,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FullName = model.FullName
            };
            userRep.createUser(user);
            userRep.commit();
            return new UserData { User = user };       
        }
        public bool VerifyPassword(string modelPassword, string userPassword)
            {
                return modelPassword == userPassword;
            }
        }
    }
