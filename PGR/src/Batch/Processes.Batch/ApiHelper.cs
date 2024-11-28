using Common;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NLog;
using Processes.Batch.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Processes.Batch
{
	public class ApiHelper
	{
		private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

		public HttpClient ApiClient { get; set; }


		public ApiHelper()
		{
			try
			{
				var setting = "";
				ApiClient = new HttpClient();
				ApiClient.BaseAddress = new Uri(ReadUrlShippingInvoice(setting));
				ApiClient.Timeout = TimeSpan.FromMinutes(30);
				ApiClient.DefaultRequestHeaders.ConnectionClose = false;
				ApiClient.DefaultRequestHeaders.Accept.Clear();
				ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


			}
			catch (Exception ex)
			{

				_logger.Error($"Exception: {0} - FullException{1} - Method: {2}", ex.Message, ex, "ApiHelper");
			}
		}

		public async Task<T> SendRequest<T>(Method method, string request, HttpContent content, Object data = null, string setting = "", bool generateRerpot = false)
		{
			_logger.Info($"SendRequest() - request: {request}");
			HttpRequestMessage message = new HttpRequestMessage();
			message.Headers.Add("Accept", "application/json");
			_logger.Info($"SendRequest() - setting: {setting}");
			message.RequestUri = new Uri(ReadUrlShippingInvoice(setting) + request);

			try
			{
				if (content != null)
				{
					message.Content = new StringContent(JsonConvert.SerializeObject(content),
						Encoding.UTF8, "application/json");
					_logger.Info("Method: SendAsync - Params: Data: {0}", message.Content.ToString());
				}

				if (data != null)
				{
					message.Content = new StringContent(JsonConvert.SerializeObject(data),
						Encoding.UTF8, "application/json");
					_logger.Info("Method: SendAsync - Params: Data: {0}", message.Content.ToString());
				}

				switch (method)
				{
					case Method.POST:
						message.Method = HttpMethod.Post;
						break;
					case Method.PUT:
						message.Method = HttpMethod.Put;
						break;
					case Method.DELETE:
						message.Method = HttpMethod.Delete;
						break;
					default:
						message.Method = HttpMethod.Get;
						break;
				}


				HttpResponseMessage response = await ApiClient.SendAsync(message);
				_logger.Info($"Status code: {response.StatusCode}");
				_logger.Info($"Message code: {response.ReasonPhrase}");

				var apiContent = await response.Content.ReadAsStringAsync();
				string res = "";
				var dto = new ResponseDto();
				if (generateRerpot)
				{
					if ((int)response.StatusCode != 200)
					{
						_logger.Error("ApiContent error: " + apiContent.ToString());
						dto = new ResponseDto
						{
							Message = "Reporte No Generado",
							IsSuccess = false,
							Code = (int)response.StatusCode,
							Data = "Failed"
						};
					}
					else
					{
						dto = new ResponseDto
						{
							Message = "Reporte Generado",
							IsSuccess = true,
							Code = 200,
							Data = "Success"
						};

					}
					res = JsonConvert.SerializeObject(dto);
					var apiResponseDtoReport = JsonConvert.DeserializeObject<T>(res);
					return apiResponseDtoReport;
				}

				var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);
				return apiResponseDto;
			}
			catch (Exception ex)
			{
				_logger.Error($"Exception: {ex.Message} - StackTrace: {ex.StackTrace} - Source: {ex.Source} - " +
				   $"InnerException: {(ex.InnerException != null ? ex.InnerException.Message : string.Empty)}");

				var dto = new ResponseBatchDto()
				{
					Message = ex.Message.ToString(),
					ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
					IsSuccess = false,
					Code = 400
				};

				var res = JsonConvert.SerializeObject(dto);
				var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
				return apiResponseDto;
			}

		}

		public void Dispose()
		{
			GC.SuppressFinalize(true);
		}

		public string ReadUrlShippingInvoice(string service)
		{
			var resultSetting = "";
			var configuration = new ConfigurationBuilder().AddJsonFile($"appsettings.json");
			var config = configuration.Build();


			switch (service)
			{
				case Constantes.SUCCESS_BATCH_INFO:
					resultSetting = config["BatchShippingUrl"];
					_logger.Info("url servicio produccion: {0}", config["BatchShippingUrl"].ToString());
					break;

			}

			return resultSetting;

		}

	}

	public enum Method
	{
		GET,
		POST,
		PUT,
		DELETE
	}
}
