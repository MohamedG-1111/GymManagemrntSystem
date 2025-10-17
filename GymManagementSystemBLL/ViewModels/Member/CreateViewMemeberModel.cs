using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Model;

namespace GymManagementSystemBLL.ViewModels.Member
{
    public class CreateViewMemeberModel
    {
        [Required(ErrorMessage = "Name Is Required")]
        [StringLength(50,MinimumLength =2 ,ErrorMessage ="Name Must Be Between 2 and 50")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Name must contain letters only.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage ="Invaild Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email Must Be Between 5 and 100")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Gender Is Required")]
        public string Gender { get; set; } = null!;

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateOnly DateOfBirth { get; set; }


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


        [Required (ErrorMessage = "HealthRecord Is Required ")]
        public HealthRecordView HealthRecordVm { get; set; }=null!;
    }
}
