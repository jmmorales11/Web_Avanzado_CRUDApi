﻿using System;
using System.Collections.Generic;

namespace ESTUDIANTECRUD.Models
{
    public partial class Materium
    {
        public Materium()
        {
            Estudiantes = new HashSet<Estudiante>();
        }

        public int IdMateria { get; set; }
        public string? NombreMateria { get; set; }
        public string? CodigoCurso { get; set; }

        public virtual ICollection<Estudiante> Estudiantes { get; set; }
    }
}
