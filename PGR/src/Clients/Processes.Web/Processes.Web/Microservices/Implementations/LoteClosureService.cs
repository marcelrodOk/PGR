using Common;
using Common.Implementations;
using NLog;
using Processes.Web.Microservices.Interfaces;
using System.Reflection;

namespace Processes.Web.Microservices.Implementations
{
	public class LoteClosureService : BaseService, ILoteClosureService
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		private readonly IHttpClientFactory _clientFactory;

		public LoteClosureService(IHttpClientFactory clientFactory) : base(clientFactory)
		{
			_clientFactory = clientFactory;
		}

		public async Task<ResponseDto> GetLoteClosure<T>()
		{
			Logger.Info("Method: {0}", MethodBase.GetCurrentMethod());

			return await SendAsync<ResponseDto>(new ApiRequest
			{
				apiType = StaticDetails.ApiType.GET,
				url = StaticDetails.LoteClosureAPIBase + "api/v1/LoteClosure/GetLoteClosure"
			});
		}
	}
}
