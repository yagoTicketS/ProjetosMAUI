namespace AppJogoForca.Models
{
	public class Word
	{
		public Word(string tips, string text)
		{
			this.Tips = tips;
			this.Text = text;
		}
        public string Tips { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
	}
}
