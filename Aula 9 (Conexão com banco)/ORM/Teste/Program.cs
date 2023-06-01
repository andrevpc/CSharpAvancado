using static System.Console;

using ORMLib;
using ORMLib.Linq;
using ORMLib.DataAnnotations;

var builder = ObjectRelationalMappingConfig.GetBuilder();
builder
    .UseMSSqlServer()
    .SetDataSource(@"CT-C-0013J\SQLEXPRESS01")
    .SetInitialCatalog("MyDatabaseTest")
    .SetIntegratedSecurity(true)
    .Build()
    .Use();
while (true)
{
    string command = ReadLine();
    switch (command.ToLower())
    {
        case "add marca":
            Marca marca = new Marca();
            Write("Nome: ");
            marca.Nome = ReadLine();
            await marca.Save();
            break;
        case "add produto":
            Produto produto = new Produto();
            Write("Nome: ");
            produto.Nome = ReadLine();
            Write("Marca: ");
            var marcaNome = ReadLine();
            var marcasSelecionadas = await Marca.All
                .Where(m => m.Nome == "Bosch")
                .ToListAsync();
            var marcaSelecionada = marcasSelecionadas[0];
            produto.MarcaID = marcaSelecionada.ID;
            await produto.Save();
            break;
        case "view produto":
            var produtos = await Produto.All
            .Select(m => m.Nome)
            .ToListAsync();
            foreach (var nome in produtos)
            {
                WriteLine(nome);
            }
            break;
        case "view marca":
            var marcas = await Marca.All
            .Select(m => m.Nome)
            .ToListAsync();
            foreach (var nome in marcas)
            {
                WriteLine(nome);
            }
            break;
        case "exit":
            return;
    }
}
public class Anuncio : Table<Anuncio>
{
    [PrimaryKey]
    public int ID { get; set; }
    [NotNull]
    public string Titulo { get; set; }
    [NotNull]
    public decimal Preco { get; set; }
    [NotNull]
    [ForeignKey(typeof(Vendedor))]
    public int VendedorID { get; set; }
    [NotNull]
    [ForeignKey(typeof(Produto))]
    public int ProdutoID { get; set; }
}

public class Marca : Table<Marca>
{
    [PrimaryKey]
    public int ID { get; set; }
    [NotNull]
    public string Nome { get; set; }
}
public class Vendedor : Table<Vendedor>
{
    [PrimaryKey]
    public int ID { get; set; }
    public string Nome { get; set; }
    [NotNull]
    public string Login { get; set; }
    [NotNull]
    public string Senha { get; set; }
    public string CPF { get; set; }
    public string PIX { get; set; }
}
public class Produto : Table<Produto>
{
    [PrimaryKey]
    public int ID { get; set; }
    [NotNull]
    public string Nome { get; set; }
    [ForeignKey(typeof(Marca))]
    public int MarcaID { get; set; }
}