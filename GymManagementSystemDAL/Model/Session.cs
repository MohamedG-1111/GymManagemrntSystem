using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Model
{
    public class Session:BaseEntity
    {
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public ICollection<Booking> SessionsBooking { get; set; } = new List<Booking>();


        public Category Category { get; set; } = null!;

        public int CategoryId { get; set; }  
       
         
         public int TrainerId { get; set; } 
         public Trainer Trainer { get; set; } = null!;
    }
}
