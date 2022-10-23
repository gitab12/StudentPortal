using NZWalksAPI.DBData;
using NZWalksAPI.Models;

namespace NZWalksAPI.Repositories
{
    public interface IRepository 
    {
        Task <IEnumerable<Region>> GetAll();

    }
}
