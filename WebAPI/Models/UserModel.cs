using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using WebAPI.Data;

namespace WebAPI.Models
{
    public class UserModel
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }


    }

    
}
