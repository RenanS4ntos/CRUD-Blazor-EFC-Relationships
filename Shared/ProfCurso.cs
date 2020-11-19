using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ProvaOA.Shared
{
    public class ProfCurso
    {
        public int ProfessorId { get; set; }

        public Professor Professor { get; set; }

        public int CursoId { get; set; }

        public Curso Curso { get; set; }
    }
}
