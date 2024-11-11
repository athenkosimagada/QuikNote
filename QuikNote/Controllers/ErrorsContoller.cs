using Microsoft.AspNetCore.Mvc;

namespace QuikNote.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error() => Problem();
}