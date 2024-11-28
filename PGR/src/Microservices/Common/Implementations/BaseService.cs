using Common.Interfaces;
using Newtonsoft.Json;
using NLog;
using System.Net.Http.Headers;
using System.Text;

namespace Common.Implementations
{
	public class BaseService : IBaseService
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		public ResponseDto responseModel { get; set; }
		public IHttpClientFactory httpClient { get; set; }


		public BaseService(IHttpClientFactory httpClient)
		{
			this.responseModel = new ResponseDto();
			this.httpClient = httpClient;
		}

		public async Task<T> SendAsync<T>(ApiRequest apiResquest)
		{
			try
			{
				Logger.Info("Method: SendAsync - Params: Url: {0} - Tipo: {1}", apiResquest.url, apiResquest.apiType.ToString());
				var client = httpClient.CreateClient("Logistic");
				HttpRequestMessage message = new HttpRequestMessage();
				message.Headers.Add("Accept", "application/json");
				message.RequestUri = new Uri(apiResquest.url);
				client.DefaultRequestHeaders.Clear();
				if (apiResquest.Data != null)
				{
					message.Content = new StringContent(JsonConvert.SerializeObject(apiResquest.Data),
						Encoding.UTF8, "application/json");
					Logger.Info("Method: SendAsync - Params: Data: {0}", message.Content.ToString());
				}

				if (!string.IsNullOrEmpty(apiResquest.AccessToken))
				{
					Logger.Info("Method: SendAsync - Params: Token: {0}", apiResquest.AccessToken);
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiResquest.AccessToken);
				}

				if (!string.IsNullOrEmpty(apiResquest.ApiKey))
				{
					Logger.Info("Method: SendAsync - Params: ApiKey: {0}", apiResquest.ApiKey);
					client.DefaultRequestHeaders.Add("x-api-key", apiResquest.ApiKey);
				}

				HttpResponseMessage apiResponse = null;

				switch (apiResquest.apiType)
				{
					case Common.StaticDetails.ApiType.POST:
						message.Method = HttpMethod.Post;
						break;
					case Common.StaticDetails.ApiType.PUT:
						message.Method = HttpMethod.Put;
						break;
					case Common.StaticDetails.ApiType.DELETE:
						message.Method = HttpMethod.Delete;
						break;
					default:
						message.Method = HttpMethod.Get;
						break;
				}

				apiResponse = await client.SendAsync(message);



				var apiContent = await apiResponse.Content.ReadAsStringAsync();
				Logger.Info("Method: SendAsync - Params: apiContent: {0}", apiContent.ToString());

				if (apiResquest.IsReport)
				{
					var dto = new ResponseDto
					{
						Message = "Reporte Generado",
						IsSuccess = true,
						Code = 200,
						Data = "Success"
					};

					var res = JsonConvert.SerializeObject(dto);
					var apiResponseDtoReport = JsonConvert.DeserializeObject<T>(res);
					return apiResponseDtoReport;
				}

				var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);

				return apiResponseDto;

			}
			catch (Exception e)
			{
				Logger.Error("Method: Error SendAsync: {0}", e.Message.ToString());

				var dto = new ResponseDto
				{
					Message = e.Message.ToString(),
					ErrorMessages = new List<string> { Convert.ToString(e.Message) },
					IsSuccess = false,
					Code = 400
				};

				var res = JsonConvert.SerializeObject(dto);
				var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
				return apiResponseDto;

			}
		}

		public T Send<T>(ApiRequest apiResquest)
		{
			try
			{
				Logger.Info("Method: SendAsync - Params: Url: {0} - Tipo: {1}", apiResquest.url, apiResquest.apiType.ToString());
				var client = httpClient.CreateClient("Logistic");
				HttpRequestMessage message = new HttpRequestMessage();
				message.Headers.Add("Accept", "application/json");
				message.RequestUri = new Uri(apiResquest.url);
				client.DefaultRequestHeaders.Clear();
				if (apiResquest.Data != null)
				{
					message.Content = new StringContent(JsonConvert.SerializeObject(apiResquest.Data),
						Encoding.UTF8, "application/json");
					Logger.Info("Method: SendAsync - Params: Data: {0}", message.Content.ToString());
				}

				if (!string.IsNullOrEmpty(apiResquest.AccessToken))
				{
					Logger.Info("Method: SendAsync - Params: Token: {0}", apiResquest.AccessToken);
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiResquest.AccessToken);
				}

				if (!string.IsNullOrEmpty(apiResquest.ApiKey))
				{
					Logger.Info("Method: SendAsync - Params: ApiKey: {0}", apiResquest.ApiKey);
					client.DefaultRequestHeaders.Add("x-api-key", apiResquest.ApiKey);
				}

				HttpResponseMessage apiResponse = null;

				switch (apiResquest.apiType)
				{
					case Common.StaticDetails.ApiType.POST:
						message.Method = HttpMethod.Post;
						break;
					case Common.StaticDetails.ApiType.PUT:
						message.Method = HttpMethod.Put;
						break;
					case Common.StaticDetails.ApiType.DELETE:
						message.Method = HttpMethod.Delete;
						break;
					default:
						message.Method = HttpMethod.Get;
						break;
				}

				apiResponse = client.Send(message);



				var apiContent = apiResponse.Content.ReadAsStringAsync();
				Logger.Info("Method: SendAsync - Params: apiContent: {0}", apiContent.ToString());

				if (apiResquest.IsReport)
				{
					var dto = new ResponseDto
					{
						Message = "Reporte Generado",
						IsSuccess = true,
						Code = 200,
						Data = "Success"
					};

					var res = JsonConvert.SerializeObject(dto);
					var apiResponseDtoReport = JsonConvert.DeserializeObject<T>(res);
					return apiResponseDtoReport;
				}

				var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent.Result);

				return apiResponseDto;

			}
			catch (Exception e)
			{
				Logger.Error("Method: Error SendAsync: {0}", e.Message.ToString());

				var dto = new ResponseDto
				{
					Message = e.Message.ToString(),
					ErrorMessages = new List<string> { Convert.ToString(e.Message) },
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
	}
}
