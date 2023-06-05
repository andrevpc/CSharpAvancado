using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace desafio.Model.DBUteis;


public class Singleton
{
    private Singleton() { }
    private Singleton(DesafioGitContext context)
        => this.context = context;
    private static Singleton crr = new Singleton();
    public static Singleton Current => crr;
    public DesafioGitContext context { get; set; }
    public static void New()
        => crr = new Singleton();
    public static void New(DesafioGitContext context)
        => crr = new Singleton(context);

    public async Task pullRepositorio(string nome, string path)
    {
        var repo = context.Repositorios.FirstOrDefault(x => x.Nome == nome);

        if (repo is not null)
            await updateRepositorio(repo);
        else
            await createRepositorio(nome, path);
    }
    public async Task updateRepositorio(Repositorio repo)
    {
        repo.LastPull = DateTime.Now;

        await context.SaveChangesAsync();
    }
    public async Task createRepositorio(string nome, string path)
    {
        Repositorio repositorio = new Repositorio();

        repositorio.Nome = nome;
        repositorio.LastPull = DateTime.Now;
        repositorio.Created = DateTime.Now;
        repositorio.RepoPath = path;
        context.Repositorios.Add(repositorio);

        await context.SaveChangesAsync();
    }


    public static void printData(DesafioGitContext context)
    {
        var query =
            from r in context.Repositorios
            select new
            {
                nome = r.Nome
            };
        
        foreach (var y in query)
            Console.WriteLine($"{y.nome}");
    }
    public static async Task<Repositorio> getItens(string nome, DesafioGitContext context)
        => context.Repositorios
                  .FirstOrDefault(x => x.Nome == nome);
}