using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
    public interface IPlatformTypeService : IService<PlatformTypeDto>
    {
        IEnumerable<PlatformTypeDto> GetBy(int gameId);
    }
}
