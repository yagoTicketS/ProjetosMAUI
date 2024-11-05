using AppTask.Models;
using AppTask.Repositories;
using System.Text;

namespace AppTask.Views;

public partial class AddEditTaskPage : ContentPage
{
	private ITaskModelRepository _repository;
	private TaskModel _task;
	public AddEditTaskPage()
	{
		InitializeComponent();

		_task = new TaskModel();
		var dbContext = new AppTaskContextFactory().CreateDbContext(Array.Empty<string>());
		_repository = new TaskModelRepository(dbContext);

		BindableLayout.SetItemsSource(BindableLayout_Steps, _task.SubTasks);
	}

	public AddEditTaskPage(TaskModel task)
	{

		var dbContext = new AppTaskContextFactory().CreateDbContext(Array.Empty<string>());
		_repository = new TaskModelRepository(dbContext);

		InitializeComponent();

		_task = task;

		FillFields();
		
		BindableLayout.SetItemsSource(BindableLayout_Steps, _task.SubTasks);
	}

	private void FillFields()
	{
		Entry_TaskName.Text = _task.Name;
		Editor_TaskDescriptions.Text = _task.Description;
		DatePicker_TaskDate.Date = _task.PrevisionDate;
	}

	private void CloseModal(object sender, EventArgs e)
	{
		Navigation.PopModalAsync();
	}

	private void Save(object sender, EventArgs e)
	{
		GetDataFromForm();

		bool valid = ValidateData();

		if (valid)
		{
			SaveInDataBase();

			Navigation.PopModalAsync();

			UpdateListInStartPage();
		}
	}

	private void UpdateListInStartPage()
	{
		var navPage = (NavigationPage)App.Current.MainPage;
		var startPage = (StartPage)navPage.CurrentPage;
		startPage.LoadData();

	}

	private void SaveInDataBase()
	{
		if (_task.Id == 0)
			_repository.Add(_task);
		else
			_repository.Update(_task);
	}

	private void GetDataFromForm()
	{
		_task.Name = Entry_TaskName.Text;
		_task.Description = Editor_TaskDescriptions.Text;
		_task.PrevisionDate = DatePicker_TaskDate.Date;
		_task.PrevisionDate = _task.PrevisionDate.AddHours(23);
		_task.PrevisionDate = _task.PrevisionDate.AddMinutes(59);
		_task.PrevisionDate = _task.PrevisionDate.AddSeconds(59);

		_task.Created = DateTime.Now;
		_task.IsCompleted = false;
	}

	private bool ValidateData()
	{
		Label_TaskName_Required.IsVisible = false;
		Label_TaskName_Required.IsVisible = false;

		bool validResult = true;

		if (string.IsNullOrWhiteSpace(_task.Name))
		{
			Label_TaskName_Required.IsVisible = true;
			validResult = false;
		}

		if (string.IsNullOrWhiteSpace(_task.Description))
		{
			Label_TaskDescription_Required.IsVisible = true;
			validResult = false;
		}

		return validResult;
	}
	private  async void AddStep(object sender, EventArgs e)
	{
		var stepName = await DisplayPromptAsync("Etapa", "Digite o nome da etapa(subtarefa):", "Adicionar", "Cancelar");

		if (!string.IsNullOrWhiteSpace(stepName))
		{
			_task.SubTasks.Add(new SubTaskModel { Name = stepName, IsCompleted = false });
		}
	}

	protected override void OnSizeAllocated(double width, double height)
	{
		base.OnSizeAllocated(width, height);

		DatePicker_TaskDate.WidthRequest = width - 40;
	}

	private void Remove_TaskOnClicked(object sender, TappedEventArgs e)
	{
		_task.SubTasks.Remove((SubTaskModel)e.Parameter);
	}
}
