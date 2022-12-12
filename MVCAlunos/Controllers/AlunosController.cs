using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAlunos.Models;
using Repository;

namespace MVCAlunos.Controllers
{
    public class AlunosController : Controller
    {
        // GET: Alunos
        public async Task<IActionResult> Index(string searchString)
        {
            RepositorioAluno ra = new();
            RepositorioAbstrato<Aluno> rb = ra; // upcast

            IEnumerable<Aluno> lista_alunos = new List<Aluno>();
            List<Aluno> lista_alunos_new = new List<Aluno>();

            lista_alunos = await rb.GetAll(); //utilizando a classe pai ao invés da especializada


            var alunosFiltrados = from a in lista_alunos
                                  select a;


            if (!String.IsNullOrEmpty(searchString))
            {
                //alunosFiltrados = await ra.GetByContendoNoNome(searchString.ToUpper()); //filtra por parte do nome 

                alunosFiltrados = alunosFiltrados.Where(s => s.Nome!.Contains(searchString.ToUpper()) || s.Matricula.ToString()!.Equals(searchString)); //filtra por parte do nome OU matricula

            }

            IEnumerable<AlunoModel> listaAlunoModel = alunosFiltrados.Select(o => new AlunoModel
            {
                Matricula = o.Matricula,
                Nome = o.Nome,
                Cpf = o.Cpf,
                Nascimento = o.Nascimento,
                Sexo = (Models.EnumeradorSexo)o.Sexo
            });


            return View(listaAlunoModel.ToList());

        }

        [HttpGet]

        public async Task<IActionResult> GetMatricula(string searchString)
        {

            RepositorioAluno ra = new();
            List<Aluno> lista_alunos = new List<Aluno>();


            Aluno alunoEncontrado = await ra.GetByMatricula(Convert.ToInt32(searchString));

            lista_alunos.Add(alunoEncontrado);

            if (alunoEncontrado == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                IEnumerable<AlunoModel> listaAlunoModel = lista_alunos.Select(o => new AlunoModel
                {
                    Matricula = o.Matricula,
                    Nome = o.Nome,
                    Cpf = o.Cpf,
                    Nascimento = o.Nascimento,
                    Sexo = (Models.EnumeradorSexo)o.Sexo
                });

                return RedirectToAction(nameof(Index), listaAlunoModel.FirstOrDefault());
            }
        }


        [HttpGet]

        public async Task<IActionResult> GetNome(string searchString)
        {

            RepositorioAluno ra = new();
            IEnumerable<Aluno> alunosEncontrados = await ra.GetByContendoNoNome(searchString);

            if (alunosEncontrados == null)
            {

                return RedirectToAction(nameof(Index));
            }
            else
            {


                IEnumerable<AlunoModel> listaAlunoModel = alunosEncontrados.Select(o => new AlunoModel
                {
                    Matricula = o.Matricula,
                    Nome = o.Nome,
                    Cpf = o.Cpf,
                    Nascimento = o.Nascimento,
                    Sexo = (Models.EnumeradorSexo)o.Sexo
                });

                return RedirectToAction(nameof(Index), listaAlunoModel);

            }
        }

        // GET: Alunos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            RepositorioAluno ra = new();

            IEnumerable<Aluno> lista_alunos = await ra.Get(m => m.Matricula == id);

            IEnumerable<AlunoModel> listaAlunoModel = lista_alunos.Select(o => new AlunoModel
            {
                Matricula = o.Matricula,
                Nome = o.Nome,
                Cpf = o.Cpf,
                Nascimento = o.Nascimento,
                Sexo = (Models.EnumeradorSexo)o.Sexo
            });


            return View(listaAlunoModel.FirstOrDefault());
        }

        // GET: Alunos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alunos/Create

        [HttpPost]
        public async Task<IActionResult> Create(AlunoModel alunoModel)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            RepositorioAluno ra = new();


            Aluno aluno = new Aluno
            {
                Matricula = alunoModel.Matricula,
                Nome = alunoModel.Nome,
                Cpf = alunoModel.Cpf,
                Nascimento = alunoModel.Nascimento,
                Sexo = (Domain.EnumeradorSexo)alunoModel.Sexo
            };
            Aluno alunoExiste = await ra.GetByMatricula(alunoModel.Matricula);
            if (alunoExiste.GetHashCode() == 0) // se o retorno de aluno for um padrão, onde a matricula é nula é pq não existe nenhum aluno com essa matricula
            {
                await ra.Add(aluno);
                return RedirectToAction(nameof(Index));

            }
            else
            {

                ModelState.AddModelError("Matricula", "Essa matrícula já existe, insira uma nova.");

                return View();

            }

        }

        // GET: Alunos/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
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
                Sexo = (Models.EnumeradorSexo)o.Sexo
            });


            return View(listaAlunoModel.FirstOrDefault());
        }

        // POST: Alunos/Edit/5

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AlunoModel alunoModel)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            RepositorioAluno ra = new RepositorioAluno();

            Aluno aluno = new Aluno
            {
                Matricula = alunoModel.Matricula,
                Nome = alunoModel.Nome,
                Cpf = alunoModel.Cpf,
                Nascimento = alunoModel.Nascimento,
                Sexo = (Domain.EnumeradorSexo)alunoModel.Sexo
            };


            await ra.Update(aluno);


            return RedirectToAction(nameof(Index));
        }

        // GET: Alunos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            RepositorioAluno ra = new RepositorioAluno();

            IEnumerable<Aluno> lista_alunos = new List<Aluno>();
            IEnumerable<AlunoModel> listaAlunoModel = new List<AlunoModel>();


            lista_alunos = await ra.Get(m => m.Matricula == id);

            listaAlunoModel = lista_alunos.Select(o => new AlunoModel
            {
                Matricula = o.Matricula,
                Nome = o.Nome,
                Cpf = o.Cpf,
                Nascimento = o.Nascimento,
                Sexo = (Models.EnumeradorSexo)o.Sexo
            });

            return View(listaAlunoModel.FirstOrDefault());
        }


        // POST: Alunos/Delete/5
        [HttpPost]


        public async Task<IActionResult> Delete(int id, AlunoModel alunoModel)
        {

            RepositorioAluno ra = new RepositorioAluno();

            Aluno aluno = new()
            {
                Matricula = alunoModel.Matricula,
                Nome = alunoModel.Nome,
                Cpf = alunoModel.Cpf,
                Nascimento = alunoModel.Nascimento,
                Sexo = (Domain.EnumeradorSexo)alunoModel.Sexo
            };

            await ra.Remove(aluno);
            return RedirectToAction(nameof(Index));

        }

    }
}