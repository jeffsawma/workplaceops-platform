using Microsoft.AspNetCore.Mvc;
using WorkplaceOps.Application.Businesses;

namespace WorkplaceOps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BusinessOperationalProfilesController : ControllerBase
{
    private readonly BusinessOperationalProfileService _businessOperationalProfileService;

    public BusinessOperationalProfilesController(BusinessOperationalProfileService businessOperationalProfileService)
    {
        _businessOperationalProfileService = businessOperationalProfileService;
    }

    // POST: api/businessOperationalProfiles
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateBusinessOperationalProfile(CreateBusinessOperationalProfileRequest request)
    {
        try
        {
            var businessOperationalProfile = await _businessOperationalProfileService.CreateBusinessOperationalProfileAsync(request);

            return CreatedAtAction(
                nameof(GetBusinessOperationalProfileByBusinessId),
                new { businessId = businessOperationalProfile.BusinessId },
                businessOperationalProfile);
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new { message = exception.Message });
        }
    }

    // GET: api/businessOperationalProfiles/business/{businessId}
    [HttpGet("business/{businessId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBusinessOperationalProfileByBusinessId(Guid businessId)
    {
        var businessOperationalProfile = await _businessOperationalProfileService.GetByBusinessIdAsync(businessId);

        if (businessOperationalProfile is null)
        {
            return NotFound();
        }

        return Ok(businessOperationalProfile);
    }
}
