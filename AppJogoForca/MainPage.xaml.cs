using AppJogoForca.Libraries.Text;
using AppJogoForca.Models;
using AppJogoForca.Resources;

namespace AppJogoForca
{
	public partial class MainPage : ContentPage
	{
		int count = 0;
		private Word _word;
		private int _errors;
		public MainPage()
		{
			InitializeComponent();

			ResetScreen();

		}

		private async void OnButtonClicked(object sender, EventArgs e)
		{

			Button button = (Button)sender;
			button.IsEnabled = false;

			String letter = button.Text;

			var positions = _word.Text.GetPosicions(letter);

			if (positions.Count == 0)
			{
				ErrorHandler(button);
				await VerifyErrors();
				return;
			}

			ReplaceLettler(letter, positions);
			button.Style = App.Current.Resources.MergedDictionaries.ElementAt(1)["Sucess"] as Style;

			HasWinner();
		}

		private void ReplaceLettler(string letter, List<int> positions)
		{
			foreach (int position in positions)
			{
				lblText.Text = lblText.Text.Remove(position, 1).Insert(position, letter);
			}
		}

		#region VERIFY GAME STATE
		private async void HasWinner()
		{
			if (!lblText.Text.Contains("_"))
			{
				await DisplayAlert("Ganhou", "Você achou a palavra!!", "Novo jogo");
				ResetScreen();
			}
		}

		private void ErrorHandler(Button button)
		{
			_errors++;

			ImgMain.Source = ImageSource.FromFile($"forca{_errors + 1}.png");
			button.Style = App.Current.Resources.MergedDictionaries.ElementAt(1)["Fail"] as Style;
		}

		private async Task VerifyErrors()
		{
			if (_errors == 6)
			{
				await DisplayAlert("Perdeu", "Você esgotou suas tentativas", "Novo jogo");
				ResetScreen();
			}
		}
		#endregion
		#region RESETSCREEN
		private void ResetScreen()
		{

			ResetVirtualKeyboard();

			ResetErrors();

			SortWords();
		}
		private void SortWords()
		{
			var repository = new WordRepositories();
			_word = repository.GetRandomWord();

			lblTips.Text = _word.Tips;
			lblText.Text = new string('_', _word.Text.Length);
		}
		private void ResetErrors()
		{
			_errors = 0;
			ImgMain.Source = ImageSource.FromFile($"forca1.png");
		}

		private void ResetVirtualKeyboard()
		{

			ResetVirtualLines((HorizontalStackLayout)KeyboardContainer.Children[0]);
			ResetVirtualLines((HorizontalStackLayout)KeyboardContainer.Children[1]);
			ResetVirtualLines((HorizontalStackLayout)KeyboardContainer.Children[2]);
		}

		private void ResetVirtualLines(HorizontalStackLayout horizontal)
		{
			foreach (Button button in horizontal.Children)
			{
				button.IsEnabled = true;
				button.Style = null;
			}
		}

		private void OnButtonCLickedResetGame(object sender, EventArgs e)
		{
			ResetScreen();
		}
		#endregion
	}

}
