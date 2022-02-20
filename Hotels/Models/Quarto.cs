using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotels.Models
{
    public class Quarto
    {
        public int Id { get; set; }
        [ForeignKey("Hotel")]
        [Display(Name = "Hotel")]
        public int HotelId { get; set; }
        public string Nome { get; set; }
        [Display(Name = "Número de Ocupantes")]
        public int NumeroOcupantes { get; set; }
        [Display(Name = "Número de Adultos")]
        public int NumeroAdultos { get; set; }
        [Display(Name = "Número de Crianças")]
        public int NumeroCriancas { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Display(Name = "Preço")]
        public double Preco { get; set; }
        public byte[] Foto { get; set; }
        public virtual Hotel Hotel { get; set; }
    }
}
