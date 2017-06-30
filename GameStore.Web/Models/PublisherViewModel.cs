using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.Models
{
    public class PublisherViewModel
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public string HomePage { get; set; }

        public IEnumerable<GameViewModel> Games { get; set; }

        public PublisherViewModel()
        {
            Games = new List<GameViewModel>();
        }
    }
}