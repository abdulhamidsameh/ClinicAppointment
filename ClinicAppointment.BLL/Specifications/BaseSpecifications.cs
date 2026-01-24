namespace ClinicAppointment.BLL.Specifications;
public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; } = null!;
    public Expression<Func<T, object>> OrderByDesc { get; set; } = null;
    public Expression<Func<T, object>> OrderBy { get; set; } = null;
    public List<Expression<Func<T, object>>> Includes { get; set; } = new();

    public BaseSpecifications()
        : this(null!)
    {
    }

    public BaseSpecifications(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }
    public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDesc = orderByDescExpression;
    }
    public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderByDesc = orderByExpression;
    }

}