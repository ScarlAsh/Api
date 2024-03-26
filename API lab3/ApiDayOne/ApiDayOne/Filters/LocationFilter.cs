using ApiDayOne.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;

namespace ApiDayOne.Filters
{
	public class LocationFilterAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			DepartmentDTO dept = context.ActionArguments["deptDTO"] as DepartmentDTO;
			if(dept == null || !Regex.IsMatch(dept.Location ,@"^(eg|usa)$"))
			{
				context.ModelState.AddModelError("loc","invalid location!");
				context.Result = new BadRequestObjectResult(context.ModelState);
			}
		}
	}
}
