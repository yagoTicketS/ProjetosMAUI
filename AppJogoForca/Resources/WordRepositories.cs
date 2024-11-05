using AppJogoForca.Models;

namespace AppJogoForca.Resources
{
	public class WordRepositories
	{
		private List<Word> _words;

		public WordRepositories()
		{
			_words = new List<Word>();
			_words.Add(new Word("Nome", "Maria".ToUpper()));
			_words.Add(new Word("Vegetal", "Cenoura".ToUpper()));
			_words.Add(new Word("Fruta", "Abacate".ToUpper()));
			_words.Add(new Word("Tempero", "Nordestino".ToUpper()));
			_words.Add(new Word("Tempero", "Bahiano".ToUpper()));
		}

		public Word GetRandomWord()
		{
			Random rand = new Random();
			var number = rand.Next(0, _words.Count);

			return _words [number];
		}
	}
}
