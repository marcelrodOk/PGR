using Microsoft.EntityFrameworkCore;
using ServiceLoteClosure.Models.Entities;

namespace ServiceLoteClosure.Data
{
	public class LoteClosureDbContext : DbContext
	{
		public LoteClosureDbContext(DbContextOptions options) : base(options)
		{

		}
		public DbSet<TablaIntermedia> TablaIntermedia { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			//ModelConfig(builder);
		}
		//private void ModelConfig(ModelBuilder modelBuilder)
		//{
		//	new TblIntermediaConfiguration(modelBuilder.Entity<TablaIntermedia>());
		//}
	}
}
