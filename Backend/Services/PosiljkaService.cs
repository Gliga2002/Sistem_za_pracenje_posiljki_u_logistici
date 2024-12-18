using Backend.Models;
using Backend.DummyData;

namespace Backend.Services
{
    public class PosiljkeService
    {
        private readonly List<Posiljka> _posiljke = PosiljkeManager.GetPosiljke();

        public IEnumerable<Posiljka> GetAll() => _posiljke;

        public Posiljka? GetById(Guid id) => _posiljke.FirstOrDefault(p => p.Id == id);

        public void Add(Posiljka posiljka)
        {
            posiljka.Id = Guid.NewGuid();
            posiljka.DatumKreiranja = DateTime.UtcNow;
            PosiljkeManager.AddPosiljka(posiljka);
        }

        public bool Update(Guid id, Posiljka updatedPosiljka)
        {
            var posiljka = GetById(id);
            if (posiljka == null) return false;

            posiljka.Naziv = updatedPosiljka.Naziv;
            posiljka.Status = updatedPosiljka.Status;
            posiljka.DatumIsporuke = updatedPosiljka.DatumIsporuke;
            return true;
        }

        public bool Delete(Guid id)
        {
            var posiljka = GetById(id);
            if (posiljka == null) return false;

            _posiljke.Remove(posiljka);
            return true;
        }
    }
}