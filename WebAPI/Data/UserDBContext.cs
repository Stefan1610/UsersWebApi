using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebAPI.Models;

namespace WebAPI.Data
{ //odata i da bira kolone
    //grpc ovo isto
    public class UserDBContext : DbContext 
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=172.20.115.35;Initial Catalog=CPCIT-SS;User ID=dotnetdev;Password=dotnetdev");
        }
        public DbSet<UserModel> User { get; set; }
        
    }
}
