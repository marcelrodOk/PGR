namespace Common
{
	public class JsonDataResult
	{
		public bool IsSuccess { get; set; } = true;
		public int Code { get; set; }
		public string Message { get; set; } = "";
		public string Token { get; set; }
		public object Data { get; set; }
	}
}
