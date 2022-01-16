using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ToDoApi.Application.ToDos;
using ToDoApi.Host.Models;

namespace ToDoApi.Host.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ToDoController : Controller
    {
        private readonly IToDoService _todoService;

        public ToDoController(IToDoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetToDos()
        {
            var userId = this.User.GetUserId();

            if(userId == null)
            {
                throw new Exception("Current user not found");
            }

            var toDos = await _todoService.GetToDos();

            return Ok(toDos);
        }
    }
}
