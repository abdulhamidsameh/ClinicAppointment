namespace ClinicAppointment.BLL.Specifications;
public interface ISpecifications<T> where T : BaseEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; }
    public Expression<Func<T, object>> OrderByDesc { get; set; }
    public Expression<Func<T, object>> OrderBy { get; set; }
    public List<Expression<Func<T, object>>> Includes { get; set; }
}