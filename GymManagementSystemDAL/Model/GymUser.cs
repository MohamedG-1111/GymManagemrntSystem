using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Model.Enums;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystemDAL.Model
{
    public abstract class GymUser:BaseEntity
    {
        public string Name { get; set; }  = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public DateOnly DateOfBirth {  get; set; }

        public Gender gender { get; set; }
        public Address Address { get; set; }=null!;
    }
    [Owned]
    public class Address
    {
        public int BuildingNumber { get; set; }
        public string City { get; set; } =null!;
        public string Streat { get; set; } = null!;
    }
}
