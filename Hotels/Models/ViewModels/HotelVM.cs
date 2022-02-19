using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Hotels.Models.ViewModels
{
    public class HotelVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Por favor, insira o Nome do Hotel")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Por favor, insira o CNPJ do Hotel")]
        public string CNPJ { get; set; }
        [Required(ErrorMessage = "Por favor, insira o Endereço do Hotel")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        [Required(ErrorMessage = "Por favor, insira a Descrição do Hotel")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public IFormFile Foto { get; set; }
    }
}
