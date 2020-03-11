using NewsFeed.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace NewsFeed.Data.Mapping
{
    public class NewsFeedMapping : EntityTypeConfiguration<Models.NewsFeed>
    {
        public NewsFeedMapping()
        {
            //Mapping created to generate migration without defining all class information if not necessary.
            HasKey(o => o.ID);

            Property(o => o.FeedName).IsRequired().HasMaxLength(50);
            Property(o => o.FeedUrl).IsRequired().HasMaxLength(255);
            Property(o => o.Subscribed).IsRequired();

            HasRequired(o => o.Category).WithMany(o => o.NewsFeeds).HasForeignKey(o => o.CategoryID);
        }
    }
}
