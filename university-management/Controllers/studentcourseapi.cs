using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using universitybusinesslayer.business;
using universitybusinesslayer.Dtos;

namespace university_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class studentcourseapi : ControllerBase
    {
        private readonly studentcoursebusiness _studentcoursebusiness;
        public studentcourseapi(studentcoursebusiness studentcoursebusiness)
        {
            _studentcoursebusiness = studentcoursebusiness;
        }
        [Authorize(Roles ="admin")]
        [HttpPost("enroll")]
        public async Task<ActionResult>enrollstudent(studentcoursedto studentcoursedto)
        {
            await _studentcoursebusiness.enrollstudent(studentcoursedto);
            return Ok("student enrolled sucessfully");
        }
    }
}
