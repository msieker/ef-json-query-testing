using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace ef_json_query_testing.Translators
{
#pragma warning disable EF1001
    public sealed class JsonQueryTranslatorPlugin : SqlServerMethodCallTranslatorProvider
#pragma warning restore EF1001
    {
        public JsonQueryTranslatorPlugin(RelationalMethodCallTranslatorProviderDependencies dependencies)
#pragma warning disable EF1001
            : base(dependencies)
#pragma warning restore EF1001
        {
            ISqlExpressionFactory expressionFactory = dependencies.SqlExpressionFactory;

            AddTranslators(new List<IMethodCallTranslator>
            {
                new QueryJsonTranslator(expressionFactory)
            });
        }
    }

    public class SqlServerDbContextOptionsExtension : IDbContextOptionsExtension
    {
        private DbContextOptionsExtensionInfo _info;

        public void Validate(IDbContextOptions options)
        {
        }

        public DbContextOptionsExtensionInfo Info
        {
            get
            {
                return this._info ??= new MyDbContextOptionsExtensionInfo(this);
            }
        }

        void IDbContextOptionsExtension.ApplyServices(IServiceCollection services)
        {
            // this does not work and I don't know why
            //services.TryAddSingleton<IMethodCallTranslatorProvider, CustomSqlServerMethodCallTranslatorPlugin>(); 
            
            services.AddScoped<IMethodCallTranslatorProvider, JsonQueryTranslatorPlugin>();
        }

        private sealed class MyDbContextOptionsExtensionInfo : DbContextOptionsExtensionInfo
        {
            public MyDbContextOptionsExtensionInfo(IDbContextOptionsExtension instance) : base(instance) { }

            public override bool IsDatabaseProvider => false;

            public override string LogFragment => "";

            public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            {
            }

            public override int GetServiceProviderHashCode()
            {
                return 0;
            }

            public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other) => false;
        }
    }
    public static class SqlServerDbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder UseJsonFunctions(
            this DbContextOptionsBuilder optionsBuilder)
        {
            var extension = GetOrCreateExtension(optionsBuilder);
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            return optionsBuilder;
        }

        private static SqlServerDbContextOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.Options.FindExtension<SqlServerDbContextOptionsExtension>()
               ?? new SqlServerDbContextOptionsExtension();
    }
}
