using Domain;
using Microsoft.AspNetCore.Mvc;
using MVCAlunos.Models;
using Repository;

namespace MVCAlunos.Controllers
{
    public class AlunosController : Controller
    {
        // GET: Alunos
        public IActionResult Index()
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
                Sexo = (Models.EnumeradorSexo)o.Sexo
            });

            return View(listaAlunoModel);
        }


        [HttpPost]

        public IActionResult GetMatricula(string searchString)
        {

            RepositorioAluno ra = new();
            List<AlunoModel> lista_alunos = new List<AlunoModel>();


            Aluno alunoEncontrado = ra.GetByMatricula(Convert.ToInt32(searchString));
            if (alunoEncontrado == null)
            {

                return RedirectToAction(nameof(Index));
            }
            else
            {

                AlunoModel aluno = new AlunoModel
                {
                    Matricula = alunoEncontrado.Matricula,
                    Nome = alunoEncontrado.Nome,
                    Cpf = alunoEncontrado.Cpf,
                    Nascimento = alunoEncontrado.Nascimento,
                    Sexo = (Models.EnumeradorSexo)alunoEncontrado.Sexo
                };

                lista_alunos.Add(aluno);

                return RedirectToAction(nameof(Index), lista_alunos.FirstOrDefault());
            }
        }
        // GET: Alunos/Details/5
        public IActionResult Details(int? id)
        {
            RepositorioAluno ra = new();

            IEnumerable<Aluno> lista_alunos = ra.Get(m => m.Matricula == id);

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
        public IActionResult Create(AlunoModel alunoModel)
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

            if (ra.GetByMatricula(alunoModel.Matricula) != null)
            {
                ModelState.AddModelError("Matricula", "Esse aluno já foi cadastrado");

                return View();

            }
            else
            {

                ra.Add(aluno);
                return RedirectToAction(nameof(Index));

            }

        }

        // GET: Alunos/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
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
                Sexo = (Models.EnumeradorSexo)o.Sexo
            });


            return View(listaAlunoModel.FirstOrDefault());
        }

        // POST: Alunos/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AlunoModel alunoModel)
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


            ra.Update(aluno);


            return RedirectToAction(nameof(Index));
        }

        // GET: Alunos/Delete/5
        public IActionResult Delete(int id)
        {
            RepositorioAluno ra = new RepositorioAluno();

            IEnumerable<Aluno> lista_alunos = new List<Aluno>();
            IEnumerable<AlunoModel> listaAlunoModel = new List<AlunoModel>();


            lista_alunos = ra.Get(m => m.Matricula == id);

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
        [ValidateAntiForgeryToken]


        public IActionResult Delete(int id, AlunoModel alunoModel)
        {
            /*if (ModelState.IsValid == true)
            {
                return NotFound();
            }
            */
            RepositorioAluno ra = new RepositorioAluno();

            Aluno aluno = new()
            {
                Matricula = alunoModel.Matricula,
                Nome = alunoModel.Nome,
                Cpf = alunoModel.Cpf,
                Nascimento = alunoModel.Nascimento,
                Sexo = (Domain.EnumeradorSexo)alunoModel.Sexo
            };

            ra.Remove(aluno);
            return RedirectToAction(nameof(Index));

        }

    }
}