using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ProvaOA.Shared
{
    public class Curso
    {
        public int CursoId { get; set; }

        [Required]
        public string Nome { get; set; }

        public ICollection<Aluno> Alunos { get; set; }

        public ICollection<ProfCurso> Professores { get; set; }
    }
}
