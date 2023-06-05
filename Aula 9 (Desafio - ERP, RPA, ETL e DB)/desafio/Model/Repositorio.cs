using System;
using System.Collections.Generic;

namespace desafio.Model;

public partial class Repositorio
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public string RepoPath { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? LastPull { get; set; }
}
