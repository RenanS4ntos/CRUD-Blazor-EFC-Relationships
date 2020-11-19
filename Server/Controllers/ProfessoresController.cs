using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ProvaOA.Shared;
using ProvaOA.Server;

[ApiController]
[Route("[controller]")]
public class ProfessoresController : Controller
{
    private readonly AppDbContext db;

    public ProfessoresController(AppDbContext db)
    {
        this.db = db;
    }

    [HttpGet]
    [Route("/professor/list")]
    public async Task<IActionResult> Get()
    {
        var Professores = await db.Professores.ToListAsync();
        return Ok(Professores);
    }

    [HttpGet]
    [Route("/professor/cursos")]
    public async Task<IActionResult> GetCursos()
    {
        var Professores = await (from pc in db.ProfessorCurso select new ProfCursoDto
        {
            ProfessorId = pc.ProfessorId,
            ProfessorNome = pc.Professor.Nome,
            CursoId = pc.CursoId,
            CursoNome = pc.Curso.Nome
        }).ToListAsync();
        return Ok(Professores);
    }

    [HttpPost]
    [Route("/professor/create")]
    public async Task<ActionResult> Post([FromBody] ProfessorDto Professor)
    {
        try
        {
            var newProfessor = new Professor
            {
                Nome = Professor.Nome,
            };

            var newProfCurso = new ProfCurso
            {
                Curso = db.Cursos.FirstOrDefault(c => c.CursoId == Convert.ToInt32(Professor.CursoId)),
                Professor = newProfessor
            };

            db.Add(newProfessor);
            db.Add(newProfCurso);
            await db.SaveChangesAsync();//INSERT INTO
            return Ok();
        }
        catch (Exception e)
        {
            return View(e);
        }
    }

    [HttpDelete]
    [Route("/professor/delete/{id}")]
    public async Task<ActionResult<Professor>> Delete(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var Professor = await db.Professores.FindAsync(id);
        if (Professor == null)
        {
            return NotFound();
        }
        db.Professores.Remove(Professor);
        await db.SaveChangesAsync();
        return NoContent();
    }

    private bool ProfessorExists(int id)
    {
        return db.Professores.Any(e => e.ProfessorId == id);
    }

}