using E_Learning.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning.Data.Configrations
{
    public class CourseConfige : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Course");
            builder.HasKey(x => x.Id).IsClustered();
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasIndex(x => x.category);
            builder.HasIndex(x => x.level);
            builder.HasIndex(x => x.title);


            builder.HasOne(x => x.instracture)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.instractureId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.HasMany(x => x.students)
                .WithMany(x => x.Courses)
                .UsingEntity<Enrollment>();

            builder.HasMany(x => x.Ratings)
              .WithOne(y => y.Course)
              .HasForeignKey(x => x.CourseId)
              .OnDelete(DeleteBehavior.Cascade)
              .IsRequired(true);

            builder.HasMany(x => x.lessons)
             .WithOne(y => y.course)
             .HasForeignKey(x => x.courseId)
             .OnDelete(DeleteBehavior.Cascade)
             .IsRequired(true);

        }
    }
}
