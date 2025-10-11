using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.ViewModels.Plan
{
    public class PlanToUpdate
    {
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(100,MinimumLength=5,ErrorMessage ="Length Between 5 and 100")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Length must be between 5 and 200")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Duration is Required")]
        [Range(1,365,ErrorMessage ="Must Between 1 and 365")]
        public int DurationDay { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        [Range(0.1, 10000.0, ErrorMessage = "Price must be between 0.1 and 10000")]
        public decimal Price { get; set; }
    }
}
