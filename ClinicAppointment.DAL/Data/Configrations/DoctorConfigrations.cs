namespace ClinicAppointment.DAL.Data.Configrations;
internal class DoctorConfigrations : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.Property(P => P.Id).UseIdentityColumn(1, 1);
        builder.Property(P => P.FullName).HasColumnType("varchar").HasMaxLength(50).IsRequired();
    }
}