using LoteClosureServices.Business.DTOs.LoteClousureDTOs.ResponseDTO;

namespace LoteClosureServices.Business.Interfaces
{
	public interface ILoteClosureService
	{
		Task<IEnumerable<TblIntermediaResponseDTO>> GetLoteClosure<T>();
	}
}
