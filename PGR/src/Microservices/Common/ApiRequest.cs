using static Common.StaticDetails;

namespace Common
{
	public class ApiRequest
	{
		public ApiType apiType { get; set; } = ApiType.GET;
		public string url { get; set; }
		public object Data { get; set; }
		public string AccessToken { get; set; }
		public string ApiKey { get; set; }
		public Boolean IsReport { get; set; } = false;
	}
}
