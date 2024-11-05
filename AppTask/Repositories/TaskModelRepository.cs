using AppTask.Models;
using Microsoft.EntityFrameworkCore;

namespace AppTask.Repositories
{
	public class TaskModelRepository : ITaskModelRepository
	{
		private AppTaskContext _db;
		public TaskModelRepository(AppTaskContext db)
		{
			_db = db;
		}
		public IList<TaskModel> GetAll()
		{
			return _db.Tasks.OrderByDescending(a=>a.PrevisionDate).ToList();
		}

		public TaskModel GetById(int id)
		{
			return _db.Tasks.Include(a => a.SubTasks).FirstOrDefault(a => a.Id == id);
		}

		public void Add(TaskModel task)
		{
			_db.Tasks.Add(task);
			_db.SaveChanges();
		}
		public void Update(TaskModel task)
		{
			var trackedEntity = _db.ChangeTracker.Entries<TaskModel>()
						   .FirstOrDefault(e => e.Entity.Id == task.Id);

			if (trackedEntity != null)
			{
				// Se a entidade já está rastreada, atualize diretamente
				trackedEntity.State = EntityState.Detached;
			}

			_db.Update(task);
			_db.SaveChanges();
		}

		public void Delete(TaskModel task)
		{
			// Desanexa qualquer entidade existente com o mesmo ID
			var existingEntity = _db.ChangeTracker.Entries<TaskModel>()
							.FirstOrDefault(e => e.Entity.Id == task.Id);

			if (existingEntity != null)
			{
				existingEntity.State = EntityState.Detached;
			}

			// Agora, remove a entidade

			task = GetById(task.Id);
			//REMOVER SUBTAREFAS PRIMEIRO
			foreach (var subtask in task.SubTasks)
			{
				_db.SubTasks.Remove(subtask);
			}

			_db.Tasks.Remove(task);
			_db.SaveChanges();
		}

	}
}
