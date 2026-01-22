namespace ClinicAppointment.DAL.Data.Configrations;
internal class PatientConfigrations : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.Property(P => P.Id).UseIdentityColumn(1, 1);
        builder.Property(P => P.FullName).HasColumnType("varchar").HasMaxLength(50).IsRequired();
        builder.Property(P => P.BirthDate).HasConversion(V => V.ToDateTime(TimeOnly.MinValue), V => DateOnly.FromDateTime(V));
    }
}