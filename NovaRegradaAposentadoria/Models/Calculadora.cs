
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace NovaRegradaAposentadoria.Models
{

    public class Calculadora
    {
        [Required]
        public string Sexo { get; set; }

        [Required(AllowEmptyStrings=false,ErrorMessage="Informe sua idade")]
        [Range(1,100,ErrorMessage="Informe um valor entre 1 e 100")]
        public int Idade { get; set; }

        [Required]
        public int TipoTempo { get; set; }

        [Required(AllowEmptyStrings=false,ErrorMessage="Informe o periodo")]
        [Range(1, 100, ErrorMessage = "Informe um valor entre 1 e 100")]
        public int Tempo { get; set; }

        [Display(Name="Trabalha como professor?")]
        public bool Professor { get; set; }

    }

    public class Resultado
    {
        public int IdadeOp1 { get; set; }

        public int TempoOp1 { get; set; }

        public int IdadeOp2 { get; set; }

        public int TempoOp2 { get; set; }
    }

}