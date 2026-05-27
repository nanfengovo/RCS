using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Wms.Locations.Dtos;

namespace Wms.Locations
{
    public interface ILocationAppService : IApplicationService
    {
        Task<PagedResultDto<LocationDto>> GetListAsync(PagedAndSortedResultRequestDto input);

        Task<LocationDto> CreateAsync(CreateLocationDto input);

        Task BatchChangeActiveStatusAsync(BatchChangeLocationActiveDto dto);
    }
}