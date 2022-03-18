public class Word
{
    public string English { get; set; }
    public string Russian { get; set; }

    public string Theme { get; set; }

    public Word(string English, string Russian, string Theme)
    {
        this.English = English;
        this.Russian = Russian;
        this.Theme = Theme;
    }
}