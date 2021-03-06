﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ProvaOA.Shared
{
    public class Aluno
    {
        public int AlunoId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public int Idade { get; set; }

        public Professor Professor { get; set; }

        public int ProfessorId { get; set; }

        public Curso Curso { get; set; }

        public int CursoId { get; set; }
    }
}
