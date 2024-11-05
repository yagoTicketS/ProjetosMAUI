using AppTask.Models;
using Microsoft.EntityFrameworkCore;

public class AppTaskContext : DbContext
{
	public AppTaskContext(DbContextOptions<AppTaskContext> options) : base(options)
	{
		Database.Migrate();
		ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
	}

	//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	//{
	//	if (!optionsBuilder.IsConfigured) // Para evitar configuração duplicada
	//	{
	//		var databasePath = Path.Combine(
	//			Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
	//			"apptask.db");

	//		optionsBuilder.UseSqlite($"Filename={databasePath}");
	//	}
	//}

	public DbSet<TaskModel> Tasks { get; set; }
	public DbSet<SubTaskModel> SubTasks { get; set; }
}
