namespace Example.TestWebApi;

public class GetAppointmentResponse
{
    public string Id { get; set; }
    public string DoctorName { get; set; }
    public string PatientName { get; set; }
}

public class GetPersonResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
}
