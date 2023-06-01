using System;
using System.Linq;
using System.Threading.Tasks;

using EntityTest.Model;

TestentityContext context = new TestentityContext();

await createUser("Don");
await createUser("Marcão");
await createUser("Queila Lima");
await createUser("Pamella");
await createProduct("Bico", "O máximo do avanço tecnológico em mecânica.");
await createProduct("Pudim", "O máximo do avanço tecnológico em gastronomia.");

var users =
    from user in context.Usuarios
    where user.Nome == "Queila Lima"
    select user;

var queila = users.FirstOrDefault();

var pudim = context.Produtos
    .FirstOrDefault(x => x.Nome == "Pudim");

await addOfertum(pudim, queila, 10);

printData();

async Task addOfertum(Produto produto, Usuario usuario, decimal value)
{
    Ofertum oferta = new Ofertum();

    oferta.Usuario = usuario.Id;
    oferta.Produto = produto.Id;
    oferta.Preco = value;
    context.Oferta.Add(oferta);

    await context.SaveChangesAsync();
}

async Task createProduct(string nome, string desc)
{
    Produto produto = new Produto();

    produto.Nome = nome;
    produto.Descricao = desc;
    context.Produtos.Add(produto);

    await context.SaveChangesAsync();
}

void printData()
{
    var query =
        from u in context.Usuarios
        join o in context.Oferta
        on u.Id equals o.Usuario
        select new
        {
            nome = u.Nome,
            produto = o.Produto,
            valor = o.Preco
        } into x
        join p in context.Produtos
        on x.produto equals p.Id
        select new
        {
            nome = x.nome,
            produto = p.Nome,
            valor = x.valor
        };
    
    foreach (var y in query)
        Console.WriteLine($"{y.nome} está vendendo um {y.produto} por {y.valor} R$.");
}

async Task createUser(string nome)
{
    Usuario usuario = new Usuario();

    usuario.Nome = nome;
    usuario.DataNascimento = DateTime.Now;
    usuario.Foto = null;
    context.Usuarios.Add(usuario);

    await context.SaveChangesAsync();
}