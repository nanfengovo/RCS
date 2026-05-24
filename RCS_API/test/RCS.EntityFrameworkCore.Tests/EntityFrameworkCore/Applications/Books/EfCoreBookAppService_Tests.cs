using RCS.Books;
using Xunit;

namespace RCS.EntityFrameworkCore.Applications.Books;

[Collection(RCSTestConsts.CollectionDefinitionName)]
public class EfCoreBookAppService_Tests : BookAppService_Tests<RCSEntityFrameworkCoreTestModule>
{

}