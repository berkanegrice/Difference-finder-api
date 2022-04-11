using System;
using System.Threading.Tasks;
using Diff.Application.Interfaces;
using Diff.Application.Models;
using Diff.Domain.Entities;

namespace Diff.Infrastructure.Files
{
    public class DifferencesFinder : IDifferencesFinder
    {
        public Task<ResultVm> GetDifferences(InputModel a, InputModel b)
        {
            var res = new ResultVm() { Id = a.Id };
            string resultMessage;
            var firstStr = Convert.FromBase64String(a.Base64Str);
            var secondStr = Convert.FromBase64String(b.Base64Str);

            if (firstStr.Length == secondStr.Length)
            {
                for (var i = 0; i < firstStr.Length; i++)
                {
                    var byteDiff = 0;
                    var diff = (firstStr[i] ^ secondStr[i]);
                    if (diff != 0)
                    {
                        while (diff != 0)
                        {
                            byteDiff++;
                            diff &= diff - 1;
                        }

                        res.Differences.Add(new Differences() { Diff = byteDiff, Offset = i });
                    }
                }

                resultMessage = res.Differences.Count == 0
                    ? "inputs were equal"
                    : "inputs were compared";
            }
            else
                resultMessage = "inputs are of different size";

            res.ResultMessage = resultMessage;
            return Task.FromResult(res);
        }
    }
}