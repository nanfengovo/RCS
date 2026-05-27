using System.Collections.Generic;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using Wms.Locations;
using Wms.Locations.Dtos;

namespace Wms;

/*
Write your mappings here...

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class WmsApplicationMappers : MapperBase<Book, BookDto>
{
    public override partial BookDto Map(Book source);

    public override partial void Map(Book source, BookDto destination);
}
*/

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class LocationMapper : MapperBase<Location, LocationDto>
{
    // 魔法就在这里：你只需要写 partial 方法签名，不需要写方法体！
    // 编译器会自动在后台生成 return new LocationDto { LocationCode = source.LocationCode ... }
    public override partial LocationDto Map(Location source);

    public override partial void Map(Location source, LocationDto destination);

    public partial List<LocationDto> MapList(List<Location> source);
}