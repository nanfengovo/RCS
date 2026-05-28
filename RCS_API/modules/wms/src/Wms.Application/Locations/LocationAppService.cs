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
using Volo.Abp.EventBus.Local;
using Wms.LocationDomainService;
using Wms.Locations;
using Wms.Locations.Dtos;
using Wms.Locations.Events;

namespace Wms.Localization
{
    public class LocationAppService : ApplicationService, ILocationAppService
    {
        private readonly IRepository<Location, Guid> _locationRepository;

        private readonly LocationManager _locationManager;

        // 注入多语言本地化器
        private readonly IStringLocalizer<WmsResource> _localizer;

        // 注入事件总线
        private readonly ILocalEventBus _localEventBus;

        public LocationAppService(IRepository<Location, Guid> locationRepository, LocationManager locationManager, IStringLocalizer<WmsResource> localizer, ILocalEventBus localEventBus)
        {
            _locationRepository = locationRepository;
            _locationManager = locationManager;
            _localizer = localizer;
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 批量修改库位的启用状态。它接受一个包含库位 ID 列表和目标状态的 DTO，验证输入，调用领域服务修改实体，并保存到数据库。
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
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

        /// <summary>
        /// 创建库位的应用服务方法。它调用了领域服务来执行业务逻辑，保存到数据库，并返回 DTO。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<LocationDto> CreateAsync(CreateLocationDto input)
        {
            //调用领域服务创建实体
            var location = await _locationManager.CreateLocationAsync(input.LocationCode, input.LocationType, input.ZoneId, input.Row, input.Column, input.Layer);

            //保存到数据库
            await _locationRepository.InsertAsync(location);

            //转换成 DTO 返回
            return ObjectMapper.Map<Location, LocationDto>(location);
        }

        /// <summary>
        /// 获取库位列表的应用服务方法。它从数据库查询库位，分页排序，并返回 DTO 列表。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 这是一个示例方法，演示了如何在业务逻辑中发布事件。当库位被锁定用于入库任务时，我们调用这个方法。它执行完业务逻辑后，通过事件总线发布一个 LocationLockedEto 事件，通知系统库位被锁定了。其他模块或组件可以订阅这个事件来做相应的处理，比如记录日志、更新状态等。
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public async Task LockForInboundAsync(Guid locationId, Guid taskId)
        {
            // 1. 执行纯粹的业务逻辑
            // ...

            // 2. 业务做完了，对着系统大喊一声：“库位被锁定了！谁关心谁去处理！”
            await _localEventBus.PublishAsync(new LocationLockedEto
            {
                LocationId = locationId,
                TaskId = taskId,
                OccurredTime = DateTime.Now
            });
        }
    }
}