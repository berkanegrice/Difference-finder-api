using System.Threading;
using AutoMapper;
using NUnit.Framework;
using Diff.Application.Commands;
using Diff.Application.Mapper;
using Diff.Domain.Entities;
using Diff.Infrastructure.Data;
using Diff.Infrastructure.Files;
using Diff.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.UnitTests
{
    public class ApplicationTests
    {
        #region Variables

        private IConfigurationProvider _configuration;
        private IMapper _mapper;
        private InputRepository _inputRepository;
        private InputContext<InputModel> _inputContext;
        private DifferencesFinder _differencesFinder;

        private AddBase64InputCommandHandler _addBase64InputCommandHandler;
        private GetDifferencesCommandHandler _getDifferencesCommandHandler;

        #endregion

        [SetUp]
        public void Setup()
        {
            _configuration = new MapperConfiguration(config =>
                config.AddProfile<MapperProfile>());
            _mapper = _configuration.CreateMapper();
            _inputContext = new InputContext<InputModel>();
            
            _inputRepository = new InputRepository(_inputContext, Mock.Of<ILogger<InputRepository>>());
            _addBase64InputCommandHandler = new AddBase64InputCommandHandler(_inputRepository, _mapper, Mock.Of<ILogger<AddBase64InputCommandHandler>>());
            _differencesFinder = new DifferencesFinder(Mock.Of<ILogger<DifferencesFinder>>());
            
            _getDifferencesCommandHandler =
                new GetDifferencesCommandHandler(_inputRepository, _differencesFinder, _mapper, Mock.Of<ILogger<GetDifferencesCommandHandler>>());
        }

        [Test]
        public void Insert_Successfully_Left()
        {
            var command = new AddInputCommand()
            {
                Id = 123,
                Side = "left",
                Data = "YXpGd2p2RHVXcA=="
            };
            var resp = _addBase64InputCommandHandler.Handle(command, CancellationToken.None);
            Assert.AreEqual(true, resp.Result);
        }

        [Test]
        public void Insert_Successfully_Right()
        {
            var command = new AddInputCommand
            {
                Id = 123,
                Side = "right",
                Data = "ZVdmZmdmWVlHTg=="
            };
            var resp = _addBase64InputCommandHandler.Handle(command, CancellationToken.None);
            Assert.AreEqual(true, resp.Result);
        }

        [Test]
        public void Input_Is_Not_Exist()
        {
            var command = new GetDifferencesCommand() { Id = 123 };
            var resp = _getDifferencesCommandHandler.Handle(command, CancellationToken.None);

            Assert.AreEqual("There is no elements corresponding to given id = '123'", resp.Result.ResultMessage);
        }

        [Test]
        public void Pair_Is_Not_Exist()
        {
            var command = new AddInputCommand()
            {
                Id = 123,
                Side = "left",
                Data = "YXpGd2p2RHVXcA=="
            };
            var resp = _addBase64InputCommandHandler.Handle(command, CancellationToken.None);
            Assert.AreEqual(true, resp.Result);

            var command1 = new AddInputCommand
            {
                Id = 124,
                Side = "right",
                Data = "ZVdmZmdmWVlHTg=="
            };
            var resp1 = _addBase64InputCommandHandler.Handle(command1, CancellationToken.None);
            Assert.AreEqual(true, resp.Result);

            var command2 = new GetDifferencesCommand() { Id = 123 };
            var resp2 = _getDifferencesCommandHandler.Handle(command2, CancellationToken.None);

            Assert.AreEqual("An error occured while trying to get 'right' pair", resp2.Result.ResultMessage);
        }
    }
}