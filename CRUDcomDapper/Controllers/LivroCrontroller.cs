using CRUDcomDapper.Models;
using CRUDcomDapper.Services.LivroService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDcomDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroCrontroller : ControllerBase
    {
        private readonly ILivroInterface _livroInterface;
        public LivroCrontroller(ILivroInterface livroInterface) 
        {
            _livroInterface = livroInterface;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetAllLivros()
        {
            IEnumerable<Livro> livros = await _livroInterface.GetAllLivros();

            if(!livros.Any()) 
            {
                return NotFound("Nenhum registro localizado.");
            }

            return Ok(livros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Livro>> GetLivroById(int livroId)
        {
            Livro livro = await _livroInterface.GetLivroById(livroId);

            if(livro == null)
            {
                return NotFound("Registro não encontrado.");
            }

            return Ok(livro);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Livro>>> CreateLivro (Livro livro)
        {
            IEnumerable<Livro> livros = await _livroInterface.CreateLivro(livro);
            return Ok(livros);
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<Livro>>> UpdateLivro(Livro livro)
        {
            Livro registro = await _livroInterface.GetLivroById(livro.id);

            if(registro == null)
            {
                return NotFound("Registro não encontrado");
            }

            IEnumerable<Livro> livros = await _livroInterface.UpdateLivro(livro);
            return Ok(livros);
        }

        [HttpDelete]
        public async Task<ActionResult<IEnumerable<Livro>>> DeleteLivro (int livroId)
        {
            Livro registro = await _livroInterface.GetLivroById(livroId);

            if (registro == null)
            {
                return NotFound("Registro não encontrado");
            }

            IEnumerable<Livro> livros = await _livroInterface.DeleteLivro(livroId);
            return Ok(livros);
        }
    }
}
