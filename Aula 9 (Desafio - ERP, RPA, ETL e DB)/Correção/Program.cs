using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Management.Automation;

var path = @"C:\Users\sii5ct\Desktop\MeusProjetos";
object _obj = new object();
bool isRunning = true;

Parallel.Invoke(
    () => {
        while (isRunning)
        {
            switch (Console.ReadLine())
            {
                case "show":
                    showCountAsync();
                    break;
                
                case "exit":
                    isRunning = false;
                    break;

            }
        }
    },

    () => {
        while (isRunning)
        {
            Thread.Sleep(5000);
            showCount();
        }
    }
);

async Task showCountAsync()
{
    await Task.Factory.StartNew(() =>
    {
        showCount();
    });
}

void showCount()
{
    var ps = PowerShell.Create();

    int count = 0;
    Console.WriteLine("Analisando repositórios...");
    foreach (var repo in getRepos(path))
    {
        Console.WriteLine(repo);
        ps.Commands.Clear();
        ps
            .AddCommand("cd")
            .AddArgument(repo)
            .Invoke();

        ps.Commands.Clear();
        var result = ps
            .AddCommand("git")
            .AddArgument("ls-files")
            .Invoke();
        
        foreach (var file in result)
        {
            lock (_obj)
            {
                string filePath = file
                    .ToString()
                    .Replace("/", "\\");
                string fullFilePath = $"{repo}\\{filePath}";
                var content = File.ReadAllLines(fullFilePath);
                count += content.Length;
            }
        }   
    }
    Console.WriteLine($"Total de linhas: {count}");
}

IEnumerable<string> getRepos(string basePath)
{
    if (!Directory.Exists(basePath))
        yield break;

    Stack<string> stack = new Stack<string>();
    stack.Push(basePath);

    while (stack.Count > 0)
    {
        var path = stack.Pop();
        var dirs = getDirs(path);
        bool isRepo = dirs
            .Any(x => x.EndsWith(".git"));
        
        if (isRepo)
        {
            yield return path;
            continue;
        }
        
        foreach (var dir in dirs)
            stack.Push(dir);
    }
}

IEnumerable<string> getDirs(string path)
    => Directory.EnumerateDirectories(path);