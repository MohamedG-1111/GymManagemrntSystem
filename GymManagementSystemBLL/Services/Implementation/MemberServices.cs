using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels;
using GymManagementSystemBLL.ViewModels.Member;
using GymManagementSystemDAL.Data.Repository.Interface;
using GymManagementSystemDAL.Model;

namespace GymManagementSystemBLL.Services.Implementation
{
    public class MemberServices : IMemberServices
    {
        private readonly IUnitOfWork unitOfWork;

        public MemberServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public bool CreateMember(CreateViewMemeberModel MemberCreated)
        {
            if (IsEmailExist(MemberCreated.Email) || IsPhoneExist(MemberCreated.Phone))
                return false;
            var member = new Member()
            {
                Email = MemberCreated.Email,
                Phone = MemberCreated.Phone,
                Name = MemberCreated.Name,
                DateOfBirth = MemberCreated.DateOfBirth,

                Address = new Address()
                {
                    City = MemberCreated.City,
                    Streat = MemberCreated.Streat,
                    BuildingNumber = MemberCreated.BuildingNumber,
                }
                ,
                 HealthRecord = new HealthRecord()
                 {
                     Height = MemberCreated.HealthRecord.Height,
                     Weight = MemberCreated.HealthRecord.Weight,
                     BloodType = MemberCreated.HealthRecord.BloodType,
                     Note = MemberCreated.HealthRecord.Note
                 }

            };
             unitOfWork.GetGenericRepository<Member>().Add(member);
            return unitOfWork.SaveChanges() > 0;
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = unitOfWork.GetGenericRepository<Member>().GetAll();
            var MemberViews = members.Select(x => new MemberViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Gender=x.gender.ToString(),
                Phone=x.Phone,
                Phote=x.Phote,
                Email=x.Email,
            });
            return MemberViews;
        }

        public HealthRecordView? GetHealthRecord(int Id)
        {
            var Member=unitOfWork.GetGenericRepository<Member>().Get(Id);
            if(Member == null) return null;
            var HealthRecord= new HealthRecordView()
            {
                Height = Member.HealthRecord.Height,
                Weight = Member.HealthRecord.Weight,
                BloodType = Member.HealthRecord.BloodType,
                Note = Member.HealthRecord.Note
            };
            return HealthRecord;
        }

        public MemberToUpdateModelView? getMemberToUpdate(int Id)
        {
            var Member=unitOfWork.GetGenericRepository<Member>().Get(Id);
            if(Member == null) return null;
            var MemberToUpdate = new MemberToUpdateModelView()
            {
                Name = Member.Name,
                Email = Member.Email,
                Phote = Member.Phote,
                Phone = Member.Phone,   
                City=Member.Address.City,
                Streat=Member.Address.Streat,
                BuildingNumber = Member.Address.BuildingNumber, 
            };
            return MemberToUpdate;
        }

        public bool IsEmailExist(string Email)
        {
            return unitOfWork.GetGenericRepository<Member>().GetAll(x=>x.Email == Email).Any();
        }
        public bool IsPhoneExist(string Phone)
        {
            return unitOfWork.GetGenericRepository<Member>().GetAll(x => x.Phone == Phone).Any();
        }

        public MemberViewModel? MemberDetails(int Id)
        {
            var Member= unitOfWork.GetGenericRepository<Member>().Get(Id);
            
            if(Member == null) return null;
            var MemberDetails = new MemberViewModel()
            {
                Name=Member.Name,
                Phone=Member.Phone,
                Email=Member.Email,
                Phote=Member.Phote,
                Gender=Member.gender.ToString(),
                DateOfBirth=Member.DateOfBirth,
                address=$"{Member.Address.BuildingNumber} - {Member.Address.Streat} - {Member.Address.City}"
            };
            var ActiveMemberShip= unitOfWork.GetGenericRepository<Membership>().GetAll(x=>x.MemberId==Member.Id && x.Status=="Active")
                .FirstOrDefault();
            
            if(ActiveMemberShip != null)
            {
                MemberDetails.MemberShipStartDate= ActiveMemberShip.CreatedAt.ToLongDateString();
                MemberDetails.MembershipEndDate = ActiveMemberShip.EndDate.ToLongDateString();
                var plan = unitOfWork.GetGenericRepository<Plan>().Get(ActiveMemberShip.PlanId);
                MemberDetails.PlanName = plan?.Name;
            }
            return MemberDetails;
        }

        public bool RemoveMember(int id)
        {
            var member = unitOfWork.GetGenericRepository<Member>().Get(id);
            if (member == null)
                return false;

            var hasMemberSession = unitOfWork.GetGenericRepository<Booking>()
                .GetAll(x => x.MemberId == id && x.session.StartDate > DateTime.Now)
                .Any();

            if (hasMemberSession)
                return false;

            var memberShips = unitOfWork.GetGenericRepository<Membership>().GetAll(x => x.MemberId == id);

            try
            {
                if (memberShips.Any())
                {
                    foreach (var membership in memberShips)
                        unitOfWork.GetGenericRepository<Membership>().Delete(membership);
                }

                unitOfWork.GetGenericRepository<Member>().Delete(member);
                return unitOfWork.SaveChanges()> 0;
            }
            catch
            {
                return false;
            }
        }


        public bool UpdateMember(int Id, MemberToUpdateModelView MemberToUpdate)
        {
            try
            {
                var Member = unitOfWork.GetGenericRepository<Member>().Get(Id);
                if (Member == null) return false;
                if (IsEmailExist(MemberToUpdate.Email) || IsPhoneExist(MemberToUpdate.Phone)) return false;
                Member.Email = MemberToUpdate.Email;
                Member.Phone = MemberToUpdate.Phone;
                Member.Address.City = MemberToUpdate.City;
                Member.Address.Streat=MemberToUpdate.Streat;
                Member.Address.BuildingNumber = MemberToUpdate.BuildingNumber;
                Member.UpdateAt=DateTime.Now;

                unitOfWork.GetGenericRepository<Member>().Update(Member);
                return unitOfWork.SaveChanges()> 0;
                

            }
            catch
            {
                return false;
            }
        }
    }
}
