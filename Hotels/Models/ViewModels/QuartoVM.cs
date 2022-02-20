using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Hotels.Models.ViewModels
{
    public class QuartoVM
    {
        public int Id { get; set; }
        [Display(Name = "Hotel")]
        public int HotelId { get; set; }
        [Required(ErrorMessage = "Por favor, insira o Nome do Quarto")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Por favor, insira o Número de Ocupantes do Quarto")]
        [Display(Name = "Número de Ocupantes")]
        public int NumeroOcupantes { get; set; }
        [Required(ErrorMessage = "Por favor, insira o Número de Adultos do Quarto")]
        [Display(Name = "Número de Adultos")]
        public int NumeroAdultos { get; set; }
        [Required(ErrorMessage = "Por favor, insira o Número de Crianças do Quarto")]
        [Display(Name = "Número de Crianças")]
        public int NumeroCriancas { get; set; }
        [Required(ErrorMessage = "Por favor, insira o Preço do Quarto")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Display(Name = "Preço")]
        public double Preco { get; set; }
        public IFormFile Foto { get; set; }
    }
}
