namespace Example.AppointmentPlugin;

public class AppointmentService
{
    private readonly List<Appointment> _appointments;
    public AppointmentService(List<Appointment> appointments) => _appointments = appointments;

    public ListedResult<Appointment> GetAll()
        => Result.Success(_appointments);

    public Result<Appointment> GetById(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return Result.Invalid("ID is required");

        var appointment = _appointments.Find(u => u.Id == id);
        if (appointment is null)
            return Result.NotFound();

        return Result.Success(appointment);
    }
}
