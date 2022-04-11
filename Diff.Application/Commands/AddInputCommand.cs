using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Diff.Domain.Repositories;
using Diff.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Diff.Application.Commands
{
    public class AddInputCommand : IRequest<bool>
    {
        [Required] public int Id { get; set; }
        [Required] public string Side { get; set; }
        [Required] public string Data { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Side: {Side}, Data: {Data}";
        }
    }

    public class AddBase64InputCommandHandler : IRequestHandler<AddInputCommand, bool>
    {
        private readonly IInputRepository _inputRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AddBase64InputCommandHandler(IInputRepository inputRepository, IMapper mapper,
            ILogger<AddBase64InputCommandHandler> logger)
        {
            _inputRepository = inputRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(AddInputCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"The given pair {request} will be added.");
            var model = _mapper.Map<InputModel>(request);
            var resp = await _inputRepository.Add(model);

            return Task.FromResult(resp).IsCompletedSuccessfully;
        }
    }
}