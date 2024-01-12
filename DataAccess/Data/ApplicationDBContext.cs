using ITReviewer.Libraries.Models;
using Microsoft.EntityFrameworkCore;

namespace ITReviewer.Libraries.DataAccess.Data
{
	public class ApplicationDBContext : DbContext
	{
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
		{
				
		}
		public DbSet<User> Users { get; set; }
		public DbSet<Company> Companies { get; set; }
	//	public DbSet<Rating> Rating { get; set; }
		public DbSet<Review> Reviews { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder) 
		{
			modelBuilder.Entity<User>().Property(u => u.RegDate).HasDefaultValueSql("GETDATE()");
			modelBuilder.Entity<Company>().Property(u => u.RegDate).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Review>().Property(u => u.RegDate).HasDefaultValueSql("GETDATE()");
			
			modelBuilder.Entity<User>().HasData(
				new User 
				{	Id = 1, 
					Name = "Admin", 
					Email = "vladrampage53@gmail.com", 
					Password = "deec6407dbcbf0bfd2f95788c0eaf7e011857dfbb7e575462f9a0ed8c270e454", 
					Role = Role.Admin 
				},
				new User 
				{	Id = 2, 
					Name = "berserk", 
					Email = "vladyslav.kukhar99@gmail.com", 
					Password = "604168406c8e775b0d97e792c966148a898cdaf3893381e6b77be85b20581e5e", 
					Role = Role.Admin 
				}
			);


		}
    }
}
