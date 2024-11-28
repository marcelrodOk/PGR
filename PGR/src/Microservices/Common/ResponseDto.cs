namespace Common
{
	public class ResponseDto
	{
		public bool IsSuccess { get; set; } = true;
		public int Code { get; set; }
		public object Data { get; set; }
		public string Message { get; set; }
		public string Token { get; set; }
		public bool Status { get; set; }
		public string Password { get; set; }
		public List<string> ErrorMessages { get; set; }
	}
}
