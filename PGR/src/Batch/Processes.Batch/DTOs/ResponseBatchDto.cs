namespace Processes.Batch.DTOs
{
	public class ResponseBatchDto
	{
		public bool IsSuccess { get; set; }
		public int Code { get; set; }
		public object Data { get; set; }
		public string Message { get; set; }
		public List<string> ErrorMessages { get; set; }
	}
}
