using System.Threading.Tasks;
using Diff.Domain.Entities;
using Diff.Application.Models;

namespace Diff.Application.Interfaces
{
    public interface IDifferencesFinder
    {
        Task<ResultVm> GetDifferences(InputModel a, InputModel b);
    } 
}