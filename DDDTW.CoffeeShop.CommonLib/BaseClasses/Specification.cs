using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DDDTW.CoffeeShop.CommonLib.BaseClasses
{
    public class Specification<T>
    {
        protected Specification()
        {
        }

        public Specification(Expression<Func<T, bool>> predicate)
        {
            this.Predicate = predicate;
        }

        public Specification(T entity, Expression<Func<T, bool>> predicate)
        {
            this.Entity = entity;
            this.Predicate = predicate;
        }

        public Expression<Func<T, bool>> Predicate { get; protected set; }

        public T Entity { get; protected set; }

        public static Specification<T> operator &(Specification<T> left, Specification<T> right)
        {
            Expression<Func<T, bool>> leftPredicate = left.Predicate;
            Expression<Func<T, bool>> rightPredicate = right.Predicate;

            BinaryExpression andAlsoExpression =
                Expression.AndAlso(
                    leftPredicate.Body,
                    new ParameterExpressionReWriter(leftPredicate.Parameters, rightPredicate.Parameters).Visit(rightPredicate.Body));

            Expression<Func<T, bool>> predicateExpression =
                Expression.Lambda<Func<T, bool>>(andAlsoExpression, leftPredicate.Parameters);

            return new Specification<T>(predicateExpression);
        }

        public static Specification<T> operator |(Specification<T> left, Specification<T> right)
        {
            Expression<Func<T, bool>> leftPredicate = left.Predicate;
            Expression<Func<T, bool>> rightPredicate = right.Predicate;

            BinaryExpression orElseExpression =
                Expression.OrElse(
                    leftPredicate.Body,
                    new ParameterExpressionReWriter(leftPredicate.Parameters, rightPredicate.Parameters).Visit(rightPredicate.Body));

            Expression<Func<T, bool>> predicateExpression =
                Expression.Lambda<Func<T, bool>>(orElseExpression, left.Predicate.Parameters.Single());

            return new Specification<T>(predicateExpression);
        }

        public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
        {
            return specification.Predicate;
        }

        public bool IsSatisfy()
        {
            return this.Predicate.Compile()(this.Entity);
        }
    }

    public class ParameterExpressionReWriter : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> parameterExpressionMap;

        public ParameterExpressionReWriter(IEnumerable<ParameterExpression> firstParams, IEnumerable<ParameterExpression> secondParams)
        {
            this.parameterExpressionMap =
                firstParams
                    .Zip(secondParams, (firstParam, secondParam) => new { firstParam, secondParam })
                    .ToDictionary(key => key.secondParam, value => value.firstParam);
        }

        protected override Expression VisitParameter(ParameterExpression parameterExpression)
        {
            if (this.parameterExpressionMap.TryGetValue(parameterExpression, out var replacement))
            {
                parameterExpression = replacement;
            }

            return base.VisitParameter(parameterExpression);
        }
    }
}