using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionBuilder
{
    public static class StringExtensions
    {
        public static String ToSqlString(this String value)
        {
            return value.Replace("'", "''");
        }
    }

    public static class ObjectExtensions
    {
        //public static Object ToSqlValue(this Int16? value) { return value == null ? DBNull.Value : (Object)value; }
        public static Object ToSqlValue(this Int32? value) { return value == null ? DBNull.Value : (Object)value; }
        public static Object ToSqlValue(this Int64? value) { return value == null ? DBNull.Value : (Object)value; }
        public static Object ToSqlValue(this String value) { return value == null ? DBNull.Value : (Object)value; }

        //public static SqlParameter ToSqlParameter(this Int16? value, String field, Boolean nullable)
        //{
        //    if (value.HasValue) { return new SqlParameter(field, SqlDbType.Int) { Value = value, IsNullable = nullable }; }
        //    return new SqlParameter(field, SqlDbType.Int) { Value = DBNull.Value, IsNullable = nullable };
        //}
        public static SqlParameter ToSqlParameter(this Int32? value, String field, Boolean nullable)
        {
            if (value.HasValue) { return new SqlParameter(field, SqlDbType.Int) { Value = value, IsNullable = nullable }; }
            return new SqlParameter(field, SqlDbType.Int) { Value = DBNull.Value, IsNullable = nullable };
        }
        public static SqlParameter ToSqlParameter(this Int64? value, String field, Boolean nullable)
        {
            if (value.HasValue) { return new SqlParameter(field, SqlDbType.BigInt) { Value = value, IsNullable = nullable }; }
            return new SqlParameter(field, SqlDbType.BigInt) { Value = DBNull.Value, IsNullable = nullable };
        }
        public static SqlParameter ToSqlParameter(this String value, String field, Boolean nullable)
        {
            if (value == null) { return new SqlParameter(field, SqlDbType.NVarChar) { Value = DBNull.Value, IsNullable = nullable }; }
            return new SqlParameter(field, SqlDbType.NVarChar) { Value = value, IsNullable = nullable };
        }
        public static SqlParameter ToSqlParameter(this String value, String field, Int32 size, Boolean nullable)
        {
            if (value == null) { return new SqlParameter(field, SqlDbType.NVarChar) { Value = DBNull.Value, IsNullable = nullable, Size = size }; }
            return new SqlParameter(field, SqlDbType.NVarChar) { Value = value, IsNullable = nullable, Size = size };
        }
        public static SqlParameter ToSqlParameter(this String value, String field, SqlDbType type, Int32 size, Boolean nullable)
        {
            if (value == null) { return new SqlParameter(field, type) { Value = DBNull.Value, IsNullable = nullable, Size = size }; }
            return new SqlParameter(field, type) { Value = value, IsNullable = nullable, Size = size };
        }
    }
}