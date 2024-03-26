using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ApiDayOne.Models
{
	public class Department
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Location { get; set; }
		public DateOnly Opendate { get; set; }

		public virtual ICollection<Student> Students { get; set; } = new List<Student>();
	}
	

}
