using Common;
using LoteClosureServices.Business.DTOs.LoteClousureDTOs.ResponseDTO;
using LoteClosureServices.Business.Interfaces;
using NLog;
using ServiceLoteClosure.Data;
using System.Reflection;

namespace LoteClosureServices.Business.Implementations
{
	public class LoteClosureService : ILoteClosureService
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private readonly LoteClosureDbContext _db;

		//TODO: Alimenta la vista de monitoreo
		public async Task<IEnumerable<TblIntermediaResponseDTO>> GetLoteClosure<T>()
		{
			_logger.Info("Method: {0}", MethodBase.GetCurrentMethod());

			List<TblIntermediaResponseDTO> tblIntermedia = new List<TblIntermediaResponseDTO>();
			tblIntermedia.Add(new TblIntermediaResponseDTO { Id = 1, ProcessResult = "datos de prueba" });
			tblIntermedia.Add(new TblIntermediaResponseDTO { Id = 2, ProcessResult = "datos de prueba" });
			var loteClosure = tblIntermedia;
			//var loteClosure = await _db.TablaIntermedia.Where(b => b.ProcessResult != null).ToListAsync();

			return loteClosure.MapTo<List<TblIntermediaResponseDTO>>();

		}
	}
}
