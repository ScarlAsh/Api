using ApiDayOne.Annotations;
using System.Text.Json.Serialization;

namespace ApiDayOne.DTOs
{
	public class DepartmentDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Location { get; set; }

		[OpenDateAnnotation]
		public DateOnly OpenDate { get; set; }

		public List<string>? Students { get; set; } = new List<string>();


	}
}
