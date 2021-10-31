using AspNetCoreProj.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreProj.WebApi.Controllers.Identity
{
    [Route(WebApiAddresses.Identity.Users)]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
    }
}
