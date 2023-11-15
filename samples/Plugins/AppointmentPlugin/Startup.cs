[assembly: Plugin(typeof(Startup))]

namespace Example.AppointmentPlugin;

public class Startup : IWebStartup
{
    public string Name => "appointment";
    public string Description => "Expose endpoints.";

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<AppointmentService>();

        var appointments = new List<Appointment>
        {
            new() 
            { 
                Id = Guid.NewGuid().ToString(),
                DoctorName = "Bob",
                PatientName = "Alice",
                Date = new DateOnly(2023, 01, 01)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(),
                DoctorName = "Dave",
                PatientName = "Martin",
                Date = new DateOnly(2023, 01, 05)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(),
                DoctorName = "Steven",
                PatientName = "Smith",
                Date = new DateOnly(2023, 01, 10)
            },
        };
        services.AddSingleton(appointments);
    }
}
