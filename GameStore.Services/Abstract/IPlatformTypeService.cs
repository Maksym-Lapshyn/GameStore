using GameStore.Services.DTOs;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IPlatformTypeService
	{
		IEnumerable<PlatformTypeDto> GetAll();
	}
}