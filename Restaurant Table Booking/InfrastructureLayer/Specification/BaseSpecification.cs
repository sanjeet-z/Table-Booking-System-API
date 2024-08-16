using Restaurant_Table_Booking.Application.ISpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Table_Booking.Infrastructure.Specification
{
    public abstract class BaseSpecification<T> : ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>>?Criteria {get;}
        protected BaseSpecification(Expression<Func<T, bool>>? criteria)
        {
            Criteria = criteria;
        }

        protected BaseSpecification()
        {
            
        }
        public List<Expression<Func<T, object>>> Includes { get; } = new();

        public List<string> IncludeStrings { get; } = new();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDecending { get; private set; }

        public Expression<Func<T, object>> GroupBy { get; private set; }

        public int? Take { get; private set; }

        public int? Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; } = false;

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
        protected virtual void ApplyPaging(int? take, int? skip)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
        protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        protected virtual void ApplyOrderByDecending(Expression<Func<T, object>> orderByDecendingExpression)
        {
            OrderByDecending = orderByDecendingExpression;
        }
        protected virtual void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
        {
            GroupBy = groupByExpression;
        }

    }
}
