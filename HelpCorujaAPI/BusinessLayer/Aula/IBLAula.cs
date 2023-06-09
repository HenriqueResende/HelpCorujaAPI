﻿using HelpCorujaAPI.Model;

namespace HelpCorujaAPI.BusinessLayer
{
    public interface IBLAula
    {
        public List<AulaDto> getAula(string? materia, int? semestre, DateTime? data);

        public List<AulaDto> getAulaTutor(string ra);

        public bool setAula(AulaSetDto aula);

        public bool deleteAula(int codigoAula);
    }
}
