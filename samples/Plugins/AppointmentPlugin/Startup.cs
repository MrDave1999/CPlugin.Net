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
                Id = "16e299b4-a6dc-4aa3-a52c-7dae088241e6",
                DoctorName = "Bob",
                PatientName = "Alice",
                Date = new DateOnly(2023, 01, 01)
            },
            new()
            {
                Id = "5dace456-13d9-42c5-92ea-89869ac44b5c",
                DoctorName = "Dave",
                PatientName = "Martin",
                Date = new DateOnly(2023, 01, 05)
            },
            new()
            {
                Id = "4575709f-cb5d-4919-9fc2-4afcf5f3c431",
                DoctorName = "Steven",
                PatientName = "Smith",
                Date = new DateOnly(2023, 01, 10)
            },
        };
        services.AddSingleton(appointments);
    }
}
