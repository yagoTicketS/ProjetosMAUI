using AppTask.Models;

namespace AppTask.Repositories
{
	public interface ITaskModelRepository
	{
		//CRUD
		IList<TaskModel> GetAll();
		TaskModel GetById(int id);
		void Add(TaskModel task);
		void Update(TaskModel task);
		void Delete(TaskModel task);
	}
}
