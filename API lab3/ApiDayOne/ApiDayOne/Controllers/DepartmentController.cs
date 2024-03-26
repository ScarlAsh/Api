using ApiDayOne.DTOs;
using ApiDayOne.Filters;
using ApiDayOne.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace ApiDayOne.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class DepartmentController : ControllerBase
	{
		private readonly StudentContext context;
		public DepartmentController(StudentContext context)
		{
			this.context = context;
		}
		[HttpGet]
		public IActionResult GetAll()
		{
			var Depts = context.Departments.ToList();
			if (Depts == null)
				return BadRequest();
			List<DepartmentDTO> deptDTOs = new List<DepartmentDTO>();
			foreach (var item in Depts)
			{
				DepartmentDTO itemdto = new();
				itemdto.Id = item.Id;
				itemdto.Name = item.Name;
				itemdto.Location = item.Location;
				itemdto.OpenDate = item.Opendate;
				foreach (var std in item.Students)
				{
					itemdto.Students?.Add(std.Name);
				}
				deptDTOs.Add(itemdto);
			}
			return Ok(deptDTOs);
		}
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var Dept = context.Departments.Find(id);
			if (Dept == null)
				return BadRequest();
			DepartmentDTO DeptDTO = new DepartmentDTO()
			{
				Id = Dept.Id,
				Name = Dept.Name,
				Location = Dept.Location,
				OpenDate = Dept.Opendate
			};
			foreach (var item in Dept.Students)
			{
				DeptDTO.Students?.Add(item.Name);
			}
			return Ok(DeptDTO);
		}
		[HttpPost]
		[LocationFilter]
		public IActionResult Create(DepartmentDTO deptDTO)
		{
			if (deptDTO == null)
				return BadRequest();

			Department Dept = new Department();
			Dept.Name = deptDTO.Name;
			Dept.Location = deptDTO.Location;
			Dept.Opendate = deptDTO.OpenDate;
			context.Departments.Add(Dept);
			context.SaveChanges();
			return Created();
		}

		[HttpPut]
		public IActionResult Edit(DepartmentDTO deptDTO)
		{
			var dept = context.Departments.Find(deptDTO.Id);
			if (dept == null)
				return BadRequest();
			dept.Name = deptDTO.Name;
			dept.Location = deptDTO.Location;
			dept.Opendate = deptDTO.OpenDate;
			context.SaveChanges();
			return NoContent();
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id) 
		{
			var dept = context.Departments.Find(id);
			if(dept == null)
				return BadRequest();
			context.Departments.Remove(dept);
			context.SaveChanges();
			return Ok(dept);
		}

	}
}
