using Microsoft.AspNetCore.Mvc.Rendering;
using Pojistenci.Models;

namespace Pojistenci.Models
{ 
	public class PojistenecViewModel
	{
		public int Id { get; set; }
		public Pojistenec? Pojistenec { get; set; }
		public IEnumerable<SelectListItem>? TypeDropDownPojisteni { get; set; }
		
	}
}
