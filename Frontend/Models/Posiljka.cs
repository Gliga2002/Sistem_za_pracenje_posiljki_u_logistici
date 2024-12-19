using System;
using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class Posiljka
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Naziv je obavezan.")]
        public string Naziv { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status je obavezan.")]
        [EnumDataType(typeof(StatusPosiljke), ErrorMessage = "Status mora biti validan.")]
        public StatusPosiljke Status { get; set; }

        [Required(ErrorMessage = "Datum kreiranja je obavezan.")]
        public DateTime DatumKreiranja { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Datum isporuke je obavezan.")]
        [FutureDate(ErrorMessage = "Datum isporuke mora biti u budućnosti.")]
        public DateTime? DatumIsporuke { get; set; }
    }

    public enum StatusPosiljke
    {
        NaPutu,
        Isporuceno,
        Uskladistu
    }

    // Custom validacija za DatumIsporuke
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dateValue)
            {
                return dateValue > DateTime.Now;
            }
            return false;
        }
    }
}