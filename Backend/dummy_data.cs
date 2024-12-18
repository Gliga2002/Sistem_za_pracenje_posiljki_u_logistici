using Backend.Models;


namespace Backend.DummyData
{
    public class PosiljkeManager
    {
  
        private static List<Posiljka> _posiljke = [];


        public static void AddPosiljka(Posiljka posiljka)
        {
            _posiljke.Add(posiljka);
        }

        public static List<Posiljka> GetPosiljke()
        {
            return _posiljke;
        }
    }
}