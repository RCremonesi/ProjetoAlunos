using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace ProjetoAlunos
{
    public class AlunoRepositorio
    {
        private string connectionString = @"Data Source=C:\Users\RICARDO\alunos.db;Version=3;";

        public bool RAExiste(string ra)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT COUNT(1) FROM alunos WHERE RA = @RA";
                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@RA", ra);
                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }


        public void Inserir(Aluno aluno)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO alunos (RA, Nome, Endereco, Curso, Idade) VALUES (@RA, @Nome, @Endereco, @Curso, @Idade)";
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

        public bool ExcluirAluno(string ra)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                
                string verificaSql = "SELECT COUNT(*) FROM alunos WHERE RA = @RA";
                using (var verificaCmd = new SQLiteCommand(verificaSql, connection))
                {
                    verificaCmd.Parameters.AddWithValue("@RA", ra);
                    long count = (long)verificaCmd.ExecuteScalar();

                    if (count == 0)
                    {
                        
                        return false;
                    }
                }

                
                string sql = "DELETE FROM alunos WHERE RA = @RA";
                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@RA", ra);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
        }

        public void AtualizarAluno(Aluno aluno)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = @"UPDATE alunos SET 
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
                string sql = "SELECT RA, Nome, Endereco, Idade, Curso FROM alunos";

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
