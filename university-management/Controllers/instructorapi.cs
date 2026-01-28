using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using universitybusinesslayer.business;
using universitybusinesslayer.Dtos;

namespace university_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class instructorapi : ControllerBase
    {
        private readonly instructorbusiness _instructorbusiness;
        public instructorapi(instructorbusiness instructorbusiness)
        {
            _instructorbusiness = instructorbusiness;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<instructordto>>>getallinstructors()
        {
            var result=await _instructorbusiness.getallinstructors();
            return Ok(result);
        }
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<instructordto>>getinstructorbyid(int id)
        {
            if(id<=0)
            {
                return BadRequest();
            }
            var instructor = await _instructorbusiness.getinstructorbyid(id);
            return Ok(instructor);
        }
        [Authorize(Roles ="admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult>addnewinstructor(newinstructordto newinstructordto)
        {
            if(string.IsNullOrEmpty(newinstructordto.name)||string.IsNullOrEmpty(newinstructordto.email))
            {
                return BadRequest();
            }
            var instructor = await _instructorbusiness.addnewinstructor(newinstructordto);
            if(instructor==null)
            {
                return BadRequest();
            }
            else
            {
                return CreatedAtAction(nameof(getinstructorbyid), new { id = instructor.id }, instructor);
            }
        }
        [Authorize(Roles ="admin")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult>updateinstructor(int id,newinstructordto newinstructordto)
        {
            if(id<=0||string.IsNullOrEmpty(newinstructordto.name)||string.IsNullOrEmpty(newinstructordto.email))
            {
                return BadRequest();
            }
            bool result = await _instructorbusiness.updateinstructor(id, newinstructordto);
            if(result==true)
            {
                return Ok("instructor updated successfully");
            }
            else
            {
                return BadRequest();
            }
        }
        [Authorize(Roles ="admin")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult>deleteinstructor(int id)
        {
            if(id<=0)
            {
                return BadRequest();
            }
            bool result=await _instructorbusiness.deleteinstructor(id);
            if(result==true)
            {
                return Ok("instructor deleted successfully");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
