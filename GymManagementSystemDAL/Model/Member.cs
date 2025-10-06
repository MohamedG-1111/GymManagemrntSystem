using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Model
{
    public class Member : GymUser
    {
        public string ? Phote {  get; set; }

        public HealthRecord HealthRecord { get; set; } = null!;

        public ICollection<Membership> Membersplans { get; set; }= new List<Membership>();

        public ICollection<Booking> MembersBooking { get; set; }=new List<Booking>();

    }
    
}
