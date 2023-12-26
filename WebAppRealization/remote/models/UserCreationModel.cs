using System.Collections.Generic;

namespace WebAppIImpl.remote.models
{
    public class UserCreationModel
    {
        public string? Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<AdminModel> Admins { get; set; }
    }
}