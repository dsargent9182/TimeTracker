using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracker.Models
{
	public class ApplicationUser : IdentityUser
	{
		[MaxLength(256)]
		[Column(TypeName = "varchar")]
		public string FirstName { get; set; }

		[MaxLength(256)]
		[Column(TypeName="varchar")]
		public string LastName { get; set; }

	}
}
