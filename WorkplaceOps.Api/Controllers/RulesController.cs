using Microsoft.AspNetCore.Mvc;
using WorkplaceOps.Application.Rules;

namespace WorkplaceOps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RulesController : ControllerBase
{
    private readonly RuleService _ruleService;

    public RulesController(RuleService ruleService)
    {
        _ruleService = ruleService;
    }

    // POST: api/rules
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateRule(CreateRuleRequest request)
    {
        var rule = await _ruleService.CreateRuleAsync(request);

        return CreatedAtAction(
            nameof(GetRuleById),
            new { id = rule.Id },
            rule);
    }

    // GET: api/rules
    [HttpGet]
    public async Task<IActionResult> GetRules()
    {
        var rules = await _ruleService.GetAllRulesAsync();

        return Ok(rules);
    }

    // GET: api/rules/{id}
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRuleById(Guid id)
    {
        var rule = await _ruleService.GetRuleByIdAsync(id);

        if (rule is null)
        {
            return NotFound();
        }

        return Ok(rule);
    }
}
