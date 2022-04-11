using System;
using System.Threading.Tasks;
using Diff.Application.Interfaces;
using Diff.Application.Models;
using Diff.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Diff.Infrastructure.Files
{
    public class DifferencesFinder : IDifferencesFinder
    {

        private readonly ILogger _logger;
        public DifferencesFinder(ILogger<DifferencesFinder> logger)
        {
            _logger = logger;
        }
        
        private byte[] ConvertToStringFromBaseStr(string data)
        {
            try
            {
                _logger.LogInformation($"Trying to converts given base64-encoded {data} to string.");
                return Convert.FromBase64String(data);
            }
            catch (FormatException e)
            {
                _logger.LogError($"Errors occured when decoding the given string {data}");
                _logger.LogError($"Error message: {e.Message}");
                throw;
            }
        }
        
        /// <summary>
        /// This method produces a byte-wise differences given two InputModel.
        /// </summary>
        /// <param name="a"> this is a InputModel. </param>
        /// <param name="b"> this is a InputModel. </param>
        /// <returns></returns>
        public Task<ResultVm> GetDifferences(InputModel a, InputModel b)
        {
            var res = new ResultVm() { Id = a.Id };
            string resultMessage;
            
            // Converts encoded-Str to string.
            var firstStr = ConvertToStringFromBaseStr(a.Data);
            var secondStr = ConvertToStringFromBaseStr(b.Data);

            // Firstly check given inputs sizes are same?.
            if (firstStr.Length == secondStr.Length)
            {
                for (var i = 0; i < firstStr.Length; i++)
                {
                    var byteDiff = 0;
                    var diff = (firstStr[i] ^ secondStr[i]);
                    if (diff == 0) continue;
                    while (diff != 0)
                    {
                        byteDiff++;
                        diff &= diff - 1;
                    }
                    res.Differences.Add(new Differences() { Diff = byteDiff, Offset = i });
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