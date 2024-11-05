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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetAsync()
        {
            try
            {
                var reviews = await reviewService.GetAsync();
                return Ok(reviews);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedList<ReviewResponse>>> GetAllAsync([FromQuery] ReviewsParameters parameters)
        {
            try
            {
                var reviews = await reviewService.GetAllAsync(parameters);
                Response.Headers.Add("X-Pagination", reviews.Metadata.ToString());
                return Ok(reviews);
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
        public async Task<ActionResult<ReviewResponse>> GetByIdAsync([FromRoute] int id)
        {
            try
            {
                var review = await reviewService.GetByIdAsync(id);
                return Ok(review);
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
        public async Task<ActionResult> InsertAsync([FromBody] ReviewRequest request)
        {
            try
            {
                await reviewService.InsertAsync(request);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = request.UserId }, request);
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
        public async Task<ActionResult> UpdateAsync([FromRoute] int id, [FromBody] ReviewRequest request)
        {
            try
            {
                await reviewService.UpdateAsync(id, request);
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
                await reviewService.DeleteAsync(id);
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

        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetByUserIdAsync([FromRoute] int userId)
        {
            try
            {
                var reviews = await reviewService.GetByUserIdAsync(userId);
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

        [HttpGet("workout/{workOutId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetByWorkOutIdAsync([FromRoute] int workOutId)
        {
            try
            {
                var reviews = await reviewService.GetByWorkOutIdAsync(workOutId);
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
