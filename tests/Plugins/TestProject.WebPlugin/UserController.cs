﻿using Microsoft.AspNetCore.Mvc;
using SimpleResults;

namespace TestProject.WebPlugin;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpGet]
    public ListedResult<User> GetAll(UserService service) 
        => service.GetAll();

    [HttpPost]
    public Result Create([FromBody]User user, UserService service) 
        => service.Create(user.Name, user.Password);

    [HttpDelete]
    public Result Clear(UserService service)
        => service.Clear();
}
