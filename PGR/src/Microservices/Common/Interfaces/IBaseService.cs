namespace Common.Interfaces
{
	public interface IBaseService
	{
		public interface IBaseService : IDisposable
		{
			ResponseDto responseModel { get; set; }

			Task<T> SendAsync<T>(ApiRequest apiResquest);
		}
	}
}
