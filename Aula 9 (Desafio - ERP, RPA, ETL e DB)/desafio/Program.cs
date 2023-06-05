using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Management.Automation;

using desafio.Model;

using static desafio.DBUteis;

var foldersRepositories = Directory
    .EnumerateDirectories("C:/Users/disrct/Desktop/Repositories");

using var ps = PowerShell.Create();


foreach (var folder in foldersRepositories)
{
    var foldersInFoldersRepositories = Directory
        .EnumerateDirectories(folder);

    foreach (var folderInFolder in foldersInFoldersRepositories)
    {
        string path = folderInFolder.Replace("\\", "/");
        string[] subs = path.Split('/');
        if(subs[subs.Length - 1].Contains(".git"))
        {
            Console.WriteLine(folder);
            ps
                .AddCommand("cd")
                .AddArgument(folder)
                .Invoke();
            ps.Commands.Clear();

            var result = ps
                .AddCommand("git")
                .AddArgument("pull")
                .Invoke();
            ps.Commands.Clear();

            foreach (var line in result)
                Console.WriteLine(line);

            DesafioGitContext context = new DesafioGitContext();
            string repoName = folder.Replace("\\", "/");
            string[] repoNameSplit = repoName.Split('/');
            await pullRepositorio(repoNameSplit[repoNameSplit.Length - 1], context, folder);
        }
    }
}