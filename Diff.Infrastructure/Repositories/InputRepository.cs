using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diff.Domain.Entities;
using Diff.Domain.Repositories;
using Diff.Infrastructure.Data;
using Diff.Infrastructure.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace Diff.Infrastructure.Repositories
{
    public class InputRepository : Repository<InputModel>, IInputRepository
    {
        private readonly ILogger _logger;

        public InputRepository(InputContext<InputModel> inputContext, ILogger<InputRepository> logger)
            : base(inputContext)
        {
            _logger = logger;
        }

        public Task<InputModel> GetPair(IEnumerable<InputModel> pairs, string side)
        {
            try
            {
                _logger.LogInformation($"Trying to get pair = {side}");
                return Task.FromResult(pairs.First(x => x.Side.Equals(side)));
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException($"An error occured while trying to get '{side}' pair");
            }
        }

        public Task<IList<InputModel>> GetPairs(int id)
        {
            _logger.LogInformation($"Trying to get pairs with id = {id}");

            var list = _inputContext.InputModels.Where(x => x.Id.Equals(id)).ToList();
            if (!list.Any())
                throw new InvalidOperationException($"There is no elements corresponding to given id = '{id}'");

            var left = GetPair(list, "left");
            var right = GetPair(list, "right");
            var l = new List<InputModel>() { left.Result, right.Result };

            return Task.FromResult<IList<InputModel>>(l);
        }

        public void IsExist(int id, string side)
        {
            var oldEntity = _inputContext.InputModels.FirstOrDefault(x => x.Id.Equals(id) && x.Side.Equals(side));
            if (oldEntity is null) return;
            _logger.LogInformation($"The id = {id} and side = {side} already exist, new entry will overwrite it.");
            Remove(oldEntity);
        }

        public new Task<bool> Add(InputModel entity)
        {
            IsExist(entity.Id, entity.Side);
            _logger.LogInformation($"The given pair {entity} is added.");
            return base.Add(entity);
        }
    }
}