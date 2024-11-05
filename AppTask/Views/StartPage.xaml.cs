using AppTask.Models;
using AppTask.Repositories;

namespace AppTask.Views;

public partial class StartPage : ContentPage
{
	private ITaskModelRepository _repository;
	private IList<TaskModel> _tasks;
	public StartPage()
	{
		InitializeComponent();

		var dbContext = new AppTaskContextFactory().CreateDbContext(Array.Empty<string>());
		_repository = new TaskModelRepository(dbContext);

		LoadData();
	}

	public void LoadData()
	{
		_tasks = _repository.GetAll();
		CollectionViewTasks.ItemsSource = _tasks;
		LblEmptyText.IsVisible = _tasks.Count <= 0;
	}

	private void Button_Clicked(object sender, EventArgs e)
	{
		//_repository.Add(new TaskModel
		//{
		//	Name = "Comprar Frutas",
		//	Description = "Comprar abacate e laranja",
		//	IsCompleted = false,
		//	Created = DateTime.Now,
		//	PrevisionDate = DateTime.Now.AddDays(2),
		//});

		LoadData();
		Navigation.PushModalAsync(new AddEditTaskPage());
	}

	private void OnBorderClickedToFocusEntry(object sender, TappedEventArgs e)
	{
		Entry_Search.Focus();
    }

	private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
	{
		var task = (TaskModel)e.Parameter;

		var confirm = await DisplayAlert("Confirme a exclus�o", $"Tem certeza que deseja excluir essa tarefa: {task.Name}?", "Sim", "N�o");

		if (confirm)
		{
			_repository.Delete(task);
			LoadData();
		}
	}

	private void OnCheckBoxClickedToComplete(object sender, TappedEventArgs e)
	{
		var checkbox = ((CheckBox)sender);
		var task = (TaskModel)e.Parameter;

		if (DeviceInfo.Platform != DevicePlatform.WinUI)
			checkbox.IsChecked = !checkbox.IsChecked;

		task.IsCompleted = checkbox.IsChecked;
		_repository.Update(task);
	}

	private void OnTapToEdit(object sender, TappedEventArgs e)
	{
		var task = (TaskModel)e.Parameter;
		Navigation.PushModalAsync(new AddEditTaskPage(task));
    }

	private void OnTextChanged_FilterList(object sender, TextChangedEventArgs e)
	{
		var word = e.NewTextValue;
		CollectionViewTasks.ItemsSource = _tasks.Where(a => a.Name.ToLower().Contains(word.ToLower()));
	}
}