using System;

namespace WebAppIImpl.remote.models
{
    public class CompanyModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? FullAddress { get; set; }
    }
}