using Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Processes.Web.Microservices.Interfaces;
using Processes.Web.Models.DTOs.RequestDTO;
using Processes.Web.Models.DTOs.ResponseDTO;

namespace Processes.Web.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class LoteClosureController : ControllerBase
	{

		private readonly IConfiguration _configuration;
		private readonly ILoteClosureService _loteClosureService;

		public LoteClosureController(IConfiguration configuration, ILoteClosureService loteClosureService)
		{
			_configuration = configuration;
			_loteClosureService = loteClosureService;

		}

		[HttpGet(Name = "GetLoteClosure")]
		public async Task<ActionResult<IList<TblIntermediaResponseDTO>>> GetLoteClosure()
		{
			var response = await _loteClosureService.GetLoteClosure<TblIntermediaRequestDTO>();

			var tblIntermediaResponseDTO = DeserializeTblIntermedia<TblIntermediaResponseDTO>(response);

			return tblIntermediaResponseDTO.ToList();
		}
		private static List<T> DeserializeTblIntermedia<T>(ResponseDto response)
		{
			var tblIntermediaResponseDTO = JsonConvert.DeserializeObject<List<T>>(response.Data.ToString()).ToList();
			return tblIntermediaResponseDTO;
		}
	}
}
