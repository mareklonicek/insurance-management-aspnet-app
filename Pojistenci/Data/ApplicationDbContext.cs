using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pojistenci.Models;

namespace Pojistenci.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{

		}

			public DbSet<Pojistenec>? Pojistenec { get; set; }

		public DbSet<TypPojisteni>? TypPojisteni { get; set; }

		
			
	}
}