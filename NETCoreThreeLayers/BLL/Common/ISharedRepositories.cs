using DAL.RepositoriesManager;

namespace BLL.Common
{
    public interface ISharedRepositories
    {
        public IRepositoriesManager RepositoriesManager { get; }
    }
}