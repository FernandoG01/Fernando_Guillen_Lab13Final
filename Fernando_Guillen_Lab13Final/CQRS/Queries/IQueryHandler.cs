using System.Threading.Tasks;

namespace Fernando_Guillen_Lab13Final.CQRS.Queries
{
    public interface IQueryHandler<TQuery, TResult>
    {
        Task<TResult> Handle(TQuery query);
    }
}