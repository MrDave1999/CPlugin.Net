namespace AppointmentPlugin;

public class Appointment
{
    public string Id { get; init; }
    public string DoctorName {  get; init; }
    public string PatientName {  get; init; }
    public DateOnly Date {  get; init; }
}
