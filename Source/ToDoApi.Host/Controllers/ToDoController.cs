using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Net;

using ToDoApi.Application.ToDos;
using ToDoApi.Application.ToDos.Dto;
using ToDoApi.Domain.Models;
using ToDoApi.Host.Models;

namespace ToDoApi.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _todoService;
        private readonly ILogger _logger;

        public ToDoController(IToDoService todoService, 
            ILogger logger)
        {
            _todoService = todoService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddToDoAsync([FromBody]CreateToDoDto toDo)
        {
            await _todoService.AddToDoAsync(toDo, GetUserId());
            return Ok();
        }

        [HttpPost("Finished")]
        public async Task<IActionResult> ToDoFinishedAsync([FromQuery] Guid id)
        {
            await _todoService.ToDoFinishedAsync(id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteToDoAsync([FromQuery] Guid id)
        {
            try
            {
                await _todoService.DeleteToDoAsync(id);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateToDoAsync([FromBody] UpdateToDoDto toDo)
        {
            await _todoService.UpdateToDoAsync(toDo);
            return Ok();
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<ToDoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetToDosAsync()
        {
            var toDos = await _todoService.GetToDosAsync(GetUserId());

            return Ok(toDos);
        }

        [HttpGet("ById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ToDoDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetToDoByIdAsync([FromQuery]Guid id)
        {
            var toDo = await _todoService.GetToDoByIdAsync(id);

            if(toDo == null)
            {
                return NotFound();
            }

            return Ok(toDo);
        }

        [HttpGet("ByState")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<ToDoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetToDosByStateAsync([FromQuery]bool isFinished)
        {
            var toDos = await _todoService.GetToDosByStateAsync(isFinished, GetUserId());

            return Ok(toDos);
        }

        [HttpGet("ByFilterText")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<ToDoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> FilterToDoAsync([FromBody] string filterText)
        {
            var toDos = await _todoService.FilterToDoAsync(filterText, GetUserId());

            return Ok(toDos);
        }

        [HttpGet("ByPage")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PaginationResult<ToDoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetToDosByPagingAsync([FromQuery] Pagination pagination)
        {
            var toDos = await _todoService.GetToDosByPagingAsync(pagination.Page, pagination.PageSize, GetUserId());

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
