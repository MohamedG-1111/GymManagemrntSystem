using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Model
{
    public class Plan:BaseEntity
    {

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;    
        public int DurationDays { get; set; }
        public int Price { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Membership> PlanMembers= new List<Membership>();
    }
}
