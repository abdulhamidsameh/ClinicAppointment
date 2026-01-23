namespace ClinicAppointment.PL.Extensions;
public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationsService(this IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
        return services;
    }
}