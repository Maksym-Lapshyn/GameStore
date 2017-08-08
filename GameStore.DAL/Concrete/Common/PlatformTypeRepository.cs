using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Entities;
using System.Collections.Generic;

namespace GameStore.DAL.Concrete.Common
{
	public class PlatformTypeRepository : IPlatformTypeRepository //TODO Consider: Remove useless wrapper
	{
		private readonly IEfPlatformTypeRepository _efRepository;

		public PlatformTypeRepository(IEfPlatformTypeRepository efRepository)
		{
			_efRepository = efRepository;
		}

		public PlatformType GetSingle(string type)
		{
			return _efRepository.GetSingle(type);
		}

		public IEnumerable<PlatformType> GetAll()
		{
			return _efRepository.GetAll();
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