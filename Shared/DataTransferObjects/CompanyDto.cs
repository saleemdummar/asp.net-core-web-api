using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    //Instead of a regular class, we are using a record for DTO.This specific
    //record type is known as a Positional record.
    //[Serializable]
    //public record class CompanyDto(Guid Id , string name , string FullAddress);

    //second solution for serilization of xml the above one is best for only json
    // this object is still serializable
    public record CompanyDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? FullAddress { get; set; }
    }
    public record CompanyForCreationDto(string Name, string Address, string Country);
}
