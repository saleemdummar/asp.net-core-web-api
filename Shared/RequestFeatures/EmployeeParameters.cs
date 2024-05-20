﻿namespace Shared.RequestFeatures
{
    public class EmployeeParameters : RequestParameters
    {
        public uint MinAge { get; set; }
        public uint MaxAge { get; set; } = int.MaxValue;
        public bool ValidRange => MaxAge > MinAge;
        public string? SearchTerm { get; set; }
    }
}
