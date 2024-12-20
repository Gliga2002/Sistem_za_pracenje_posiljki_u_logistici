using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;

namespace Backend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PosiljkeController : ControllerBase
    {
        private readonly PosiljkeService _service;
        private readonly IValidator<Posiljka> _validator;
        private readonly ILogger<PosiljkeController> _logger;

        public PosiljkeController(PosiljkeService service, IValidator<Posiljka> validator, ILogger<PosiljkeController> logger)
        {
            _service = service;
            _validator = validator;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Posiljka>))]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Controller: Fetching all posiljke.");
            var posiljke = _service.GetAll();
            return Ok(posiljke);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(Posiljka))]
        [ProducesResponseType(404)]
        public IActionResult GetById(Guid id)
        {
            _logger.LogInformation("Controller: Fetching posiljka with ID: {id}", id);
            var posiljka = _service.GetById(id);
            if (posiljka == null)
            {
                _logger.LogWarning("Posiljka with ID: {id} not found.", id);
                return NotFound();
            }

            return Ok(posiljka);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Posiljka))]
        [ProducesResponseType(400)]
        public IActionResult Create(Posiljka novaPosiljka)
        {
            _logger.LogInformation("Controller: Creating posiljka with ID: {id}", novaPosiljka.Id);

            var result = _validator.Validate(novaPosiljka);
            if (!result.IsValid)
            {
                _logger.LogWarning("Controller: Validation failed for posiljka with ID: {id}", novaPosiljka.Id);
                return BadRequest(result.Errors);
            }

            _service.Add(novaPosiljka);
            _logger.LogInformation("Controller: Posiljka with ID: {id} created successfully.", novaPosiljka.Id);
            return CreatedAtAction(nameof(GetById), new { id = novaPosiljka.Id }, novaPosiljka);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(Guid id, Posiljka izmenjenaPosiljka)
        {
            _logger.LogInformation("Controller: Updating posiljka with ID: {id}", id);

            var result = _validator.Validate(izmenjenaPosiljka);
            if (!result.IsValid)
            {
                _logger.LogWarning("Controller: Validation failed for posiljka with ID: {id}", id);
                return BadRequest(result.Errors);
            }

            if (!_service.Update(id, izmenjenaPosiljka))
            {
                _logger.LogWarning("Controller: Posiljka with ID: {id} not found for update.", id);
                return NotFound();
            }

            _logger.LogInformation("Controller: Posiljka with ID: {id} updated successfully.", id);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(Guid id)
        {
            _logger.LogInformation("Controller: Deleting posiljka with ID: {id}", id);

            if (!_service.Delete(id))
            {
                _logger.LogWarning("Controller: Posiljka with ID: {id} not found for deletion.", id);
                return NotFound();
            }

            _logger.LogInformation("Controller: Posiljka with ID: {id} deleted successfully.", id);
            return NoContent();
        }
    }
}