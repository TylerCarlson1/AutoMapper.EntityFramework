using System.Data.Entity.Infrastructure;
using AutoMapper.EquivilencyExpression;

namespace AutoMapper.EntityFramework
{
    public class GenerateEntityFrameworkPrimaryKeyEquivilentExpressions<TDatabaseContext> : GenerateEquivilentExpressionsBasedOnGeneratePropertyMaps
        where TDatabaseContext : IObjectContextAdapter, new() 
    {
        public GenerateEntityFrameworkPrimaryKeyEquivilentExpressions()
            : base(new GenerateEntityFrameworkPrimaryKeyPropertyMaps<TDatabaseContext>())
        {
        }
    }
}