using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pojistenci.Models
{
	public class Pojistenec
	{
		public int Id { get; set; }
		[Display(Name = "Jméno")]
		public string? FirstName { get; set; }
		[Display(Name = "Příjmení")]
		public string? Surname { get; set; }
		[Display(Name = "Věk")]
		public int Vek { get; set; }
		[DataType(DataType.Date)]
		[Display(Name = "Registrován")]
		public DateTime Registrovan { get; set; }
		
		public string? Kraj { get; set; }
		
		public int TypPojisteniId { get; set; }

        [ForeignKey("TypPojisteniId")]
        public virtual TypPojisteni? TypPojisteni { get; set; }
		

	}
				






}
