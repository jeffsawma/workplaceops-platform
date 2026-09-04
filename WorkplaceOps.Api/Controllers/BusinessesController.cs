using Microsoft.AspNetCore.Mvc;
using WorkplaceOps.Application.Businesses;

namespace WorkplaceOps.Api.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class BusinessesController : ControllerBase
{
    private readonly BusinessService _businessService;

    public BusinessesController(BusinessService businessService)
    {
        _businessService = businessService;
    }

    // POST: api/businesses
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBusiness(CreateBusinessRequest request)
    {
        var business = await _businessService.CreateBusinessAsync(request); // Call the service to create a new business

        // Return a 201 Created response with the location of the newly created business
        return CreatedAtAction(
            nameof(GetBusinesses),
            new { id = business.Id },
            business);
    }

    // GET: api/businesses
    [HttpGet]
    public async Task<IActionResult> GetBusinesses()
    {
        var businesses = await _businessService.GetAllBusinessesAsync(); // Call the service to retrieve all businesses

        return Ok(businesses); // Return the list of businesses with a 200 OK status
    }

    // GET: api/businesses/{id}
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBusinessById(Guid id)
    {
        var business = await _businessService.GetBusinessByIdAsync(id);

        // If the business is not found, return a 400 Bad Request response
        if (business is null)
        {
            return NotFound();
        }

        // Or else return the business with a 200 OK response
        return Ok(business);
    }
}
