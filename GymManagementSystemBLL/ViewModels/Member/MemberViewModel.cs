using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Model;

namespace GymManagementSystemBLL.ViewModels.Member
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string?Phote { get; set; }

        public string Phone {  get; set; } = null!;

        public string Gender { get; set; } = null!;

     // --------------------------------
        public string ? address { get; set; }

        public DateOnly? DateOfBirth {  get; set; }

        public string ?MemberShipStartDate { get; set; }
        public string ? MembershipEndDate { get; set; }

        public string ?PlanName{ get; set; }

        
    }
}
