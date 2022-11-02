using Connectivity_Pro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Connectivity_Pro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase 
    {
        private readonly User_Context mycontext;

        public UserController(User_Context mycontext)
        {
            this.mycontext = mycontext;
        }


        [HttpGet("/{email}")]
        public ActionResult<Users> GetUserByEmail(string email)
        {
            var data = this.mycontext.users.Where(x => x.Email == email).FirstOrDefault();
            if(data == null)
            {
                return NotFound("The requested data is not found");
            }
            return data;
        }

        [HttpGet]
        public List<Users> GetAllUser()
        {
            var data = this.mycontext.users.ToList();
            return data;
        }

        [HttpPost]
        public ActionResult<string> CreateUser(Users obj)
        {
            var email=obj.Email;
            if(!IsValidEmail(email))
            {
                return BadRequest("Incorrect pattern");
            }
            
            var firstname = obj.Firstname;
            if (IsValidName(firstname))
            {
                return BadRequest("Name should not contain any whitespace");
            }
            else
            {
                this.mycontext.Add(obj);
                this.mycontext.SaveChanges();
                return Ok("Success");
            }
        }


        [HttpPut]
        public ActionResult<string> UpdateUser(Users obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.Email))
            {
                return BadRequest("Invalid Request");
            }

            var email = obj.Email;
            if (!IsValidEmail(email))
            {
                return BadRequest("Incorrect email pattern");
            }

            var data = this.mycontext.users.Find(obj.Email);
            if (data == null)
            {
                return NotFound("The requested data was not found");
            }

            var firstname = obj.Firstname;
            if (IsValidName(firstname))
            {
                return BadRequest("Name should not contain any whitespace");
            }
            data.Firstname = obj.Firstname;
            data.Lastname = obj.Lastname;
            data.Email = obj.Email;
            data.Password = obj.Password;
            data.DOB = obj.DOB;
            data.Gender = obj.Gender;
            this.mycontext.SaveChanges();
            return Ok("Success");
        }

        [HttpDelete]
        public ActionResult<string> RemoveUser(string Email)
        {
            var data = this.mycontext.users.Find(Email);
            if (data == null)
            {
                return NotFound("The requested resource was not found.");
            }
            this.mycontext.users.Remove(data);
            this.mycontext.SaveChanges();
            return Ok("Success");
        }

        private bool IsValidName(string firstname)
        {
                return firstname.Any(x=>Char.IsWhiteSpace(x));
        }

        public static bool IsValidEmail(string email)
        {
            Regex emailregex =new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",RegexOptions.IgnoreCase);
            return emailregex.IsMatch(email);
        }
    }


}

