using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User_Service.Data;
using User_Service.Data.Entities;
using User_Service.Web.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace User_Service.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EventBusSend eventbusSend;

        public UserController(ApplicationDbContext context, EventBusReceive cake)
        {
            _context = context;
            eventbusSend = new EventBusSend();
        }


        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<User> GetUsers()
        {

            return StatusCode(200, _context.User);
        }

        // GET api/<UserController>/2ef23f2f-23fr2fe2-4cf2f2f
        [HttpGet("{id}")]
        public ActionResult<User> Get(Guid id)
        {
            return StatusCode(200, _context.User.Find(id));
        }

        // POST api/<UserController>
        [HttpPost]
        public ActionResult<User> Post([FromBody] UserDto userdto)
        {
            _context.User.Add(userdto.ToEntity());
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
                //return StatusCode(500);

            }
            return StatusCode(201, User);
        }

        // TODO: implement patch service
        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] UserDto value)
        {
            try
            {
                //TODO update user

                
                UserDto sendUser = new UserDto
                {
                    Name = value.Name,
                    Surname = value.Surname,
                    Email = value.Email
                };

                //Call eventbus
                eventbusSend.SendUser(sendUser);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // TODO: implement delete sequence using rabitmq.
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
        
    }
}
