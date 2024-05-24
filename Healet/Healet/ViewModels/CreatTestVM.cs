using System.ComponentModel.DataAnnotations;

namespace Healet.ViewModels
{
	public class CreatTestVM
	{
		[Required, MaxLength(25), MinLength(3)]
		public string Name { get; set; }

		[Required, MinLength(3)]
		public string Description { get; set; }

		[Required, MinLength(3)]
		public string ImageUrl { get; set; }
	}
}
