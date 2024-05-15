using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    // we do not need Id beacuse it will be accpted through URI , like in delete requests 
    public record class EmployeeForUpdateDto(string Name,int Age,string position);
}
