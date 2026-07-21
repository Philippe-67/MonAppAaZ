using Microsoft.AspNetCore.Mvc;
using MonApi.Services;
using MonApi.Models;
using MonApi.IServices;

namespace MonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MotsController : ControllerBase
{
    private readonly IMotsService _motsService;

    public MotsController(IMotsService motsService)
    {
        _motsService = motsService;
    }

    // ✅ GET ALL - /api/mots
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var mots = await _motsService.GetAllMotsAsync();
        return Ok(mots);
    }

    // ✅ GET ONE - /api/mots/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var mot = await _motsService.GetMotByIdAsync(id);
        if (mot is null)
            return NotFound($"Mot avec l'id {id} introuvable.");
        return Ok(mot);
    }

    // ✅ CREATE - /api/mots
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Mot mot)
    {
        await _motsService.AddMotAsync(mot);
        return CreatedAtAction(nameof(GetById), new { id = mot.Id }, mot);
    }

    // ✅ UPDATE - /api/mots/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Mot mot)
    {
        var existing = await _motsService.GetMotByIdAsync(id);
        if (existing is null)
            return NotFound($"Mot avec l'id {id} introuvable.");

        mot.Id = id;
        await _motsService.UpdateMotAsync(mot);
        return NoContent();
    }

    // ✅ DELETE - /api/mots/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var existing = await _motsService.GetMotByIdAsync(id);
        if (existing is null)
            return NotFound($"Mot avec l'id {id} introuvable.");

        await _motsService.DeleteMotAsync(id);
        return NoContent();
    }

    // ✅ GET INTERRO ITEMS - /api/mots/interro?count=5    

    [HttpGet("interro")]
    public async Task<IActionResult> GetInterro([FromQuery] int count = 5)
    {
        var items = await _motsService.GetInterroItemsAsync(count);
        return Ok(items);
    }}
