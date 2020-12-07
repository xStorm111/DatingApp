using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))] //set the user Last Time Active every time he makes a new request
    [ApiController]
    [Route("api/[controller]")]

    public class BaseApiController : ControllerBase
    {

    }
}