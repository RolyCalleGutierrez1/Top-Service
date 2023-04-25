using System.ComponentModel.DataAnnotations;
namespace PR_Top_Service_MVC.Models
{
    public class CustomerUser
    {

        [Key]
        public int IdCostumer { get; set; }
        public string Address { get; set; } = null!;


        public int IdPerson { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string SecondLastName { get; set; } = null!;
        public byte IdDepartment { get; set; }
        public virtual Department? IdDepartmentNavigation { get; set; } = null!;
        public byte status { get; set; }


        public int IdUser { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
