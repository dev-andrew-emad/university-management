using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using universitybusinesslayer.business;
using universitybusinesslayer.Dtos;

namespace university_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userapi : ControllerBase
    {
        private readonly userbusiness _userbusiness;
        public userapi(userbusiness userbusiness)
        {
            _userbusiness = userbusiness;
        }
        [HttpPost]
        public async Task<ActionResult>login(logindto login)
        {
            var token = await _userbusiness.login(login);
            if(token == null)
            {
                return BadRequest("wrong email or password");
            }
            else
            {
                return Ok(token);
            }
        }
        [Authorize(Policy ="addnewuserpermission")]
        [HttpPost("addnewuser")]
        public async Task<ActionResult<userdto>>addnewuser(newuserdto newuserdto)
        {
            if(string.IsNullOrEmpty(newuserdto.email)||string.IsNullOrEmpty(newuserdto.password)||string.IsNullOrEmpty(newuserdto.role))
            {
                return BadRequest();
            }
            var user=await _userbusiness.addnewuser(newuserdto);
            if(user == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(user);
            }
        }
        [Authorize(Roles ="admin")]
        [HttpPut("updateuser")]
        public async Task<ActionResult>updateuser(int userid,updateuserdto updateuserdto)
        {
            if(string.IsNullOrEmpty(updateuserdto.email)||string.IsNullOrEmpty(updateuserdto.role))
            {
                return BadRequest();
            }
            bool result = await _userbusiness.updateuser(userid,updateuserdto);
            if(!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok("user updatted successfully");
            }
        }
        [Authorize(Roles ="admin")]
        [HttpPut("changepassword")]
        public async Task<ActionResult>changepassword(int userid,changepassworddto changepassworddto)
        {
            if(userid<=0||string.IsNullOrEmpty(changepassworddto.oldpassword)||string.IsNullOrEmpty(changepassworddto.newpassword))
            {
                return BadRequest();
            }
            bool result=await _userbusiness.changepassword(userid,changepassworddto);
            if(!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok("password changed successfully");
            }
        }
        [Authorize(Policy = "deleteuserpermission")]
        [HttpDelete]
        public async Task<ActionResult>deleteuser(int userid)
        {
            if(userid<=0)
            {
                return BadRequest();
            }
            bool result=await _userbusiness.deleteuser(userid);
            if(!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok("user deleted successfully");
            }
        }
    }
}
