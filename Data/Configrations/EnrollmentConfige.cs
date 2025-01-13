using E_Learning.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning.Data.Configrations
{
    public class EnrollmentConfige : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(x => new { x.studentId, x.courseId });

            builder.ToTable("Enrollments");
        }
    }
}
