using Backend.Models;
using System;
using System.Collections.Generic;

namespace Backend.DummyData
{
    public class PosiljkeManager
    {
        
        private static List<Posiljka> _posiljke = new List<Posiljka>();

     
        public static void AddPosiljka(Posiljka posiljka)
        {
            _posiljke.Add(posiljka);
        }

       
        public static List<Posiljka> GetPosiljke()
        {
            return _posiljke;
        }

     
        public static Posiljka? GetPosiljkaById(Guid id)
        {
            return _posiljke.FirstOrDefault(p => p.Id == id);
        }

      
        public static bool UpdatePosiljka(Guid id, Posiljka updatedPosiljka)
        {
            var posiljka = GetPosiljkaById(id);
            if (posiljka == null) return false;

            posiljka.Naziv = updatedPosiljka.Naziv;
            posiljka.Status = updatedPosiljka.Status;
            posiljka.DatumIsporuke = updatedPosiljka.DatumIsporuke;

            return true;
        }

 
        public static bool DeletePosiljka(Guid id)
        {
            var posiljka = GetPosiljkaById(id);
            if (posiljka == null) return false;

            _posiljke.Remove(posiljka);
            return true;
        }
    }
}