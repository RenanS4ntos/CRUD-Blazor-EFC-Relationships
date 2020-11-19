using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ProvaOA.Shared
{
    public class Professor
    {
        public int ProfessorId { get; set; }

        [Required]
        public string Nome { get; set; }

        public Aluno Orientando { get; set; }

        public ICollection<ProfCurso> Cursos { get; set; }
    }
}
