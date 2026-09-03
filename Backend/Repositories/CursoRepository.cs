using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using web_api.Models;

namespace web_api.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly string _connectionString;

        public CursoRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConexaoBanco"].ConnectionString;
        }

        public async Task<IEnumerable<Curso>> ObterTodosAsync()
        {
            var cursos = new List<Curso>();
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = "SELECT Id, Nome, CargaHoraria, Valor, DataInicio, Online, Ativo FROM curso";
                using (var cmd = new SqlCommand(query, conn))
                {
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            cursos.Add(MapearCurso(reader));
                        }
                    }
                }
            }
            return cursos;
        }

        public async Task<Curso> ObterPorIdAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = "SELECT Id, Nome, CargaHoraria, Valor, DataInicio, Online, Ativo FROM curso WHERE Id = @Id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapearCurso(reader);
                        }
                    }
                }
            }
            return null;
        }

        public async Task<int> InserirAsync(Curso curso)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO curso (Nome, CargaHoraria, Valor, DataInicio, Online, Ativo) OUTPUT INSERTED.Id VALUES (@Nome, @CargaHoraria, @Valor, @DataInicio, @Online, @Ativo)";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", curso.Nome);
                    cmd.Parameters.AddWithValue("@CargaHoraria", curso.CargaHoraria);
                    cmd.Parameters.AddWithValue("@Valor", curso.Valor);
                    cmd.Parameters.AddWithValue("@DataInicio", curso.DataInicio);
                    cmd.Parameters.AddWithValue("@Online", curso.Online);
                    cmd.Parameters.AddWithValue("@Ativo", curso.Ativo);

                    await conn.OpenAsync();
                    return (int)await cmd.ExecuteScalarAsync();
                }
            }
        }

        public async Task<bool> AtualizarAsync(Curso curso)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = "UPDATE curso SET Nome = @Nome, CargaHoraria = @CargaHoraria, Valor = @Valor, DataInicio = @DataInicio, Online = @Online, Ativo = @Ativo WHERE Id = @Id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", curso.Id);
                    cmd.Parameters.AddWithValue("@Nome", curso.Nome);
                    cmd.Parameters.AddWithValue("@CargaHoraria", curso.CargaHoraria);
                    cmd.Parameters.AddWithValue("@Valor", curso.Valor);
                    cmd.Parameters.AddWithValue("@DataInicio", curso.DataInicio);
                    cmd.Parameters.AddWithValue("@Online", curso.Online);
                    cmd.Parameters.AddWithValue("@Ativo", curso.Ativo);

                    await conn.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = "DELETE FROM curso WHERE Id = @Id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    await conn.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        private Curso MapearCurso(SqlDataReader reader)
        {
            return new Curso
            {
                Id = Convert.ToInt32(reader["Id"]),
                Nome = reader["Nome"].ToString(),
                CargaHoraria = Convert.ToInt32(reader["CargaHoraria"]),
                Valor = Convert.ToDecimal(reader["Valor"]),
                DataInicio = Convert.ToDateTime(reader["DataInicio"]),
                Online = Convert.ToBoolean(reader["Online"]),
                Ativo = Convert.ToBoolean(reader["Ativo"])
            };
        }
    }
}