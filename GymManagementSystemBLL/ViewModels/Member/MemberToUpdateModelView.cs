using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.ViewModels.Member
{
    public class MemberToUpdateModelView
    {
        public string Name { get; set; } = null!;
        public  string ?Phote { get; set; }
        [Required(ErrorMessage = "Phone is Required")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; } = null!;


        [Required(ErrorMessage = "City is required.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "City name must contain letters and spaces only.")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Street is required.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Street name must contain letters and spaces only.")]
        public string Streat { get; set; } = null!;

        [Required(ErrorMessage = "Building number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Building number must be a positive number.")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invaild Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email Must Be Between 5 and 100")]
        public string Email { get; set; } = null!;
    }
}
