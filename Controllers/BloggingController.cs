using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrapecityAssignment.Model;
using GrapecityAssignment.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrapecityAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BloggingController : ControllerBase
    {
        Blogging blog = new Blogging();

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result =  blog.GetAllBlogs();
                return Ok(result);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] PostModel value)
        {
            try
            {
                var result = blog.PostBlogs(value);
                return Ok(result);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
