using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace desafio.Model.DBUteis;

public static class DBUteis
{
    public static async Task pullRepositorio(string nome, DateTime dt, DesafioGitContext context)
    {
        var sla = context.Repositorios.FirstOrDefault(x => x.Nome == nome);

        if (sla is not null)
            await updateRepositorio(sla, context);
        else
            await createRepositorio(nome, context);
    }
    public static async Task updateRepositorio(Repositorio repo, DesafioGitContext context)
    {
        repo.LastPull = DateTime.Now;

        await context.SaveChangesAsync();
    }
    public static async Task createRepositorio(string nome, DesafioGitContext context)
    {
        Repositorio repositorio = new Repositorio();

        repositorio.Nome = nome;
        repositorio.LastPull = DateTime.Now;
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