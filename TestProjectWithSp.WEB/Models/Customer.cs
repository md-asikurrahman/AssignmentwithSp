using System.ComponentModel.DataAnnotations;

namespace TestProjectWithSp.WEB.Models
{
    public class Customer :BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string NidNo { get; set; }
        [Required]
        public DateTime Dob {  get; set; }
        public bool IsActive {  get; set; } = true;
    }
}
