using Domain;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class RepositorioAbstrato<T> where T : IEntidade
    {

        public abstract Task Add(T aluno);
        public abstract Task Remove(T aluno);
        public abstract Task  Update(T aluno);
        public abstract Task<IEnumerable<T>> GetAll();
        public abstract Task<IEnumerable<T>> Get(Expression<Func<Aluno, bool>> predicate);

    }
}
