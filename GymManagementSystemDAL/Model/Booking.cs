using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Model
{
    public class Booking:BaseEntity
    {
        public int MemberId { get; set; }
        public Member member { get; set; } = null!;

        public Session session { get; set; } = null!;
        public int SessionId { get; set; }

        // CreatedAt ==>BookingDate
        public bool IsAttend { get; set; }  



    }
}
