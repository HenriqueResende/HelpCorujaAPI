﻿namespace HelpCorujaAPI.Model
{
    public class Tutor : Usuario
    {
        public Curso Curso { get; set; }

        public int? Semestre { get; set; }

        public string? Contato { get; set; }
    }
}
