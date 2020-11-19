using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ProvaOA.Shared;
using ProvaOA.Server;

[ApiController]
[Route("[controller]")]
public class CursosController : Controller
{
    private readonly AppDbContext db;

    public CursosController(AppDbContext db)
    {
        this.db = db;
    }

    [HttpGet]
    [Route("/curso/list")]
    public async Task<IActionResult> Get()
    {
        var Cursos = await db.Cursos.ToListAsync();
        return Ok(Cursos);
    }

    [HttpPost]
    [Route("/curso/create")]
    public async Task<ActionResult> Post([FromBody] Curso Curso)
    {
        try
        {
            var newCurso = new Curso
            {
                Nome = Curso.Nome,
            };

            db.Cursos.Add(newCurso);
            await db.SaveChangesAsync();//INSERT INTO
            return Ok();
        }
        catch (Exception e)
        {
            return View(e);
        }
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<ActionResult<Curso>> Delete(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var Curso = await db.Cursos.FindAsync(id);
        if (Curso == null)
        {
            return NotFound();
        }
        db.Cursos.Remove(Curso);
        await db.SaveChangesAsync();
        return NoContent();
    }

    private bool CursoExists(int id)
    {
        return db.Cursos.Any(e => e.CursoId == id);
    }

}