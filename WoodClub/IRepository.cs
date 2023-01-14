using System;
using System.Linq.Expressions;

namespace WoodClub
{
    public interface IRepository
    {
        void Update<T>(T obj, params Expression<Func<T, object>>[] propertiesToUpdate) where T : class;
    }
}
