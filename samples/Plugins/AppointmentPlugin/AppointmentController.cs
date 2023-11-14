namespace AppointmentPlugin;

[TranslateResultToActionResult]
[ApiController]
[Route("[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly AppointmentService _appointmentService;
    public AppointmentController(AppointmentService appointmentService) => _appointmentService = appointmentService;

    [HttpGet]
    public ListedResult<Appointment> Get() 
        => _appointmentService.GetAll();

    [HttpGet("{id}")]
    public Result<Appointment> Get(string id) 
        => _appointmentService.GetById(id);
}
