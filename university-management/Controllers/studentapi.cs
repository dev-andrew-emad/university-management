using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using universitybusinesslayer.business;
using universitybusinesslayer.Dtos;

namespace university_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class studentapi : ControllerBase
    {
        private readonly studentbusiness _studentbusiness;
        public studentapi(studentbusiness studentbusiness)
        {
            _studentbusiness = studentbusiness;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<pagedresultdto<studentdto>>>getallstudents(int pagenumber,int pagesize)
        {
            var result=await _studentbusiness.getallstudents(pagenumber,pagesize);

            return Ok(result);
        }
        [Authorize(Roles ="admin")]
        [HttpGet("studentswithcourses")]
        public async Task<ActionResult<pagedresultdto<studentwithcoursesdto>>>getallstudentswithcourses(int pagenumber,int pagesize)
        {
            var result =await _studentbusiness.getallstudentswithcourses(pagenumber,pagesize);
            return Ok(result);
        }
        [Authorize(Roles ="admin")]
        [HttpGet("activestudents")]
        public async Task<ActionResult<pagedresultdto<studentdto>>>getactivestudents(int pagenumber,int pagesize)
        {
            var result = await _studentbusiness.getactivestudents(pagenumber, pagesize);
            return Ok(result);
        }
        [Authorize(Roles ="admin")]
        [HttpGet("deletedstudents")]
        public async Task<ActionResult<pagedresultdto<studentdto>>> getdeletedstudents(int pagenumber, int pagesize)
        {
            var result = await _studentbusiness.getdeletedstudents(pagenumber, pagesize);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<studentdto>>getstudentbyid(int id)
        {
            if(id<=0)
            {
                return BadRequest();
            }
            var student = await _studentbusiness.getstudentbyid(id);
            if(student!=null)
            {
                return Ok(student);
            }
            else
            {
                return NotFound();
            }
        }
        [Authorize(Policy ="addstudentpermission")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult>addnewstudent(newstudentdto newstudentdto)
        {
            if(string.IsNullOrEmpty(newstudentdto.name)||string.IsNullOrEmpty(newstudentdto.email)||newstudentdto.age<20)
            {
                return BadRequest();
            }
            var newstudent = await _studentbusiness.addnewstudent(newstudentdto);
            if(newstudent==null)
            {
                return BadRequest();
            }
            else
            {
                return CreatedAtAction(nameof(getstudentbyid), new { id = newstudent.id }, newstudent);
            }
        }
        [Authorize(Roles ="admin")]
        [HttpPut("updatestudent")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult>updatestudent(int id,newstudentdto newstudentdto)
        {
            if(id<=0||string.IsNullOrEmpty(newstudentdto.name)||string.IsNullOrEmpty(newstudentdto.email)||newstudentdto.age<20)
            {
                return BadRequest();
            }
            bool result = await _studentbusiness.updatestudent(id, newstudentdto);
            if(result==true)
            {
                return Ok("student updated successfully");
            }
            else
            {
                return BadRequest();
            }
        }
        [Authorize(Policy = "deletestudentpermission")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> deletestudent(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            bool result = await _studentbusiness.deletestudent(id);
            if(result==true)
            {
                return Ok("student deleted successfully");
            }
            else
            {
                return BadRequest();
            }
        }
        [Authorize(Roles ="admin")]
        [HttpPut("restorestudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult>restorestudent(int id)
        {
            if(id<=0)
            {
                return BadRequest();
            }
            bool result=await _studentbusiness.restorestudent(id);
            if(result==true)
            {
                return Ok("student restored successfully");
            }
            else
            {
                return BadRequest();
            }
           
        }
    }
}
