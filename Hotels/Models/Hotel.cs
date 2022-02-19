using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Endereco { get; set; }
        public string Descricao { get; set; }
        public byte[] Foto { get; set; }

        public virtual Quarto Quarto { get; set; }
    }
}
