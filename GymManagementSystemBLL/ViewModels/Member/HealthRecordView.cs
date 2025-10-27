using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.ViewModels.Member
{
    public class HealthRecordView
    {

        [StringLength(100, MinimumLength = 10, ErrorMessage = "Note Must Be Between 10 and 100")]
        public string? Note { get; set; }

        [Required(ErrorMessage = "BloodType Is Required")]
        public string BloodType { get; set; } = null!;
        [Required(ErrorMessage = "Height is required")]
        [Range(0.1, 500, ErrorMessage = "Height must be between 0.1 and 350 cm")]
        public decimal Height { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        [Range(0.1, 500, ErrorMessage = "Weight must be between 0.1 and 350 kg")]
        public decimal Weight { get; set; }

    }
}
