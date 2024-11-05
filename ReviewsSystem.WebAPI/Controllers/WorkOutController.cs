using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReviewsSystem.BLL.DTO.Requests;
using ReviewsSystem.BLL.DTO.Responses;
using ReviewsSystem.BLL.Interfaces;
using ReviewsSystem.DAL.Exceptions;
using ReviewsSystem.DAL.Pagination;
using ReviewsSystem.DAL.Parameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewsSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkOutController : ControllerBase
    {
        private readonly IWorkOutService workOutService;

        public WorkOutController(IWorkOutService workOutService)
        {
            this.workOutService = workOutService;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<WorkOutResponse>>> GetAsync()
        {
            try
            {
                var workouts = await workOutService.GetAsync();
                return Ok(workouts);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedList<WorkOutResponse>>> GetAllAsync([FromQuery] WorkOutsParameters parameters)
        {
            try
            {
                var workouts = await workOutService.GetAllAsync(parameters);
                Response.Headers.Add("X-Pagination", workouts.Metadata.ToString());
                return Ok(workouts);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<WorkOutResponse>> GetByIdAsync([FromRoute] int id)
        {
            try
            {
                var workout = await workOutService.GetByIdAsync(id);
                return Ok(workout);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> InsertAsync([FromBody] WorkOutRequest request)
        {
            try
            {
                await workOutService.InsertAsync(request);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = request.Name }, request);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync([FromRoute] int id, [FromBody] WorkOutRequest request)
        {
            try
            {
                await workOutService.UpdateAsync(id, request);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                await workOutService.DeleteAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        [HttpGet("{userId}/reviews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetByUserIdAsync([FromRoute] int userId)
        {
            try
            {
                var reviews = await workOutService.GetByUserIdAsync(userId);
                return Ok(reviews);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        [HttpGet("workout/{workOutId}/reviews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetByWorkOutIdAsync([FromRoute] int workOutId)
        {
            try
            {
                var reviews = await workOutService.GetByWorkOutIdAsync(workOutId);
                return Ok(reviews);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
    }
}
