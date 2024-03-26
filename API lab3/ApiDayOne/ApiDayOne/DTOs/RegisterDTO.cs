using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace ApiDayOne.DTOs
{
	public class RegisterDTO
	{
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

		[Compare("Password")]
		public string ConfirmPassword { get; set; }

	}
}
