using System.ComponentModel.DataAnnotations;
namespace MVCAlunos.Models
{
   
    public class AlunoModel
    {
        [Key]
        [Range(1, int.MaxValue, ErrorMessage = "Insira um valor positivo")]
        [Required(ErrorMessage = "O Campo Matricula é obrigatório")]

        public int Matricula { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "O Campo Nome é obrigatório")]

        public string Nome { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage = "O Campo CPF deve ter 11 dígitos. ")]
        [Required(ErrorMessage = "O Campo CPF é obrigatório")]
        public string Cpf { get; set; }

        /*[Range(typeof(DateTime), "01/01/1000", "01/01/2999",
        ErrorMessage = "{0} deve estar entre {1:d} e {2:d}")]*/
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "O Campo Nascimento é obrigatório")]
        public DateTime Nascimento { get; set; }

        [Required(ErrorMessage = "O Campo Sexo é obrigatório")]
        public EnumeradorSexo Sexo { get; set; }
    }
}
