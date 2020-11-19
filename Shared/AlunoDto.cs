using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ProvaOA.Shared
{
    public class AlunoDto
    {
        public int AlunoId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public int Idade { get; set; }

        public Professor Professor { get; set; }

        public string ProfessorNome { get; set; }

        public string ProfessorId { get; set; }

        public Curso Curso { get; set; }

        public string CursoNome { get; set; }

        public string CursoId { get; set; }
    }
}
