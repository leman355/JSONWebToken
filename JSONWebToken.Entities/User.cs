using System.ComponentModel.DataAnnotations;

namespace JSONWebToken.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public int Age { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
