namespace ORMLib.MSSql;

using Providers;

public class MSSqlProvider : IAccessProvider
{
    public Access Provide()
        => new SqlAccess();
}