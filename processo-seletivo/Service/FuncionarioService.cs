using processo_seletivo.Interfaces;
using processo_seletivo.Messages;
using processo_seletivo.Models;
using System.Net;

namespace processo_seletivo.Service
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly ProcessoseletivoContext _dbContext;

        public FuncionarioService(ProcessoseletivoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Funcionario> GetTodosFuncionarios()
        {
            try
            {
                return _dbContext.Funcionarios.ToList();
            }
            catch (Exception ex)
            {
                throw new MessageException("Erro ao obter todos os funcionários.", (int)HttpStatusCode.InternalServerError);
            }
        }

        public Funcionario GetFuncionarioPorId(int id)
        {
            try
            {
                var funcionario = _dbContext.Funcionarios.FirstOrDefault(f => f.Id == id);

                if (funcionario == null)
                {
                    throw new MessageException($"Nenhum funcionário encontrado com o ID {id}.", (int)HttpStatusCode.NotFound);
                }

                return funcionario;
            }
            catch (Exception ex)
            {
                throw new MessageException("Erro ao obter o funcionário por ID.", (int)HttpStatusCode.InternalServerError);
            }
        }

        private void validarFuncionarioPorCpf(string cpf)
        {
            var funcionario = _dbContext.Funcionarios.FirstOrDefault(f => f.Cpf == cpf);

            if (funcionario != null)
            {
                throw new MessageException($"Já existe um funcionário com o CPF {cpf}.", (int)HttpStatusCode.BadRequest);
            }
        }

        private void validarFuncionarioPorEmail(string email)
        {
            var funcionario = _dbContext.Funcionarios.FirstOrDefault(f => f.Email == email);

            if (funcionario != null)
            {
                throw new MessageException($"Já existe um funcionário com o email {email}.", (int)HttpStatusCode.BadRequest);
            }
        }

        private void validarNomeFuncionarioVazio(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new MessageException("O nome do funcionário é obrigatório.", (int)HttpStatusCode.BadRequest);
            }
        }

        public Funcionario AddFuncionario(Funcionario novoFuncionario)
        {
            validarNomeFuncionarioVazio(novoFuncionario.Nome);
            validarFuncionarioPorCpf(novoFuncionario.Cpf);
            validarFuncionarioPorEmail(novoFuncionario.Email);
            try
            {

                _dbContext.Funcionarios.Add(novoFuncionario);
                _dbContext.SaveChanges();
                return novoFuncionario;
            }
            catch (Exception ex)
            {
                throw new MessageException("Erro ao adicionar o funcionário.", (int)HttpStatusCode.InternalServerError);
            }
        }

        public Funcionario UpdateFuncionario(int id, Funcionario funcionarioAlterado)
        {
            validarNomeFuncionarioVazio(funcionarioAlterado.Nome);
            var funcionario = GetFuncionarioPorId(id);
            try
            {
                funcionario.Nome = funcionarioAlterado.Nome;
                funcionario.DataNascimento = funcionarioAlterado.DataNascimento;
                funcionario.Cpf = funcionarioAlterado.Cpf;
                funcionario.Email = funcionarioAlterado.Email;
                funcionario.IdLotacao = funcionarioAlterado.IdLotacao;

                _dbContext.Funcionarios.Update(funcionario);
                _dbContext.SaveChanges();

                return funcionario;
            }
            catch (Exception ex)
            {
                throw new MessageException("Erro ao atualizar o funcionário.", (int)HttpStatusCode.InternalServerError);
            }
        }

        public void DeleteFuncionario(int id)
        {
            var funcionario = GetFuncionarioPorId(id);
            try
            {
                _dbContext.Funcionarios.Remove(funcionario);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new MessageException("Erro ao excluir o funcionário.", (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
