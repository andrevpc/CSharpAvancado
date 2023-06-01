using System;
using System.Collections.Generic;

namespace EntityTest.Model;

public partial class Ofertum
{
    public int Id { get; set; }

    public int Produto { get; set; }

    public int Usuario { get; set; }

    public decimal Preco { get; set; }

    public virtual Produto ProdutoNavigation { get; set; } = null;

    public virtual Usuario UsuarioNavigation { get; set; } = null;
}
