using System;
namespace ORMLib.DataAnnotations;
// No C# 11.0 em diante poderemos usar Atributos Genéricos
public class ForeignKeyAttribute : Attribute
{
    public Type ForeignTable { get; set; }
    public ForeignKeyAttribute(Type foreignTable)
        => this.ForeignTable = foreignTable;
}