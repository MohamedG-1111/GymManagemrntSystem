using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.ViewModels.Trainer
{
    public class CreateTrainerModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name must contain letters only.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email must be between 5 and 100 characters.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Invalid Egyptian phone number.")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; } = null!;

        [Required(ErrorMessage = "Building number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Building number must be a positive integer.")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Street name is required.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Street name must contain letters and spaces only.")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "City is required.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "City name must contain letters and spaces only.")]
        public string City { get; set; } = null!;


        [Required(ErrorMessage = "Specilization Is Required")]
        public string Specialization { get; set; } = null!;
    }
}
