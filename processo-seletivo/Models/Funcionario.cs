namespace processo_seletivo.Models;

public partial class Funcionario
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public DateTime DataNascimento { get; set; }

    public string Cpf { get; set; } = null!;

    public string? Email { get; set; }

    public int IdLotacao { get; set; }

    public virtual Lotacao? IdLotacaoNavigation { get; set; } = null!;

}
