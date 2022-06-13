using WebAPI.Models;

namespace WebAPI.Data
{
    public class UserData
    {
        public UserModel User { get; set; }
        public string Token { get; set; }
        public IEnumerable<string> Errors { get; set; } 
    }
}
