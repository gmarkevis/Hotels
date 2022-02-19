using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotels.Models
{
    public class Quarto
    {
        public int Id { get; set; }
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
        public string Nome { get; set; }
        public int NumeroOcupantes { get; set; }
        public int NumeroAdultos { get; set; }
        public int NumeroCriancas { get; set; }
        public double Preco { get; set; }
        public byte[] Foto { get; set; }
        public virtual Hotel Hotel { get; set; }
    }
}
