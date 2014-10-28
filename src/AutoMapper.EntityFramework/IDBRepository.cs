using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AutoMapper.EntityFramework
{
    public static class Extensions
    {
        public static IPersistance Persist<TSource>(this DbSet<TSource> source)
            where TSource : class
        {
            return new Persistance<TSource>(source, Mapper.Engine);
        }
    }

    public interface IPersistance
    {
        void From<TFrom>(TFrom from) where TFrom : class;
        void Remove<TFrom>(TFrom from) where TFrom : class;
    }

    public class Persistance<TTo> : IPersistance
        where TTo : class
    {
        private readonly DbSet<TTo> _sourceSet;
        private readonly IMappingEngine _mappingEngine;

        public Persistance(DbSet<TTo> sourceSet, IMappingEngine mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _sourceSet = sourceSet;
        }

        public void From<TFrom>(TFrom from)
            where TFrom : class
        {
            var equivExpr = Mapper.Map<TFrom,Expression<Func<TTo, bool>>>(from);
            if (equivExpr == null)
                return;

            var to = _sourceSet.FirstOrDefault(equivExpr);

            if (to == null)
            {
                to = _sourceSet.Create<TTo>();
                _sourceSet.Add(to);
            }
            Mapper.Map(from,to);
        }

        public void Remove<TFrom>(TFrom from)
            where TFrom : class
        {
            var equivExpr = Mapper.Map<TFrom, Expression<Func<TTo, bool>>>(from);
            if (equivExpr == null)
                return;
            var to = _sourceSet.FirstOrDefault(equivExpr);

            if (to != null)
                _sourceSet.Remove(to);
        }
    }
}