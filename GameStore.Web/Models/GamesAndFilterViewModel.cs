using System.Collections.Generic;

namespace GameStore.Web.Models
{
    public class GamesAndFilterViewModel
    {
        public FilterViewModel Filter { get; set; }

        public List<GameViewModel> Games { get; set; }

        public GamesAndFilterViewModel()
        {
            Games = new List<GameViewModel>();
        }
    }
}