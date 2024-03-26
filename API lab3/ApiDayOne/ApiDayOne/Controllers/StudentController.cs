using ApiDayOne.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace ApiDayOne.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		private readonly StudentContext context;
		public StudentController(StudentContext contex)
		{
			this.context = contex;
		}
		[HttpGet]
		public ActionResult<List<Student>> GetAll()
		{
			return context.Students.ToList();
		}
		[HttpGet("{id:int}")]
		public IActionResult GetById(int id)
		{
			var std = context.Students.Find(id);
			if (std == null)
			{
				return NotFound();
			}
			else
				return Ok(std);
		}
		[HttpGet("{name:alpha}")]
		public IActionResult GetByName(string name)
		{
			var std = context.Students.Where(i => i.Name == name);
			if (std == null)
			{
				return NotFound();
			}
			else
				return Ok(std);
		}
		[HttpPost]
		public ActionResult Add(Student std)
		{
			if (ModelState.IsValid)
			{
				context.Students.Add(std);
				context.SaveChanges();
				return Created("success", std);
			}
			else
				return BadRequest();
		}
		[HttpPut]
		public IActionResult Update(Student std)
		{
			if (ModelState.IsValid)
			{
				context.Entry(std).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				context.SaveChanges();
				return NoContent();
			}
			else
				return BadRequest();
		}
		[HttpDelete]
		public IActionResult Delete(int id)
		{
			var std = context.Students.Find(id);
			if (std == null)
			{
				return NotFound();
			}
			else
			{
				context.Students.Remove(std);
				context.SaveChanges();
				return Ok(std);
			}
		}
	}
}
