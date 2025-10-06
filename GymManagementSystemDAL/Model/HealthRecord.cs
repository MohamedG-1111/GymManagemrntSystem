using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Model
{
    public class HealthRecord:BaseEntity
    {

        public int Height { get; set; }
        public int Weight { get; set; }

        public string BloodType { get; set; } = null!;
        public string ?Note { get; set; }   =null!;
    }
}
