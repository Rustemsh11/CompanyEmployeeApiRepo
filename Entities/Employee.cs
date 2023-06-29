using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Employee
    {
        [Column("EmployeeId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Employee name is a required field")]
        [MaxLength(30, ErrorMessage = "Maximum length for Name is 30 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Age is a required field")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Position  is a required field")]
        [MaxLength(20, ErrorMessage = "Maximum length for Position is 20 characters")]
        public string? Position { get; set; }

        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
