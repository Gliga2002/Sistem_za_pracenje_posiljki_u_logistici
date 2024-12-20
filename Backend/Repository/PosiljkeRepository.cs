using Backend.Models;
using Backend.DummyData;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repositories
{
    public class PosiljkaRepository : IPosiljkaRepository
    {
        private readonly ILogger<PosiljkaRepository> _logger;

        public PosiljkaRepository(ILogger<PosiljkaRepository> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Posiljka> GetAll()
        {
            _logger.LogInformation("Fetching all posiljke from in-memory storage.");
            return PosiljkeManager.GetPosiljke();
        }

        public Posiljka? GetById(Guid id)
        {
            _logger.LogInformation("Fetching posiljka with ID: {id}", id);
            return PosiljkeManager.GetPosiljkaById(id);
        }

        public void Add(Posiljka posiljka)
        {
            _logger.LogInformation("Adding new posiljka with ID: {id}", posiljka.Id);
            PosiljkeManager.AddPosiljka(posiljka);
            _logger.LogInformation("Posiljka with ID: {id} added successfully.", posiljka.Id);
        }

        public bool Update(Guid id, Posiljka updatedPosiljka)
        {
            _logger.LogInformation("Updating posiljka with ID: {id}", id);
            var success = PosiljkeManager.UpdatePosiljka(id, updatedPosiljka);
            if (success)
            {
                _logger.LogInformation("Posiljka with ID: {id} updated successfully.", id);
            }
            else
            {
                _logger.LogWarning("Posiljka with ID: {id} not found for update.", id);
            }
            return success;
        }

        public bool Delete(Guid id)
        {
            _logger.LogInformation("Deleting posiljka with ID: {id}", id);
            var success = PosiljkeManager.DeletePosiljka(id);
            if (success)
            {
                _logger.LogInformation("Posiljka with ID: {id} deleted successfully.", id);
            }
            else
            {
                _logger.LogWarning("Posiljka with ID: {id} not found for deletion.", id);
            }
            return success;
        }
    }
}