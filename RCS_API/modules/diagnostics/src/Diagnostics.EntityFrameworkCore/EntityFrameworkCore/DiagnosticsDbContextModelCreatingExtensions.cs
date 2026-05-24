using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Diagnostics.EntityFrameworkCore;

public static class DiagnosticsDbContextModelCreatingExtensions
{
    public static void ConfigureDiagnostics(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(DiagnosticsDbProperties.DbTablePrefix + "Questions", DiagnosticsDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
    }
}
