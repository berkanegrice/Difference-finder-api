using Diff.Domain.Entities;
using NUnit.Framework;
using Diff.Infrastructure.Files;
using Microsoft.Extensions.Logging;
using Moq;

namespace Infrastructure.UnitTests
{
    public class ComparatorTests
    {
        private DifferencesFinder _differencesFinder;

        [SetUp]
        public void Setup()
        {
            _differencesFinder = new DifferencesFinder(Mock.Of<ILogger<DifferencesFinder>>());
        }

        [Test]
        public void Inputs_Size_Are_Different()
        {
            var input1 = new InputModel() { Id = 123, Data = "YW5hbmRh" };
            var input2 = new InputModel() { Id = 123, Data = "dmluaWNpdXM=" };
            var resp = _differencesFinder.GetDifferences(input1, input2);
            Assert.AreEqual("inputs are of different size", resp.Result.ResultMessage);
        }
        
        [Test]
        public void Inputs_Are_Equal()
        {
            var input1 = new InputModel() { Id = 123, Data = "eyJpbnB1dCI6InRlc3RWYWx1ZSJ9" };
            var input2 = new InputModel() { Id = 123, Data = "eyJpbnB1dCI6InRlc3RWYWx1ZSJ9" };
            var resp = _differencesFinder.GetDifferences(input1, input2);
            Assert.AreEqual("inputs were equal", resp.Result.ResultMessage);
        }

        [Test]
        public void Inputs_Are_Different()
        {
            var input1 = new InputModel() { Id = 123, Data = "cWViWm9uWHRWTw==" };
            var input2 = new InputModel() { Id = 123, Data = "cWp5QW5hTkp2WA==" };
            var resp = _differencesFinder.GetDifferences(input1, input2);
            Assert.AreEqual("inputs were compared", resp.Result.ResultMessage);
        }
    }
}