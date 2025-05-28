namespace People.Web.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string JobTitle { get; set; } = "";
        public decimal Salary { get; set; }
        public string Department { get; set; } = "";
        public string Username { get; set; } = "";
        public string Role { get; internal set; }
    }
}
