using E_Learning.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning.Data.Configrations
{
    public class RatingConfige : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("Rating");
            builder.HasKey(x => x.id);
            builder.HasIndex(x => new { x.StudentId, x.CourseId }).IsUnique();

            builder.HasOne(x => x.Student)
                .WithMany(y => y.Ratings)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
