namespace ReviewsSystem.DAL.Parameters
{
    public class UsersParameters : QueryStringParameters
    {
        public string UserName { get; set; } 
        public string Email { get; set; }
    }
}
