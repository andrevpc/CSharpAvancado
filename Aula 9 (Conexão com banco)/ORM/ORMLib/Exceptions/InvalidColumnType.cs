using System;

namespace ORMLib.Exceptions;

public class InvalidColumnType : Exception
{
    string type;
    public InvalidColumnType(Type type)
        => this.type = type.Name;
    public override string Message => $"O tipo {type} não é valido para uma coluna no banco de dados.";
}