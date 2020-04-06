using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BMS.Common
{
    /// <summary>
    /// Paged List should be Used on Service if Data is allways need to get from DataBase Directly.
    /// if Data need to be chached then Paged List should be used on Controller.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T>
    {
        public List<T> List { get; set; }

        public int PageCount
        {
            get;
            set;
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int ItemCount { get; set; }

        public PagedList(IQueryable<T> list, int pageIndex, int? pageSize, string[] SortDirections, string[] SortNames)
        {
            PageSize = pageSize != null ? Convert.ToInt32(pageSize) : 10;
            if (SortDirections != null && SortDirections.Length > 0)
            {
                for (int i = 0; i < SortDirections.Length; i++)
                {
                    //list = OrderBy(list, SortNames[i], SortDirections[i] == "asc" ? "OrderBy" : "OrderByDescending");
                    string methodname = i == 0 ? "OrderBy" : "ThenBy";
                    methodname += SortDirections[i] == "desc" ? "Descending" : "";
                    if (!string.IsNullOrEmpty(SortNames[i]))
                        list = ApplyOrder<T>(list, SortNames[i], methodname);
                }
            }
            List = list.Skip((pageIndex - 1) * PageSize).Take(PageSize).ToList();
            PageIndex = pageIndex;
            PageSize = PageSize;
            PageCount = (int)Math.Ceiling((float)list.Count() / Convert.ToInt32(pageSize));
            ItemCount = list.Count();
        }
        //public IQueryable<T> OrderBy<T>(IQueryable<T> source, string ordering, string orderby)
        //{
        //    var type = typeof(T);
        //    var property = type.GetProperty(ordering);
        //    var parameter = Expression.Parameter(type, "p");
        //    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        //    var orderByExp = Expression.Lambda(propertyAccess, parameter);
        //    MethodCallExpression resultExp = Expression.Call(typeof(Queryable), orderby, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
        //    return source.Provider.CreateQuery<T>(resultExp);
        //}
        public IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }
    }
}
