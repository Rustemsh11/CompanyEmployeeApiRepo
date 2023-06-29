using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public abstract record CompanyForManipulationDTO
    {
        [Required(ErrorMessage = "The Name field is can not be null")]
        [MaxLength(30, ErrorMessage = "The Name field should be lower than 30 characters")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "The Address field is can not be null")]
        public string? Address { get; init; }
        [Required(ErrorMessage = "The Contry field is can not be null")]
        public string? Country { get; init; }   

        IEnumerable<EmployeeForCreateDTO> Employees { get; init; }  
    }
}
