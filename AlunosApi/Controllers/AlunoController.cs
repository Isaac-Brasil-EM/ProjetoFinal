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

        public async Task<IEnumerable<Aluno>> GetAlunos()
        {

            RepositorioAluno ra = new();

            IEnumerable<Aluno> lista_alunos = new List<Aluno>();
            lista_alunos = await ra.GetAll();

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
        public async Task<ActionResult<Aluno>> GetAlunos(int id)
        {
            RepositorioAluno ra = new();

            IEnumerable<Aluno> lista_alunos = new List<Aluno>();

            lista_alunos = await ra.Get(m => m.Matricula == id);

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
        public async Task PostAlunos([FromBody] Aluno aluno)
        {

            if (ModelState.IsValid == true)
            {
                RepositorioAluno ra = new();
                await ra.Add(aluno);

            }
            else
            {
                ModelState.AddModelError("", "Um erro ocorreu!");

            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(Aluno aluno)
        {

            if (ModelState.IsValid == true)
            {
                RepositorioAluno ra = new RepositorioAluno();
                await ra.Remove(aluno);
            }

        }

        [HttpPut("{id}")]

        public async Task PutAlunos(int id, [FromBody] Aluno aluno)
        {
            if (ModelState.IsValid == true)
            {
                RepositorioAluno ra = new RepositorioAluno();
                await ra.Update(aluno);

            }

        }

    }
}
