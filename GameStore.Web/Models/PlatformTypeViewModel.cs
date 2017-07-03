using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.Services.DTOs;

namespace GameStore.Web.Models
{
    public class PlatformTypeViewModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public List<GameViewModel> Games { get; set; }
    }
}