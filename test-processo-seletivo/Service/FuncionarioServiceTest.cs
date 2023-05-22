using Moq;
using processo_seletivo.Messages;
using processo_seletivo.Models;
using processo_seletivo.Service;
using System.Net;
using test_processo_seletivo.Data;
using test_processo_seletivo.DbSetMock;

namespace test_processo_seletivo.Service
{
    public class FuncionarioServiceTest
    {
        private readonly List<Funcionario> funcionarios;
        private readonly Mock<ProcessoseletivoContext> dbContextMock;
        private readonly FuncionarioService funcionarioService;

        public FuncionarioServiceTest()
        {
            funcionarios = FuncionarioData.GetFuncionarios();
            dbContextMock = new Mock<ProcessoseletivoContext>();
            funcionarioService = new FuncionarioService(dbContextMock.Object);
        }

        [Fact]
        public void GetTodosFuncionarios_DeveRetornarListaDeFuncionarios()
        {
            // Arrange
            dbContextMock.Setup(db => db.Funcionarios).Returns(DbContextMock.GetQueryableMockDbSet(funcionarios));
            var funcionarioService = new FuncionarioService(dbContextMock.Object);

            // Act
            var resultado = funcionarioService.GetTodosFuncionarios();

            // Assert
            Assert.Equal(funcionarios, resultado);
            Assert.Equal(resultado.Count(), 3);
        }

        [Fact]
        public void GetTodosFuncionarios_ExceptionThrown_ShouldThrowMessageException()
        {
            // Arrange
            dbContextMock.Setup(db => db.Funcionarios);

            // Act & Assert
            var exception = Assert.Throws<MessageException>(() => funcionarioService.GetTodosFuncionarios());
            Assert.Equal((int)HttpStatusCode.InternalServerError, exception.StatusCode);
            Assert.Equal("Erro ao obter todos os funcionários.", exception.Message);
        }

        [Fact]
        public void GetFuncionarioPorId_DeveRetornarFuncionarioExistente()
        {
            // Arrange
            var id = 1;
            var funcionarioEsperado = funcionarios.FirstOrDefault(f => f.Id == id);
            dbContextMock.Setup(db => db.Funcionarios).Returns(DbContextMock.GetQueryableMockDbSet(funcionarios));

            // Act
            var resultado = funcionarioService.GetFuncionarioPorId(id);

            // Assert
            Assert.Equal(funcionarioEsperado, resultado);
        }

        [Fact]
        public void GetFuncionarioPorId_DeveLancarExcecaoParaFuncionarioInexistente()
        {
            // Arrange
            var id = 4;
            dbContextMock.Setup(db => db.Funcionarios).Returns(DbContextMock.GetQueryableMockDbSet(funcionarios));

            // Assert
            Assert.Throws<MessageException>(() => funcionarioService.GetFuncionarioPorId(id));
        }

        [Fact]
        public void AddFuncionario_DeveAdicionarNovoFuncionario()
        {
            // Arrange
            var novoFuncionario = new Funcionario
            {
                Id = 4,
                Nome = "Novo Funcionario",
                DataNascimento = new DateTime(1990, 1, 1),
                Cpf = "987654323",
                Email = "novo.funcionario@example.com",
                IdLotacao = 1
            };
            var funcionariosCopia = new List<Funcionario>(funcionarios);
            dbContextMock.Setup(db => db.Funcionarios).Returns(DbContextMock.GetQueryableMockDbSet(funcionariosCopia));

            // Act
            var resultado = funcionarioService.AddFuncionario(novoFuncionario);

            // Assert
            Assert.Equal(novoFuncionario, resultado);
            Assert.Equal(funcionariosCopia.Count(), 4);
            dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Fact]
        public void AddFuncionario_DeveLancarExcecaoParaNomeVazioOuNulo()
        {
            // Arrange
            var novoFuncionario = new Funcionario
            {
                Id = 4,
                Nome = null,
                DataNascimento = new DateTime(1990, 1, 1),
                Cpf = "987654321",
                Email = "novo.funcionario@example.com",
                IdLotacao = 1
            };
            dbContextMock.Setup(db => db.Funcionarios).Returns(DbContextMock.GetQueryableMockDbSet(funcionarios));

            // Assert
            Assert.Throws<MessageException>(() => funcionarioService.AddFuncionario(novoFuncionario));
            dbContextMock.Verify(db => db.SaveChanges(), Times.Never);
        }

        [Fact]
        public void AddFuncionario_DeveLancarExcecaoParaCpfExistente()
        {
            // Arrange
            var novoFuncionario = new Funcionario
            {
                Id = 4,
                Nome = "Novo Funcionario",
                DataNascimento = new DateTime(1990, 1, 1),
                Cpf = "123456789", // CPF já existente na lista de funcionários
                Email = "novo.funcionario@example.com",
                IdLotacao = 1
            };
            dbContextMock.Setup(db => db.Funcionarios).Returns(DbContextMock.GetQueryableMockDbSet(funcionarios));

            // Assert
            Assert.Throws<MessageException>(() => funcionarioService.AddFuncionario(novoFuncionario));
            dbContextMock.Verify(db => db.SaveChanges(), Times.Never);
        }

