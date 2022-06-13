using System.Linq.Expressions;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class UserRepository
    {
        private UserDBContext _dbContext;

        public UserRepository(UserDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Delete(UserModel user)
        {
            _dbContext.User.Remove(user);
        }
        public List<UserModel> getAll()
        {
            return _dbContext.User.ToList();
        }
        public UserModel updateUser(UserModel user, int id)
        {
            UserModel model = _dbContext.User.Find(id);
            model.UserName = user.UserName;
            model.Password = user.Password;
            model.Email = user.Email;
            model.FullName = user.FullName;
            model.PhoneNumber = user.PhoneNumber;
            return model;
        }
        public UserModel findById(int id)
        {
            return _dbContext.User.SingleOrDefault(us => us.UserID == id);
        }
        public UserModel createUser(UserModel user)
        {
            if(_dbContext.User.Add(user) == null)
            {
                return null;
            }
            return user;
        }
        public UserModel FindOne(Expression<Func<UserModel, bool>> func)
        {
            return _dbContext.User.SingleOrDefault(func);
        }
        public void commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
