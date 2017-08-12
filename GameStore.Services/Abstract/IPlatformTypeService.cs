using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IPlatformTypeService
	{
		IEnumerable<PlatformTypeDto> GetAll();
	}
}