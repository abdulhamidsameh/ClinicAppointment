namespace ClinicAppointment.DAL.Data.Configrations;
internal class AppointmentConfigrations : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.Property(P => P.Id).UseIdentityColumn(1, 1);
        builder.Property(P => P.VisitMinutes).HasDefaultValue(30);
        builder.Property(P => P.Status).HasDefaultValue(AppointmentStatus.Booked).HasConversion
            (
                (AppointmentStatus) => AppointmentStatus.ToString(),
                (AppointmentStatusAsString) => (AppointmentStatus)Enum.Parse(typeof(AppointmentStatus), AppointmentStatusAsString, true)
            );
        builder.HasIndex(P => new { P.DoctorId, P.StartDateTime });
    }
}