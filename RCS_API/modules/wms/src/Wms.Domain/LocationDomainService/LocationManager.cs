using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Wms.Locations;

namespace Wms.LocationDomainService
{
    public class LocationManager : DomainService
    {
        private readonly IRepository<Location, Guid> _locationRepository;

        public LocationManager(IRepository<Location, Guid> locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Location> CreateLocationAsync(string locationCode, LocationType locationType, Guid zoneId, int row, int column, int layer)
        {
            //检查 locationCode 是否唯一
            var existingLocation = await _locationRepository.FirstOrDefaultAsync(x => x.LocationCode == locationCode);
            if (existingLocation != null)
            {
                throw new BusinessException(WmsErrorCodes.LocationCodeAlreadyExists)
                    .WithData("LocationCode", locationCode)
                    .WithData("ExistingLocationId", existingLocation.Id);
            }

            // 组装值对象
            var coordinate = new LocationCoordinate(row, column, layer);

            // 返回实体
            return new Location(
                GuidGenerator.Create(),
                locationCode,
                locationType,
                zoneId,
                coordinate,
                LocationStatus.Empty
            );

        }
    }
}