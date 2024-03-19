using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CurdOperationUsingAngular.Models;
using CurdOperationUsingAngular.DataAccessLayer;
using System.Web.Http.Cors;

namespace CurdOperationUsingAngular.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {
        UserDAL userDAL = new UserDAL();


        //Get all User Api
        [HttpGet]
        public List<UserTable> Get()
        {
            var allUsers = userDAL.GetAllUsers();
            return allUsers;
        }


        //Get User by Id Api
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            if (id == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            }
            var result = userDAL.GetUserById(id);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal Server Error");
        }


        //Create User Api
        [HttpPost]
        public HttpResponseMessage CreateUser([FromBody] UserTable user)
        {
            if(user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            }
            var result = userDAL.AddUser(user);
            if(result == true)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "User Added");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal Server Error");
        }


        //Edit User Api
        [HttpPut]
        public HttpResponseMessage EditUser(int id, UserTable user)
        {
            if (user == null || id == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            }
            var result = userDAL.UpdateUser(id, user);
            if (result == true)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "User Updated");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal Server Error");
        }

        
        //Delete User Api
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            if (id == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            }
            var result = userDAL.DeleteUser(id);
            if (result == true)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "User Deleted");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal Server Error");
        }
    }
}
