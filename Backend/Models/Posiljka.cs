
namespace Backend.Models
{
    public class Posiljka
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Naziv { get; set; } = string.Empty; 

        public StatusPosiljke Status { get; set; }

        public DateTime DatumKreiranja { get; set; } 
        public DateTime? DatumIsporuke { get; set; } 
    }

    public enum StatusPosiljke
    {
        NaPutu,
        Isporuceno,
        Uskladistu
    }
}