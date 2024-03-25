using CRUDcomDapper.Models;
using Dapper;
using System.Data.SqlClient;

namespace CRUDcomDapper.Services.LivroService
{
    public class LivroService : ILivroInterface
    {
        private readonly IConfiguration _configuration;
        private readonly string getConnection;
        public LivroService(IConfiguration configuration)
        {
            _configuration = configuration;
            getConnection = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Livro>> CreateLivro(Livro livro)
        {
            using (var con  = new SqlConnection(getConnection)) 
            {
                var sql = "INSERT into Livros (titulo, autor) VALUES (@titulo, @autor)";
                await con.ExecuteAsync(sql, livro);
                return await con.QueryAsync<Livro>("SELECT * FROM Livros");
            }
        }

        public async Task<IEnumerable<Livro>> DeleteLivro(int livroId)
        {
            using (var con = new SqlConnection(getConnection))
            {
                var sql = "DELETE FROM Livros WHERE id = @id";
                await con.ExecuteAsync(sql, new {id = livroId});
                return await con.QueryAsync<Livro>("SELECT * FROM Livros");
            }
        }

        public async Task<IEnumerable<Livro>> GetAllLivros()
        {
            using (var con = new SqlConnection(getConnection))
            {
                var sql = "SELECT * FROM Livros";
                return await con.QueryAsync<Livro>(sql);
            }
        }

        public async Task<Livro> GetLivroById(int livroId)
        {
            using (var con = new SqlConnection(getConnection))
            {
                var sql = "SELECT * FROM Livros WHERE id = @id";
                return await con.QueryFirstOrDefaultAsync<Livro>(sql, new { Id = livroId });
            }
        }

        public async Task<IEnumerable<Livro>> UpdateLivro(Livro livro)
        {
            using (var con = new SqlConnection(getConnection))
            {
                var sql = "UPDATE Livros SET titulo = @titulo, autor = @autor WHERE id = @id";
                await con.ExecuteAsync(sql, livro);
                return await con.QueryAsync<Livro>("SELECT * FROM Livros");
            }
        }
    }
}
