namespace Example.ProgramPlugin;

public class SummaryService
{
    private static readonly string[] Summaries =
    [
        "Freezing", 
        "Bracing", 
        "Chilly", 
        "Cool", 
        "Mild", 
        "Warm", 
        "Balmy", 
        "Hot", 
        "Sweltering", 
        "Scorching"
    ];

    public IEnumerable<string> GetAll() 
        => Summaries;
}
