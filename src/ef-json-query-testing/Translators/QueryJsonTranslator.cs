using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.SqlServer.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace ef_json_query_testing.Translators
{
    public static class QueryJsonExtensions
    {
        // Pattern stolen from https://github.com/dotnet/efcore/blob/main/src/EFCore/DbFunctionsExtensions.cs
        public static string JsonValue(this DbFunctions _, object jsonField, string jsonKey) => throw new InvalidOperationException();
    }

    public static class QueryConversionExtensions
    {
        // Pattern stolen from https://github.com/dotnet/efcore/blob/main/src/EFCore/DbFunctionsExtensions.cs
        public static DateTime DateConvert(this DbFunctions _, string convertDate) => throw new InvalidOperationException();
    }

    public class QueryJsonTranslator : IMethodCallTranslator
    {
        private readonly ISqlExpressionFactory _expressionFactory;

        private static readonly MethodInfo _jsonValueMethod
            = typeof(QueryJsonExtensions).GetMethod(
                nameof(QueryJsonExtensions.JsonValue),
                new[] { typeof(DbFunctions), typeof(object), typeof(string) })!;


        private static readonly MethodInfo _dateConvertMethod
            = typeof(QueryConversionExtensions).GetMethod(
                nameof(QueryConversionExtensions.DateConvert),
                new[] { typeof(DbFunctions), typeof(string) })!;

        public QueryJsonTranslator(ISqlExpressionFactory expressionFactory)
        {
            _expressionFactory = expressionFactory;
        }

        public SqlExpression? Translate(SqlExpression? instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments, IDiagnosticsLogger<DbLoggerCategory.Query> logger)
        {
            if (method == _jsonValueMethod)
            {
                var propertyReference = arguments[1];
                if (propertyReference is not ColumnExpression)
                {
                    throw new InvalidOperationException("jsonField must be a property reference");
                }

                var typeMapping = propertyReference.TypeMapping;
                var freeText = propertyReference.Type == arguments[2].Type
                    ? _expressionFactory.ApplyTypeMapping(arguments[2], typeMapping)
                    : _expressionFactory.ApplyDefaultTypeMapping(arguments[2]);

                var functionArguments = new List<SqlExpression> { propertyReference, freeText };

                return _expressionFactory.Function("JSON_VALUE",
                    functionArguments,
                    nullable: true,
                    argumentsPropagateNullability: functionArguments.Select(_ => false).ToList(),
                    typeof(string));
            }
            else if (method == _dateConvertMethod)
            {
                //CONVERT(dataType, valueToConvert, ConvertStyle)

                var sqlType = _expressionFactory.Constant(DbType.DateTime2);
                var sqlConvertStyle = _expressionFactory.Constant(127);

                var functionArguments = new List<SqlExpression> { sqlType, arguments[1], sqlConvertStyle };

                var thing =  _expressionFactory.Function("CONVERT",
                    functionArguments,
                    nullable: true,
                    argumentsPropagateNullability: functionArguments.Select(_ => false).ToList(),
                    typeof(string));

                return thing;
            }

            return null;

        }
    }
}
