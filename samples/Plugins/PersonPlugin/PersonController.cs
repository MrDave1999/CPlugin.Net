﻿namespace Example.PersonPlugin;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    [HttpGet] 
    public Result<IEnumerable<Person>> GetAll(IPersonService service)
        => service.GetAll();

    [HttpPost]
    public ActionResult<Result> Create() => Result.CreatedResource().ToActionResult();
}
