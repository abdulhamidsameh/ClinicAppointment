namespace ClinicAppointment.DAL.Data.Configrations;
internal class ClinicConfigrations : IEntityTypeConfiguration<Clinic>
{
    public void Configure(EntityTypeBuilder<Clinic> builder)
    {
        builder.Property(P => P.Id).UseIdentityColumn(1, 1);
        builder.Property(P => P.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
    }
}