using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stories.Application.Features.GetBestStories;

namespace Stories.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("best")]
        public async Task<IActionResult> GetBestStories(int n)
        {
            if (n <= 0)
            {
                return BadRequest("The number of stories must be greater than 0.");
            }

            var query = new GetBestStoriesQuery { NumberOfStories = n };
            var stories = await _mediator.Send(query);
            return Ok(stories);
        }
    }
}
