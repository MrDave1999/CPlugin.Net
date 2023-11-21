[assembly: Plugin(typeof(Startup))]

namespace Example.PersonPlugin;

public class Startup : IWebStartup
{
    public string Name => "person";
    public string Description => "Expose endpoints.";

    public void ConfigureServices(IServiceCollection services)
    {
        var persons = new List<Person>
        {
            new()
            {
                Id = 1,
                Name = "Alice",
                Document = "0923611701",
                DocumentType = DocumentTypes.IdentityCard
            },
            new()
            {
                Id = 2,
                Name = "Bob",
                Document = "0923611755",
                DocumentType = DocumentTypes.Passport
            },
            new()
            {
                Id = 3,
                Name = "Steven",
                Document = "0923611777",
                DocumentType = DocumentTypes.IdentityCard
            }
        };

        services.AddSingleton(persons);
        services.AddTransient<IPersonService, PersonService>();
    }
}
