using Common;

namespace Processes.Web.Microservices.Interfaces
{
	public interface ILoteClosureService
	{
		Task<ResponseDto> GetLoteClosure<T>();
	}
}
