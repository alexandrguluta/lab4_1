namespace Entities.RequestFeatures
{
    public class UserParameters: RequestParameters
    {
        public UserParameters() 
        {
            OrderBy = "name";
        }

        public string FirstUser { get; set; } = "A";
        public string LastUser { get; set; } = "Z";
        public string SearchTerm { get; set; }

    }
}
