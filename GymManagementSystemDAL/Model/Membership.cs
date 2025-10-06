using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Model
{
    public class Membership:BaseEntity
    {
        public int MemberId { get; set; }
        public Member member { get; set; } = null!;

        public Plan plan { get; set; } = null!;
        public int PlanId { get; set; }


        // Start Date == > CreatedAt

        public  DateTime EndDate { get; set; }


        public string Status
        {
            get
            {
                if (EndDate > DateTime.Now)
                    return "Expired";
                else
                    return "Active";
            }
        }
    }
}
