using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Model.Enums;

namespace GymManagementSystemDAL.Model
{
    public class Trainer:GymUser
    {
        public Specialties specialties {  get; set; }

        public ICollection<Session> TrainerSeesions { get; set; }  = new HashSet<Session>();
    }
}
