namespace ClinicAppointment.PL.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<DoctorViewModel, Doctor>().ReverseMap();
        CreateMap<ClinicViewModel, Clinic>().ReverseMap();
        CreateMap<PatientViewModel, Patient>().ReverseMap();
    }
}
