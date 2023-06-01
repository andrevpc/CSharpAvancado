namespace ORMLib;

using MSSql;

public static class ObjectRelationalMappingConfigBuilderExtension
{
    public static ObjectRelationalMappingConfigBuilder UseMSSqlServer(this ObjectRelationalMappingConfigBuilder builder)
    {
        builder.SetDataBaseSystem(DataBaseSystem.SqlServer);
        builder.SetQueryProvider(new MSSqlQueryProvider());
        builder.SetAccessProvider(new MSSqlProvider());
        return builder;
    }
}