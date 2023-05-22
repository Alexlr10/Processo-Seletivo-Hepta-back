using System;
using System.Collections.Generic;

namespace processo_seletivo.Models;

public partial class Lotacao
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
}
