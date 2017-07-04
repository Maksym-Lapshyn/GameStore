using System.Collections.Generic;

namespace GameStore.Web.Models
{
    public class PlatformTypeViewModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public List<GameViewModel> Games { get; set; }
    }
}