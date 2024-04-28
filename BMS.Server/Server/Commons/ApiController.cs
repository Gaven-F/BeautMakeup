using Microsoft.AspNetCore.Mvc;

namespace Server.Commons;

[Route("[controller]/[action]")]
public abstract class ApiController : ControllerBase
{
}
