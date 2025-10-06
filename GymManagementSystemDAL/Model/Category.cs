using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Model
{
    public class Category:BaseEntity
    {
        public string Name { get; set; } = null!;

        public ICollection<Session> Sessions { get; set; }= new HashSet<Session>(); 
    }
}
