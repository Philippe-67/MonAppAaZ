using Microsoft.AspNetCore.Mvc;
using MonApi.Services;
using MonApi.Models;

namespace MonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrenomsController : ControllerBase
{
    private readonly IPrenomsService _prenomService;

    public PrenomsController(IPrenomsService prenomService)
    {
        _prenomService = prenomService;
    }

    // ✅ GET ALL - /api/prenoms
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var prenoms = await _prenomService.GetAllPrenomsAsync();
        return Ok(prenoms);
    }

    // ✅ GET ONE - /api/prenoms/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var prenom = await _prenomService.GetPrenomByIdAsync(id);
        if (prenom is null)
            return NotFound($"Prénom avec l'id {id} introuvable.");
        return Ok(prenom);
    }

    // ✅ CREATE - /api/prenoms
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Prenoms prenom)
    {
        await _prenomService.AddPrenomAsync(prenom);
        return CreatedAtAction(nameof(GetById), new { id = prenom.Id }, prenom);
    }

    // ✅ UPDATE - /api/prenoms/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Prenoms prenom)
    {
        var existing = await _prenomService.GetPrenomByIdAsync(id);
        if (existing is null)
            return NotFound($"Prénom avec l'id {id} introuvable.");

        prenom.Id = id;
        await _prenomService.UpdatePrenomAsync(prenom);
        return NoContent();
    }

    // ✅ DELETE - /api/prenoms/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var existing = await _prenomService.GetPrenomByIdAsync(id);
        if (existing is null)
            return NotFound($"Prénom avec l'id {id} introuvable.");

        await _prenomService.DeletePrenomAsync(id);
        return NoContent();
    }
}
