namespace Entities.Exceptions
{
    public class EmployeeNotFound : NotFoundException
    {
        public EmployeeNotFound(Guid employeeId) : base($"Emplyee with id: {employeeId} doent exist in database")
        {
        }
    }
}
