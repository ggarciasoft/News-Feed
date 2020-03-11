using NewsFeed.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace NewsFeed.Data.Mapping
{
    public class CategoryMapping : EntityTypeConfiguration<Models.Category>
    {
        public CategoryMapping()
        {
            //Mapping created to generate migration without defining all class information if not necessary.
            HasKey(o => o.ID);

            Property(o => o.Description).IsRequired().HasMaxLength(50);

            HasMany(o => o.NewsFeeds).WithRequired(o => o.Category).HasForeignKey(o => o.CategoryID);
        }
    }
}
