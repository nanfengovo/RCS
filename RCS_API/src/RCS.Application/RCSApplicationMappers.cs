using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using RCS.Books;

namespace RCS;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RCSBookToBookDtoMapper : MapperBase<Book, BookDto>
{
    public override partial BookDto Map(Book source);

    public override partial void Map(Book source, BookDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RCSCreateUpdateBookDtoToBookMapper : MapperBase<CreateUpdateBookDto, Book>
{
    public override partial Book Map(CreateUpdateBookDto source);

    public override partial void Map(CreateUpdateBookDto source, Book destination);
}
