using GameStore.Web.Infrastructure.Abstract;
using GameStore.Web.Models;
using System.Collections.Generic;

namespace GameStore.Web.Infrastructure.Concrete
{
    public class GamePipeline : IPipeline<IEnumerable<GameViewModel>>
    {
        private readonly List<IFilter<IEnumerable<GameViewModel>>> _filters = new List<IFilter<IEnumerable<GameViewModel>>>();

        public void ApplyFilter(FilterViewModel model)
        {
            
        }

        public IPipeline<IEnumerable<GameViewModel>> Register(IFilter<IEnumerable<GameViewModel>> filter)
        {
            _filters.Add(filter);

            return this;
        }

        public IEnumerable<GameViewModel> Process(IEnumerable<GameViewModel> input)
        {
            _filters.ForEach(f => input = f.Execute(input));

            return input;
        }
    }
}