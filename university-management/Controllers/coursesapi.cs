using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using universitybusinesslayer.business;
using universitybusinesslayer.Dtos;

namespace university_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class coursesapi : ControllerBase
    {
        private readonly coursebusiness _coursebusiness;
        public coursesapi(coursebusiness coursebusiness)
        {
            _coursebusiness = coursebusiness;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<coursedto>>>getallcourses()
        {
            var courseslist=await _coursebusiness.getallcourses();
            return Ok(courseslist);
        }
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<coursedto>>getcoursebyid(int id)
        {
            var course = await _coursebusiness.getcoursebyid(id);
            if(course!=null)
            {
                return Ok(course);
            }
            else
            {
                return NotFound();
            }
        }
        [Authorize(Roles ="admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult>addnewcourse(newcoursedto newcoursedto)
        {
            if(string.IsNullOrEmpty(newcoursedto.name)||newcoursedto.hours<=0||newcoursedto.maxstudents<=0)
            {
                return BadRequest();
            }
            var newcourse = await _coursebusiness.addnewcourse(newcoursedto);
            if(newcourse==null)
            {
                return BadRequest();
            }
            else
            {
                return CreatedAtAction(nameof(getcoursebyid), new { id = newcourse.id }, newcourse);
            }
            
        }
        [Authorize(Roles ="admin")]
        [HttpGet("coursewithstudentscount")]
        public async Task<ActionResult<IEnumerable<coursewithstudentscountdto>>>getcoursewithstudentscount()
        {
            var courselist = await _coursebusiness.getcoursewithstudentscount();
            return Ok(courselist);
        }
        [Authorize(Roles ="admin")]
        [HttpPut("assigninstructor")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult>assigninstructor(int courseid,int instructorid)
        {
            if(courseid<=0||instructorid<=0)
            {
                return BadRequest();
            }
            bool result=await _coursebusiness.assigninstructor(courseid, instructorid);
            if(result==true)
            {
                return Ok("instructor assigned successfully");
            }
            else
            {
                return BadRequest();
            }
        }
        [Authorize(Roles ="admin")]
        [HttpPut("updatecourse")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult>updatecourse(int id,newcoursedto newcoursedto)
        {
            if(id<=0||string.IsNullOrEmpty(newcoursedto.name)||newcoursedto.hours<=0||newcoursedto.maxstudents<=0)
            {
                return BadRequest();
            }
            bool result=await _coursebusiness.updatecourse(id,newcoursedto);
            if(result==true)
            {
                return Ok("course updated successfully");
            }
            else
            {
                return BadRequest();
            }

        }
        [Authorize(Roles ="admin")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult>deletecourse(int id)
        {
            if(id<=0)
            {
                return BadRequest();
            }
            bool result=await _coursebusiness.deletecourse(id);

            if (result == true)
                return Ok("course deleted successfully");
            else
                return BadRequest();
        }
    }
}
