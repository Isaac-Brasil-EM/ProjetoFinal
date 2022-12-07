using System.ComponentModel.DataAnnotations;
namespace MVCAlunos.Models
{
    public enum EnumeradorSexo
    {
        Masculino = 0,
        Feminino = 1
    }
    public class AlunoModel
    {
        [Key]
        [Range(0, int.MaxValue, ErrorMessage = "Insira um valor positivo")]
        public int Matricula { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Nome { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage = "O Campo CPF deve ter 11 dígitos. ")]
        public string Cpf { get; set; }

        [DataType(DataType.Date)]
      /*  [Range(typeof(DateTime), "01/01/0001", "01/01/2002",
        ErrorMessage = "{0} deve estar entre {1:d} e {2:d}")]*/
        public DateTime Nascimento { get; set; }

        public EnumeradorSexo Sexo { get; set; }
    }
}
