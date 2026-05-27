using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Wms.LocationDomainService;
using Wms.Locations;
using Wms.Locations.Dtos;

namespace Wms.Localization
{
    public class LocationAppService : ApplicationService, ILocationAppService
    {
        private readonly IRepository<Location, Guid> _locationRepository;

        private readonly LocationManager _locationManager;

        // 注入多语言本地化器
        private readonly IStringLocalizer<WmsResource> _localizer;

        public LocationAppService(IRepository<Location, Guid> locationRepository, LocationManager locationManager, IStringLocalizer<WmsResource> localizer)
        {
            _locationRepository = locationRepository;
            _locationManager = locationManager;
            _localizer = localizer;
        }

        public async Task BatchChangeActiveStatusAsync(BatchChangeLocationActiveDto dto)
        {
            if (dto.LocationIds == null || !dto.LocationIds.Any())
            {
                // 使用本地化器翻译错误信息，并抛出用户友好异常
                throw new UserFriendlyException(_localizer["Wms:EmptyLocationIdList"]);
            }

            //去重
            var uniqueIds = dto.LocationIds.Distinct().ToList();
            var locations = await _locationRepository.GetListAsync(x => uniqueIds.Contains(x.Id));
            // 一个都没有
            if (locations.Count == 0)
            {
                throw new UserFriendlyException(_localizer["Wms:NotFoundAnyLocations"]);
            }

            if (locations.Count != uniqueIds.Count)
            {
                var foundIds = locations.Select(x => x.Id).ToHashSet();
                var notFoundIds = uniqueIds.Where(id => !foundIds.Contains(id));
                throw new UserFriendlyException(_localizer["Wms:SomeLocationsNotFound", string.Join(", ", notFoundIds)]);
            }

            foreach (var location in locations)
            {
                location.ChangeLocationAble(dto.IsActive);
            }
            await _locationRepository.UpdateManyAsync(locations);
        }

        public async Task<LocationDto> CreateAsync(CreateLocationDto input)
        {
            //调用领域服务创建实体
            var location = await _locationManager.CreateLocationAsync(input.LocationCode, input.LocationType, input.ZoneId, input.Row, input.Column, input.Layer);

            //保存到数据库
            await _locationRepository.InsertAsync(location);

            //转换成 DTO 返回
            return ObjectMapper.Map<Location, LocationDto>(location);
        }

        public async Task<PagedResultDto<LocationDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var query = await _locationRepository.GetQueryableAsync();

            var totalCount = await AsyncExecuter.CountAsync(query);

            var queryResult = query
                .OrderBy(x => x.LocationCode)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var locations = await AsyncExecuter.ToListAsync(queryResult);

            var dtos = ObjectMapper.Map<List<Location>, List<LocationDto>>(locations);

            return new PagedResultDto<LocationDto>(totalCount, dtos);
        }
    }
}