        [Fact]
        public void AddFuncionario_DeveLancarExcecaoParaEmailExistente()
        {
            // Arrange
            var novoFuncionario = new Funcionario
            {
                Id = 4,
                Nome = "Novo Funcionario",
                DataNascimento = new DateTime(1990, 1, 1),
                Cpf = "987654325",
                Email = "funcionario1@example.com", // Email já existente na lista de funcionários
                IdLotacao = 1
            };
            dbContextMock.Setup(db => db.Funcionarios).Returns(DbContextMock.GetQueryableMockDbSet(funcionarios));

            // Assert
            Assert.Throws<MessageException>(() => funcionarioService.AddFuncionario(novoFuncionario));
            dbContextMock.Verify(db => db.SaveChanges(), Times.Never);
        }

        [Fact]
        public void UpdateFuncionario_DeveAtualizarFuncionarioExistente()
        {
            // Arrange
            var id = 1;
            var funcionarioAlterado = new Funcionario
            {
                Id = id,
                Nome = "Novo Nome",
                DataNascimento = new DateTime(1990, 1, 1),
                Cpf = "123456789",
                Email = "novo.email@example.com",
                IdLotacao = 1
            };
            dbContextMock.Setup(db => db.Funcionarios).Returns(DbContextMock.GetQueryableMockDbSet(funcionarios));
            dbContextMock.Setup(db => db.SaveChanges()).Verifiable();

            // Act
            var resultado = funcionarioService.UpdateFuncionario(id, funcionarioAlterado);

            // Assert
            Assert.Equal(funcionarioAlterado.Nome, resultado.Nome);
            Assert.Equal(funcionarioAlterado.DataNascimento, resultado.DataNascimento);
            Assert.Equal(funcionarioAlterado.Cpf, resultado.Cpf);
            Assert.Equal(funcionarioAlterado.Email, resultado.Email);
            dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Fact]
        public void UpdateFuncionario_DeveLancarExcecaoParaNomeVazioOuNulo()
        {
            // Arrange
            var id = 1;
            var funcionarioAlterado = new Funcionario
            {
                Id = id,
                Nome = null,
                DataNascimento = new DateTime(1990, 1, 1),
                Cpf = "123456789",
                Email = "novo.email@example.com",
                IdLotacao = 1
            };
            dbContextMock.Setup(db => db.Funcionarios).Returns(DbContextMock.GetQueryableMockDbSet(funcionarios));
            dbContextMock.Setup(db => db.SaveChanges()).Verifiable();

            // Assert
            Assert.Throws<MessageException>(() => funcionarioService.UpdateFuncionario(id, funcionarioAlterado));
            dbContextMock.Verify(db => db.SaveChanges(), Times.Never);
        }

        [Fact]
        public void UpdateFuncionario_DeveLancarExcecaoParaFuncionarioInexistente()
        {
            // Arrange
            var id = 10; // ID de um funcionário inexistente
            var funcionarioAlterado = new Funcionario
            {
                Id = id,
                Nome = "Novo Nome",
                DataNascimento = new DateTime(1990, 1, 1),
                Cpf = "123456789",
                Email = "novo.email@example.com",
                IdLotacao = 1
            };
            dbContextMock.Setup(db => db.Funcionarios).Returns(DbContextMock.GetQueryableMockDbSet(funcionarios));
            dbContextMock.Setup(db => db.SaveChanges()).Verifiable();

            // Assert
            Assert.Throws<MessageException>(() => funcionarioService.UpdateFuncionario(id, funcionarioAlterado));
            dbContextMock.Verify(db => db.SaveChanges(), Times.Never);
        }

        [Fact]
        public void DeleteFuncionario_DeveRemoverFuncionarioExistente()
        {
            // Arrange
            var id = 1;
            var funcionario = funcionarios.FirstOrDefault(f => f.Id == id);
            var funcionariosCopia = new List<Funcionario>(funcionarios);
            dbContextMock.Setup(db => db.Funcionarios).Returns(DbContextMock.GetQueryableMockDbSet(funcionariosCopia));
            var funcionarioService = new FuncionarioService(dbContextMock.Object);

            // Act
            funcionarioService.DeleteFuncionario(id);

            // Assert
            dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
            dbContextMock.Verify(db => db.Funcionarios.Remove(It.IsAny<Funcionario>()), Times.Once);
            Assert.DoesNotContain(funcionario, funcionariosCopia);
            Assert.Equal(funcionariosCopia.Count(), 2);
        }


        [Fact]
        public void DeleteFuncionario_DeveLancarExcecaoParaFuncionarioInexistente()
        {
            // Arrange
            var id = 10; // ID de um funcionário inexistente
            dbContextMock.Setup(db => db.Funcionarios).Returns(DbContextMock.GetQueryableMockDbSet(funcionarios));
            dbContextMock.Setup(db => db.SaveChanges()).Verifiable();

            // Assert
            Assert.Throws<MessageException>(() => funcionarioService.DeleteFuncionario(id));
            dbContextMock.Verify(db => db.Funcionarios.Remove(It.IsAny<Funcionario>()), Times.Never);
            dbContextMock.Verify(db => db.SaveChanges(), Times.Never);
        }

    }
}