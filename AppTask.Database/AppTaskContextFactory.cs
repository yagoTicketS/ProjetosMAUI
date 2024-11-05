using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class AppTaskContextFactory : IDesignTimeDbContextFactory<AppTaskContext>
{
	public AppTaskContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<AppTaskContext>();

		var databasePath = Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
			"apptask.db");

		optionsBuilder.UseSqlite($"Filename={databasePath}");

		return new AppTaskContext(optionsBuilder.Options);
	}
}
