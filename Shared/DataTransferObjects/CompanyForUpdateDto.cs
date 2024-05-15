namespace Shared.DataTransferObjects
{
    public record class CompanyForUpdateDto(string Name,string Address, string Country,
        IEnumerable<EmployeeForCreationDto> Employees);
}
