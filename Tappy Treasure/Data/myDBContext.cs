using Microsoft.EntityFrameworkCore;
using Tappy_Treasure.Models;

namespace Tappy_Treasure.Data
{
	public class myDBContext : DbContext
	{
		public myDBContext(DbContextOptions<myDBContext> options) : base(options) { }

		public DbSet<Contact> Contacts { get; set; }

		// No need to modify OnConfiguring anymore
	}
}

//optionsBuilder.UseSqlServer("Server=MSI\\SQLEXPRESS;Database=Tappy_Treasure;Trusted_Connection=True;");