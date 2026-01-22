namespace ClinicAppointment.BLL.Specifications;
public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; } = null!;
    public List<Expression<Func<T, object>>> Includes { get; set; } = new();

    public BaseSpecifications()
        : this(null!)
    {
    }

    public BaseSpecifications(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

}