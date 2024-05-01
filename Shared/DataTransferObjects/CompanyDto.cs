using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    //Instead of a regular class, we are using a record for DTO.This specific
    //record type is known as a Positional record.
    public record class CompanyDto(Guid Id , string name , string FullAddress);
}
