using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableQuery]
    //Controller for users
    public class UsersController : ODataController
    {
        private readonly UserService userService;
        public UsersController(UserService userService)
        {
            this.userService = userService;
        }
        [HttpGet("{column}")]
        public IActionResult getDyn([FromRoute] string column)
        {
            object dyn = userService.getDynamic(column);
            
            return Ok(dyn);
        }
        
        [HttpGet]
        //getting all the users without condition and all the columns
        public IActionResult getAll()
        {
            dynamic userModels = userService.getAll();            
            return Ok(userModels);
        }        
        [HttpGet("{column}/{table}/{condition}")]
        //Getting all the users with specific columns, table name and specific condition
        public IQueryable getAllWithUslov([FromRoute]string column,[FromRoute] string table,[FromRoute] string condition)
        {
            try
            {
                List<UserModel> users = userService.getAllWithUslov(column, table, condition);
                return users.AsQueryable();   
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        //Method for creating user
        public IActionResult CreateUser(UserModel user)
        {         
            UserModel model = new UserModel
            {
                UserID = user.UserID,
                UserName = user.UserName,
                Password = user.Password,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            try
            {
                UserModel u = userService.createUser(model);                  
                return Ok(u);                
            }
            catch (Exception)
            {
                throw new Exception("Doesn't work");
            }           
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //Method for deleting users by their id.
        public IActionResult DeleteUser(int id)
        {
            try
            {
                userService.deleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }       
    }
}
