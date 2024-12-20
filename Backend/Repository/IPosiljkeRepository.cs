using Backend.Models;
using System;
using System.Collections.Generic;

namespace Backend.Repositories
{
    public interface IPosiljkaRepository
    {
        IEnumerable<Posiljka> GetAll();
        Posiljka? GetById(Guid id);
        void Add(Posiljka posiljka);
        bool Update(Guid id, Posiljka updatedPosiljka);
        bool Delete(Guid id);
    }
}