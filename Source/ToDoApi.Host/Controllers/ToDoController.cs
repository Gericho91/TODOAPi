using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ToDoApi.Application.ToDos;
using ToDoApi.Host.Models;

namespace ToDoApi.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _todoService;

        public ToDoController(IToDoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetToDos()
        {
            var toDos = await _todoService.GetToDosAsync(GetUserId());

            return Ok(toDos);
        }

        [HttpGet("ByPage")]
        public async Task<IActionResult> GetToDosByPage([FromQuery] Pagination pagination)
        {
            var toDos = await _todoService.GetToDosAsync(GetUserId());

            return Ok(toDos);
        }

        private Guid GetUserId()
        {
            var userId = this.User.GetUserId();

            if (userId == null)
            {
                throw new Exception("Current user not found");
            }

            return userId.Value;
        }
    }
}
