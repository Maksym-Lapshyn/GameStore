using GameStore.Web.Infrastructure.Abstract;
using GameStore.Web.Models;
using System.Collections.Generic;

namespace GameStore.Web.Infrastructure.Concrete
{
    public class GenreFilter : IFilter<IEnumerable<GameViewModel>>
    {
        public IEnumerable<GameViewModel> Execute(IEnumerable<GameViewModel> input)
        {
            throw new System.NotImplementedException();
        }
    }
}