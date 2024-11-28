using Common;
using NLog;
using Processes.Batch.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processes.Batch
{
	public class ServiceClient
	{
		private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
		private readonly ApiHelper _api;

		public async Task<ResponseBatchDto> GetBatchProcess()
		{
			_logger.Info($"GetBatchProcess()");

			var request = $"api/v1/BatchShippingInvoice/GetBatchProcess";
			return (ResponseBatchDto)await _api.SendRequest<ResponseBatchDto>(Method.GET, request, null, null, Constantes.SUCCESS_BATCH_INFO, false);


		}
	}
}
