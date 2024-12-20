using Backend.Models;
using System;
using System.Collections.Generic;

namespace Backend.DummyData
{
    public class PosiljkeManager
    {
        // Staticka lista koja se koristi za čuvanje posiljki u memoriji
        private static List<Posiljka> _posiljke = new List<Posiljka>();

        // Dodavanje nove posiljke u listu
        public static void AddPosiljka(Posiljka posiljka)
        {
            _posiljke.Add(posiljka);
        }

        // Dohvatanje svih posiljki
        public static List<Posiljka> GetPosiljke()
        {
            return _posiljke;
        }

        // Dohvatanje posiljke po ID-u
        public static Posiljka? GetPosiljkaById(Guid id)
        {
            return _posiljke.FirstOrDefault(p => p.Id == id);
        }

        // Ažuriranje posiljke po ID-u
        public static bool UpdatePosiljka(Guid id, Posiljka updatedPosiljka)
        {
            var posiljka = GetPosiljkaById(id);
            if (posiljka == null) return false;

            posiljka.Naziv = updatedPosiljka.Naziv;
            posiljka.Status = updatedPosiljka.Status;
            posiljka.DatumIsporuke = updatedPosiljka.DatumIsporuke;

            return true;
        }

        // Brisanje posiljke po ID-u
        public static bool DeletePosiljka(Guid id)
        {
            var posiljka = GetPosiljkaById(id);
            if (posiljka == null) return false;

            _posiljke.Remove(posiljka);
            return true;
        }
    }
}