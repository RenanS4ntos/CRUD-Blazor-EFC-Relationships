using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ProvaOA.Shared;
using ProvaOA.Server;

[ApiController]
[Route("[controller]")]
public class AlunosController : Controller
{
    private readonly AppDbContext db;

    public AlunosController(AppDbContext db)
    {
        this.db = db;
    }

    [HttpGet]
    [Route("/aluno/list")]
    public async Task<IActionResult> Get()
    {
        var alunos = await (from a in db.Alunos
                    select new AlunoDto
                    {
                              AlunoId = a.AlunoId,
                              Nome = a.Nome,
                              Idade = a.Idade,
                              ProfessorId = a.ProfessorId.ToString(),
                              ProfessorNome = a.Professor.Nome,
                              CursoId = a.CursoId.ToString(),
                              CursoNome = a.Curso.Nome
                    }).ToListAsync();
        return Ok(alunos);
    }

    [HttpPost]
    [Route("/aluno/create")]
    public async Task<ActionResult> Post([FromBody] AlunoDto Aluno)
    {
        try
        {
            var newAluno = new Aluno
            {
                Nome = Aluno.Nome,
                Idade = Aluno.Idade,
                CursoId = Convert.ToInt32(Aluno.CursoId),
                ProfessorId = Convert.ToInt32(Aluno.ProfessorId)
            };

            db.Alunos.Add(newAluno);
            await db.SaveChangesAsync();//INSERT INTO
            return Ok();
        }
        catch (Exception e)
        {
            return View(e);
        }
    }

    [HttpDelete]
    [Route("/aluno/delete/{id}")]
    public async Task<ActionResult<Aluno>> Delete(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var Aluno = await db.Alunos.FindAsync(id);
        if (Aluno == null)
        {
            return NotFound();
        }
        db.Alunos.Remove(Aluno);
        await db.SaveChangesAsync();
        return NoContent();
    }

    private bool AlunoExists(int id)
    {
        return db.Alunos.Any(e => e.AlunoId == id);
    }

}