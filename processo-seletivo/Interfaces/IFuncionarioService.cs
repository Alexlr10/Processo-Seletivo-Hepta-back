using processo_seletivo.Models;

namespace processo_seletivo.Interfaces
{
    public interface IFuncionarioService
    {
        List<Funcionario> GetTodosFuncionarios();
        Funcionario GetFuncionarioPorId(int id);
        Funcionario AddFuncionario(Funcionario novoFuncionario);
        Funcionario UpdateFuncionario(int id, Funcionario funcionarioAlterado);
        void DeleteFuncionario(int id);
    }
}
