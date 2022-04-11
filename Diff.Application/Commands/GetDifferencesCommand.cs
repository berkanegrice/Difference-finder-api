using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Diff.Application.Interfaces;
using Diff.Application.Models;
using Diff.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Diff.Application.Commands
{
    public class GetDifferencesCommand : IRequest<ResultVm>
    {
        public int Id { get; init; }
    }

    public class GetDifferencesCommandHandler : IRequestHandler<GetDifferencesCommand, ResultVm>
    {
        private readonly IInputRepository _repository;
        private readonly IDifferencesFinder _diffFinder;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetDifferencesCommandHandler(IInputRepository repository, IDifferencesFinder finder, IMapper mapper, ILogger<GetDifferencesCommandHandler> logger)
        {
            _repository = repository;
            _diffFinder = finder;
            _mapper = mapper;
            _logger = logger;
        }
        
        public Task<ResultVm> Handle(GetDifferencesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get difference for id = {0}", request.Id);
            try
            {
                var pairs = _repository.GetPairs(request.Id).Result; 
                return _diffFinder.GetDifferences(pairs[0], pairs[1]); 
            }
            catch (Exception e)
            {
                _logger.LogError("Error message: {0}", e.Message);
                var res = new ResultVm()
                {
                    Id = request.Id,
                    ResultMessage = e.Message
                };
                
                return Task.FromResult(res);
            }
        }
    }
}