using processo_seletivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_processo_seletivo.Data
{
    public static class FuncionarioData
    {
        public static List<Funcionario> GetFuncionarios()
        {
            return new List<Funcionario>
                {
                    new Funcionario
                    {
                        Id = 1,
                        Nome = "Funcionario 1",
                        DataNascimento = new DateTime(1990, 1, 1),
                        Cpf = "123456789",
                        Email = "funcionario1@example.com",
                        IdLotacao = 1
                    },
                    new Funcionario
                    {
                        Id = 2,
                        Nome = "Funcionario 2",
                        DataNascimento = new DateTime(1995, 2, 2),
                        Cpf = "987654321",
                        Email = "funcionario2@example.com",
                        IdLotacao = 2
                    },
                    new Funcionario
                    {
                        Id = 3,
                        Nome = "Funcionario 3",
                        DataNascimento = new DateTime(2000, 3, 3),
                        Cpf = "456789123",
                        Email = "funcionario3@example.com",
                        IdLotacao = 3
                    }
                };
        }
    }
}
