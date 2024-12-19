using Microsoft.AspNetCore.Mvc;
using Serilog;
using Backend.Models;
using Backend.Services;
using FluentValidation;

namespace Backend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PosiljkeController : ControllerBase
    {
        private readonly PosiljkeService _service;
        private readonly IValidator<Posiljka> _validator;

        public PosiljkeController(PosiljkeService service, IValidator<Posiljka> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult GetAll()
        {   
            
            Log.Information("Pregled svih pošiljki");
            return Ok(_service.GetAll());
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var posiljka = _service.GetById(id);
            if (posiljka == null)
            {
                Log.Warning("Pošiljka sa ID-jem {Id} nije pronađena", id);
                return NotFound();
            }

            Log.Information("Pregled pošiljke sa ID-jem {Id}", id);
            return Ok(posiljka);
        }

        [HttpPost]
        public IActionResult Create(Posiljka novaPosiljka)
        {
            var result = _validator.Validate(novaPosiljka);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            _service.Add(novaPosiljka);
            Log.Information("Dodana nova pošiljka");
            return CreatedAtAction(nameof(GetById), new { id = novaPosiljka.Id }, novaPosiljka);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, Posiljka izmenjenaPosiljka)
        {
            var result = _validator.Validate(izmenjenaPosiljka);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            if (!_service.Update(id, izmenjenaPosiljka))
            {
                Log.Warning("Pošiljka sa ID-jem {Id} nije pronađena", id);
                return NotFound();
            }

            Log.Information("Izmenjena pošiljka sa ID-jem {Id}", id);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            if (!_service.Delete(id))
            {
                Log.Warning("Pošiljka sa ID-jem {Id} nije pronađena", id);
                return NotFound();
            }

            Log.Information("Obrisana pošiljka sa ID-jem {Id}", id);
            return NoContent();
        }
    }
}