namespace ClinicAppointment.DAL.Data.Configrations;
internal class DoctorScheduleRuleConfigrations : IEntityTypeConfiguration<DoctorScheduleRule>
{
    public void Configure(EntityTypeBuilder<DoctorScheduleRule> builder)
    {
        builder.Property(P => P.Id).UseIdentityColumn(1, 1);
        builder.Property(P => P.DayOfWeek).HasConversion<int>();
    }
}