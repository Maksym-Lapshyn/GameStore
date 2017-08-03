using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Entities;
using System.Collections.Generic;

namespace GameStore.DAL.Concrete.Common
{
	public class PlatformTypeRepository : IPlatformTypeRepository
	{
		private readonly IEfPlatformTypeRepository _efRepository;

		public PlatformTypeRepository(IEfPlatformTypeRepository efRepository)
		{
			_efRepository = efRepository;
		}

		public PlatformType Get(string type)
		{
			return _efRepository.Get(type);
		}

		public IEnumerable<PlatformType> Get()
		{
			return _efRepository.Get();
		}

		public bool Contains(string type)
		{
			return _efRepository.Contains(type);
		}

		public void Insert(PlatformType platformType)
		{
			_efRepository.Insert(platformType);
		}
	}
}
