using System;
using System.Data.SQLite;
using ProjetoAlunos;

namespace ProjetoAlunos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AlunoRepositorio repositorio = new AlunoRepositorio();
            bool sair = false;

            while (!sair)
            {
                Console.Clear();
                Console.WriteLine("=== Sistema de Gerenciamento de Alunos ===");
                Console.WriteLine("1 - Inserir aluno");
                Console.WriteLine("2 - Listar alunos");
                Console.WriteLine("3 - Atualizar aluno");
                Console.WriteLine("4 - Excluir aluno");
                Console.WriteLine("5 - Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        InserirAluno(repositorio);
                        break;
                    case "2":
                        ListarAlunos(repositorio);
                        break;
                    case "3":
                        AtualizarAluno(repositorio);
                        break;
                    case "4":
                        ExcluirAluno(repositorio);
                        break;
                    case "5":
                        sair = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

                if (!sair)
                {
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void InserirAluno(AlunoRepositorio repositorio)
        {
            Console.Clear();
            Console.WriteLine("=== Inserir novo aluno ===");
            Console.Write("RA: ");
            string ra = Console.ReadLine();

            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Idade: ");
            int idade = int.Parse(Console.ReadLine());

            Console.Write("Curso: ");
            string curso = Console.ReadLine();

            Aluno aluno = new Aluno { Ra = ra, Nome = nome, Idade = idade, Curso = curso };

            repositorio.Inserir(aluno);

            Console.WriteLine("Aluno inserido com sucesso!");
        }

        static void ListarAlunos(AlunoRepositorio repositorio)
        {
            Console.Clear();
            Console.WriteLine("=== Lista de alunos ===");
            var alunos = repositorio.ListarAlunos();

            if (alunos.Count == 0)
            {
                Console.WriteLine("Nenhum aluno cadastrado.");
            }
            else
            {
                foreach (var aluno in alunos)
                {
                    Console.WriteLine($"RA: {aluno.Ra}, Nome: {aluno.Nome}, Idade: {aluno.Idade}, Curso: {aluno.Curso}");
                }
            }
        }

        static void AtualizarAluno(AlunoRepositorio repositorio)
        {
            Console.Clear();
            Console.WriteLine("=== Atualizar aluno ===");
            Console.Write("RA do aluno a atualizar: ");
            string ra = Console.ReadLine();

            Console.Write("Novo nome: ");
            string nome = Console.ReadLine();

            Console.Write("Nova idade: ");
            int idade = int.Parse(Console.ReadLine());

            Console.Write("Novo curso: ");
            string curso = Console.ReadLine();

            Aluno aluno = new Aluno { Ra = ra, Nome = nome, Idade = idade, Curso = curso };

            repositorio.AtualizarAluno(aluno);

            Console.WriteLine("Aluno atualizado com sucesso!");
        }

        static void ExcluirAluno(AlunoRepositorio repositorio)
        {
            Console.Clear();
            Console.WriteLine("=== Excluir aluno ===");
            Console.Write("RA do aluno a excluir: ");
            string ra = Console.ReadLine();

            repositorio.ExcluirAluno(ra);

            Console.WriteLine("Aluno excluído com sucesso!");
        }
    }
}
