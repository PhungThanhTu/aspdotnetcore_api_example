using BLL.Common;
using DAL.Aggregates;
using DAL.Repositories;

namespace BLL.Services;

public class Common : ICommon
{
    private readonly IGenericRepository<User> _userRepository;

    public Common(ISharedRepositories sharedRepositories)
    {
        _userRepository = sharedRepositories.RepositoriesManager.UserRepository;
    }

    public bool IsUserIdValid(Guid userId)
    {
        if (_userRepository.GetById(userId) is not null) return true;
        return false;
    }
}