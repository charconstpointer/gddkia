using System.Threading.Tasks;
using Gddkia.Models;

namespace Gddkia.Clients
{
    public interface IGddkiaClient
    {
        Task<GddkiaResponse> GetReport();
    }
}