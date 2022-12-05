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
        [Range(1, int.MaxValue, ErrorMessage = "Insira um valor positivo")]
        public int Matricula { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Nome { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage = "O Campo CPF deve ter 11 dígitos. ")]
        public string Cpf { get; set; }


        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }

        public EnumeradorSexo Sexo { get; set; }
    }
}
