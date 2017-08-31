using AutoMapper;
using GameStore.Authentification.Abstract;
using GameStore.Services.Abstract;
using System.Collections.Generic;
using System.Web.Http;

namespace GameStore.Web.Controllers.Api
{
	public class GenresController : BaseApiController
	{
		private readonly IGenreService _genreService;
		private readonly IMapper _mapper;

		public GenresController(IGenreService genreService,
			IMapper mapper, 
			IApiAuthentication authentication)
			:base(authentication)
		{
			_genreService = genreService;
			_mapper = mapper;
		}

		// GET api/<controller>
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<controller>/5
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<controller>
		public void Post([FromBody]string value)
		{
		}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/<controller>/5
		public void Delete(int id)
		{
		}
	}
}