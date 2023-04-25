using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pojistenci.Models
{
	public class PojistenecPojisteniViewModel
	{
		public List<Pojistenec>? Pojistenci { get; set; }
		public SelectList? TypyPojisteni { get; set; }
		public string? TypPojisteni { get; set; }
	}
}
