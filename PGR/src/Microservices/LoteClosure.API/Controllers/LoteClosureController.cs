using Common;
using LoteClosureServices.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Reflection;

namespace LoteClosure.API.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class LoteClosureController : ControllerBase
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private readonly ILoteClosureService _loteClosureService;
		protected ResponseDto _responseDto;
		protected JsonDataResult _jsonDataResult;
		//private IWebHostEnvironment _hostingEnvironment;

		public LoteClosureController(ILoteClosureService loteClosureService)
		{
			_loteClosureService = loteClosureService;
			this._jsonDataResult = new JsonDataResult();
		}

		[HttpGet("GetLoteClosure")]
		public async Task<IActionResult> GetLoteClosure()
		{
			try
			{
				_logger.Info("Method: {0}", MethodBase.GetCurrentMethod());

				var batchDto = await _loteClosureService.GetLoteClosure<ResponseDto>();

				if (batchDto.Count() == 0)
				{
					_jsonDataResult.Data = "Sin registros";
					_jsonDataResult.IsSuccess = true;
					_jsonDataResult.Code = 200;
					_jsonDataResult.Message = "successfully";
					return Ok(_jsonDataResult);
				}

				_jsonDataResult.Data = batchDto;
				_jsonDataResult.IsSuccess = true;
				_jsonDataResult.Code = 200;
				_jsonDataResult.Message = "successfully";
				return Ok(_jsonDataResult); ;
			}
			catch (Exception ex)
			{
				_logger.Error("Error: {0} - Detalle: {1} - Metodo: {2}", ex.Message.ToString(), ex.ToString(), ex.TargetSite);
				_jsonDataResult.IsSuccess = false;
				_jsonDataResult.Code = 400;
				_jsonDataResult.Message = ex.ToString();
				return BadRequest(_jsonDataResult);
			}

		}
	}
}
