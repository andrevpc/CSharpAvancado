using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace desafio;

using desafio.Model;


public static class DBUteis
{
    public static async Task pullRepositorio(string nome, DesafioGitContext context, string path)
    {
        var repo = context.Repositorios.FirstOrDefault(x => x.Nome == nome);

        if (repo is not null)
            await updateRepositorio(repo, context);
        else
            await createRepositorio(nome, context, path);
    }
    public static async Task updateRepositorio(Repositorio repo, DesafioGitContext context)
    {
        repo.LastPull = DateTime.Now;

        await context.SaveChangesAsync();
    }
    public static async Task createRepositorio(string nome, DesafioGitContext context, string path)
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