using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrenomsController : ControllerBase
    {
        // Simule une base de données en mémoire (statique pour l'exemple)
        private static List<string> _prenoms = new List<string> {
            "Philippe",
            "Hugo",
            "Christelle",
            "Aurore",
            "Margaux",
            "Guillaume"
        };

        // GET /prenoms
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetPrenoms()
        {
            return Ok(_prenoms);
        }

        // GET /prenoms/{id}
        [HttpGet("{id}")]
        public ActionResult<string> GetPrenom(int id)
        {
            if (id < 0 || id >= _prenoms.Count)
                return NotFound();
            return Ok(_prenoms[id]);
        }

        // POST /prenoms  (body: string (nom à ajouter))
        [HttpPost]
        public ActionResult AddPrenom([FromBody] string prenom)
        {
            if (string.IsNullOrWhiteSpace(prenom))
                return BadRequest("Le prénom ne peut pas être vide.");
            _prenoms.Add(prenom);
            return CreatedAtAction(nameof(GetPrenom), new { id = _prenoms.Count - 1 }, prenom);
        }

        // PUT /prenoms/{id} (body: string (nouveau prénom))
        [HttpPut("{id}")]
        public ActionResult UpdatePrenom(int id, [FromBody] string prenom)
        {
            if (id < 0 || id >= _prenoms.Count)
                return NotFound();
            if (string.IsNullOrWhiteSpace(prenom))
                return BadRequest("Le prénom ne peut pas être vide.");
            _prenoms[id] = prenom;
            return NoContent();
        }

        // DELETE /prenoms/{id}
        [HttpDelete("{id}")]
        public ActionResult DeletePrenom(int id)
        {
            if (id < 0 || id >= _prenoms.Count)
                return NotFound();
            _prenoms.RemoveAt(id);
            return NoContent();
        }
    }
}
