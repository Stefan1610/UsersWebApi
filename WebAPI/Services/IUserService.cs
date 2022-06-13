using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IUserService
    {
        public List<UserModel> getAllWithUslov(string kolone, string tabela, string uslov);
        public List<UserModel> getAll();
        public UserModel createUser(UserModel user);
        public void deleteUser(int id);

        public UserModel updateUser(UserModel user, int id);
    }
}
