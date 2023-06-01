using System;

namespace ORMLib.Exceptions;

public class ConfigNotInitializedException : Exception
{
    public override string Message => "ORM config is not initializated";
}