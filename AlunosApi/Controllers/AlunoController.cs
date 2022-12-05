using Domain;
using Microsoft.AspNetCore.Mvc;
using MVCAlunos.Models;
using Repository;


namespace AlunosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        [HttpGet]

        public IEnumerable<Aluno> GetAlunos()
        {

            RepositorioAluno ra = new();

            IEnumerable<Aluno> lista_alunos = new List<Aluno>();
            lista_alunos = ra.GetAll();

            IEnumerable<AlunoModel> listaAlunoModel = lista_alunos.Select(o => new AlunoModel
            {
                Matricula = o.Matricula,
                Nome = o.Nome,
                Cpf = o.Cpf,
                Nascimento = o.Nascimento,
                Sexo = (MVCAlunos.Models.EnumeradorSexo)o.Sexo
            });

            return lista_alunos;

        }

        [HttpGet("{id}")]
        public ActionResult<Aluno> GetAlunos(int id)
        {
            RepositorioAluno ra = new();

            IEnumerable<Aluno> lista_alunos = new List<Aluno>();

            lista_alunos = ra.Get(m => m.Matricula == id);

            IEnumerable<AlunoModel> listaAlunoModel = lista_alunos.Select(o => new AlunoModel
            {
                Matricula = o.Matricula,
                Nome = o.Nome,
                Cpf = o.Cpf,
                Nascimento = o.Nascimento,
                Sexo = (MVCAlunos.Models.EnumeradorSexo)o.Sexo
            });
            return lista_alunos.FirstOrDefault();
        }

        [HttpPost]
        public void PostAlunos([FromBody] Aluno aluno)
        {

            if (ModelState.IsValid == true)
            {
                RepositorioAluno ra = new();

           
                ra.Add(aluno);

            }
            else
            {

            }
        }

        [HttpDelete("{id}")]
        public void Delete(Aluno aluno)
        {

            if (ModelState.IsValid == true)
            {
                RepositorioAluno ra = new RepositorioAluno();
                ra.Remove(aluno);
            }

        }

        [HttpPut("{id}")]

        public void PutAlunos(int id, [FromBody] Aluno aluno)
        {
            if (ModelState.IsValid == true)
            {
                RepositorioAluno ra = new RepositorioAluno();
                ra.Update(aluno);

            }

        }

    }
}
