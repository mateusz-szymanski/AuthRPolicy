using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Domain
{
    public interface IUnitOfWork
    {
        Task SaveChanges();
    }
}
