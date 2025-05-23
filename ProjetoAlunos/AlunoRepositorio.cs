using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ProjetoAlunos
{
    public class AlunoRepositorio
    {
        private string connectionString = @"Data Source=C:\Users\RICARDO\alunos.db;Version=3;";

        public void Inserir(Aluno aluno)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Aluno (RA, Nome, Endereco, Curso, Idade) VALUES (@RA, @Nome, @Endereco, @Curso, @Idade)";
                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@RA", aluno.Ra);
                    cmd.Parameters.AddWithValue("@Nome", aluno.Nome);
                    cmd.Parameters.AddWithValue("@Endereco", aluno.Endereco);
                    cmd.Parameters.AddWithValue("@Curso", aluno.Curso);
                    cmd.Parameters.AddWithValue("@Idade", aluno.Idade);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ExcluirAluno(string ra)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Aluno WHERE RA = @RA";
                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@RA", ra);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarAluno(Aluno aluno)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = @"UPDATE Aluno SET 
                               Nome = @Nome,
                               Endereco = @Endereco,
                               Idade = @Idade,
                               Curso = @Curso
                               WHERE RA = @RA";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Nome", aluno.Nome);
                    cmd.Parameters.AddWithValue("@Endereco", aluno.Endereco);
                    cmd.Parameters.AddWithValue("@Idade", aluno.Idade);
                    cmd.Parameters.AddWithValue("@Curso", aluno.Curso);
                    cmd.Parameters.AddWithValue("@RA", aluno.Ra);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Aluno> ListarAlunos()
        {
            List<Aluno> lista = new List<Aluno>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT RA, Nome, Endereco, Idade, Curso FROM Aluno";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Aluno aluno = new Aluno
                            {
                                Ra = reader["RA"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                Endereco = reader["Endereco"].ToString(),
                                Idade = Convert.ToInt32(reader["Idade"]),
                                Curso = reader["Curso"].ToString()
                            };

                            lista.Add(aluno);
                        }
                    }
                }
            }

            return lista;
        }
    }
}