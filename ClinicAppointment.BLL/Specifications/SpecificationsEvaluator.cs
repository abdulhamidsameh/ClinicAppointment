using System.Linq;

namespace ClinicAppointment.BLL.Specifications;
internal static class SpecificationsEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> spec)
    {
        var query = inputQuery;

        if (spec.Criteria is not null)
            query = query.Where(spec.Criteria);

        if (spec.Includes.Count > 0)
            foreach (var include in spec.Includes)
                query = query.Include(include);

        return query;
    }
}