using Backend.Models;
using Backend.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Backend.Services
{
    public class PosiljkeService
    {
        private readonly IPosiljkaRepository _repository;
        private readonly ILogger<PosiljkeService> _logger;

        public PosiljkeService(IPosiljkaRepository repository, ILogger<PosiljkeService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IEnumerable<Posiljka> GetAll()
        {
            _logger.LogInformation("Service: Fetching all posiljke.");
            return _repository.GetAll();
        }

        public Posiljka? GetById(Guid id)
        {
            _logger.LogInformation("Service: Fetching posiljka with ID: {id}", id);
            return _repository.GetById(id);
        }

        public void Add(Posiljka posiljka)
        {   posiljka.Id = Guid.NewGuid();
            _logger.LogInformation("Service: Adding posiljka with ID: {id}", posiljka.Id);
            _repository.Add(posiljka);
        }

        public bool Update(Guid id, Posiljka updatedPosiljka)
        {
            _logger.LogInformation("Service: Updating posiljka with ID: {id}", id);
            return _repository.Update(id, updatedPosiljka);
        }

        public bool Delete(Guid id)
        {
            _logger.LogInformation("Service: Deleting posiljka with ID: {id}", id);
            return _repository.Delete(id);
        }
    }
}