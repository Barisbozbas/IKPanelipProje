namespace IzinTakipProject.Data.Entities
{
    public class LoginResult
    {
   

        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
