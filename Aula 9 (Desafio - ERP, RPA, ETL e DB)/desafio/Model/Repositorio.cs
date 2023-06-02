using System;
using System.Collections.Generic;

namespace desafio.Model;

public partial class Repositorio
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public DateTime LastPull { get; set; }
}
