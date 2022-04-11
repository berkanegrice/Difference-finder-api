using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Diff.Domain.Repositories;
using Diff.Domain.Entities;
using MediatR;

namespace Diff.Application.Commands
{
    public class AddInputCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Side { get; set; }
        public string Base64Str { get; set; }
    }

    public class AddBase64InputCommandHandler : IRequestHandler<AddInputCommand, bool>
    {
        private readonly IInputRepository _inputRepository;
        private readonly IMapper _mapper;

        public AddBase64InputCommandHandler(IInputRepository inputRepository, IMapper mapper)
        {
            _inputRepository = inputRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AddInputCommand request,
            CancellationToken cancellationToken)
        {
            var model = _mapper.Map<InputModel>(request);            
            var resp = await _inputRepository.Add(model);

            return Task.FromResult(resp).IsCompletedSuccessfully;
        }
    }
}