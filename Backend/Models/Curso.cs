using System;
using System.ComponentModel.DataAnnotations;

namespace web_api.Models
{
    public class Curso
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Nome { get; set; }

        [Range(1, int.MaxValue)]
        public int CargaHoraria { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Valor { get; set; }

        [DataInicioValidation]
        public DateTime DataInicio { get; set; }

        public bool Online { get; set; }

        public bool Ativo { get; set; }
    }

    public class DataInicioValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var curso = (Curso)validationContext.ObjectInstance;

            if (curso.Id == 0 && value is DateTime dataInicio)
            {
                if (dataInicio.Date < DateTime.Now.Date)
                {
                    return new ValidationResult("Data Invalida");
                }
            }
            return ValidationResult.Success;
        }
    }
}