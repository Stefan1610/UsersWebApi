using Microsoft.Data.SqlClient;
using System.Data;
using System.Dynamic;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Services
{   //Service for Users controller with all the logic being here
    public class UserService : IUserService
    {
        private UserRepository userRep;
        public UserService(UserRepository userRep)
        {
            this.userRep = userRep;
        }
        //updating user
        public UserModel updateUser(UserModel user, int id)
        {
            UserModel model = userRep.findById(id);
            if(model == null)
            {
                throw new Exception("User with that id doesn't exist");
            }
            model.UserName = user.UserName;
            model.Password = user.Password;
            model.Email = user.Email;
            model.FullName = user.FullName;
            model.PhoneNumber = user.PhoneNumber;
            UserModel u = userRep.updateUser(model, id);
            userRep.commit();
           return u;
        }
        //method for creating new user
        public UserModel createUser(UserModel user)
        {
            if(user == null)
            {
                throw new ArgumentNullException("please insert user");
            }
            try
            {
                UserModel us = userRep.createUser(user);
                userRep.commit();
                return us;
            }
            catch(Exception)
            {
                throw new Exception("You didn't create user");
            }
        }
        //method for deleting user by giving user id of the user.
        public void deleteUser(int id)
        {
            if (id < 0)
            {
                throw new Exception("Invalid id");
            }
            UserModel user = userRep.findById(id);
            if(user == null)
            {
                throw new Exception("User with that ID doesn't exist");
            }
            userRep.Delete(user);
            userRep.commit();
        }
        //returning all the users
        public List<UserModel> getAll()
        {
            List<UserModel> list = userRep.getAll();
            return list;
        } 
        public List<object> getDynamic(string kolone)
        {
            if(kolone == "all")
            {
                kolone = "userid,username,password,email,fullname,phonenumber";
            }
            DataTable dt = new DataTable();
            var list = new List<object>();
            SqlConnection conn = new SqlConnection("Data Source=172.20.115.35;Initial Catalog=CPCIT-SS;User ID=dotnetdev;Password=dotnetdev");
            conn.Open();
            string sqlQuery = "";
            sqlQuery = "Select " + kolone + " from [user]";
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            SqlDataReader reader = cmd.ExecuteReader();
            dynamic obj = new ExpandoObject();
            string[] str = kolone.Split(',');

            while (reader.Read())
            {  
                dynamic obj2 = new ExpandoObject();

                for (int i = 0; i < str.Length; i++) {
                    if (str[i].Contains("userid"))
                    {
                        obj.userid = reader["userid"];
                        obj2.userid = obj.userid;                        
                    }
                    if (str[i].Contains("username"))
                    {                       
                        obj.username = reader["username"];
                        obj2.username = obj.username;                        
                    }
                    if (str[i].Contains("password"))
                    {
                        obj.password = reader["password"];
                        obj2.password = obj.password;
                    }
                    if (str[i].Contains("email"))
                    {                       
                        obj.email = reader["email"];
                        obj2.email = obj.email;                        
                    }
                    if (str[i].Contains("fullname"))
                    {                        
                        obj.fullname = reader["fullname"];
                        obj2.fullname = obj.fullname;                        
                    }
                    if (str[i].Contains("phonenumber"))
                    {
                        obj.phonenumber = reader["phonenumber"];
                        obj2.phonenumber = obj.phonenumber;
                    }                    
                }
                list.Add(obj2);
            }      
            return list;
        }
        //method for getting all the users by giving columns, table and condition 
        public List<UserModel> getAllWithUslov(string kolone, string tabela, string uslov)
        {
            DataTable dt = new DataTable(); 
            if (kolone == "all")
            {
                kolone = "*";
            }
            if(tabela != "user" && tabela != "User")
            {
                throw new Exception("Table doesn't exist");
            }
          List<UserModel> list = new List<UserModel>();
          SqlConnection conn = new SqlConnection("Data Source=172.20.115.35;Initial Catalog=CPCIT-SS;User ID=dotnetdev;Password=dotnetdev");
            conn.Open();
            string sqlQuery = "";
            if (uslov == "no")
            {
                sqlQuery = "Select " + kolone + " from [" + tabela + "]";
            }
            else
            {
                sqlQuery = "Select " + kolone + " from [" + tabela + "] where " + uslov;
            }
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            SqlDataReader reader = cmd.ExecuteReader();
            string[] str = kolone.Split(',');           
            while (reader.Read())
                
            {
                UserModel user = new UserModel();
                for (int i = 0; i < str.Length; i++) {
                    
                    if (str[i].Contains("userid"))                  
                    {
                        user.UserID = Convert.ToInt32(reader[str[i]]);
                    }
                    if (str[i].Contains("username"))
                    {
                        user.UserName = reader["username"].ToString();
                    }
                    if (str[i].Contains("password"))
                    {
                        user.Password = reader["password"].ToString();
                    }
                    if (str[i].Contains("email"))
                    {
                        user.Email = reader["email"].ToString();
                    }
                    if (str[i].Contains("fullname"))
                    {
                        user.FullName = reader["fullname"].ToString();
                    }
                    if (str[i].Contains("phonenumber"))
                    {
                        user.PhoneNumber = Convert.ToInt32(reader["phonenumber"]);
                    }
                    if (str[i] == "*")
                    {
                        user.UserID = Convert.ToInt32(reader["userid"]);
                        user.UserName = reader["username"].ToString();
                        user.Password = reader["password"].ToString();
                        user.Email = reader["email"].ToString();
                        user.FullName = reader["fullname"].ToString();
                        user.PhoneNumber = Convert.ToInt32(reader["phonenumber"]);
                    }                    
                }
                list.Add(user);
            }   
            return list;
        }
    }
}
