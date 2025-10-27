using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels;
using GymManagementSystemBLL.ViewModels.Member;
using GymManagementSystemDAL.Data.Repository.Implementation;
using GymManagementSystemDAL.Data.Repository.Interface;
using GymManagementSystemDAL.Model;

namespace GymManagementSystemBLL.Services.Implementation
{
    public class MemberServices : IMemberServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public MemberServices(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public bool CreateMember(CreateViewMemeberModel MemberCreated)
        {
            if (IsEmailExist(MemberCreated.Email) || IsPhoneExist(MemberCreated.Phone))
                return false;
            var member = _mapper.Map< CreateViewMemeberModel ,Member>(MemberCreated);
             unitOfWork.GetGenericRepository<Member>().Add(member);
            return unitOfWork.SaveChanges() > 0;
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            try
            {
                var Members = unitOfWork.GetGenericRepository<Member>().GetAll();
                if (!Members.Any()) return [];
                return _mapper.Map<IEnumerable<MemberViewModel>>(Members);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return [];
            }
        }

        public HealthRecordView? GetHealthRecord(int Id)
        {
            var MemberHealthRecord = unitOfWork.GetGenericRepository<HealthRecord>().Get(Id);
            if(MemberHealthRecord == null) return null;
            var HealthRecordView = _mapper.Map<HealthRecordView>(MemberHealthRecord);
            return HealthRecordView;
        }

        public MemberToUpdateModelView? getMemberToUpdate(int Id)
        {
            var Member = unitOfWork.GetGenericRepository<Member>().Get(Id);
            if (Member == null) return null;
            return _mapper.Map<Member,MemberToUpdateModelView>(Member);
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
            var MemberDetails = _mapper.Map<MemberViewModel>(Member);
            var ActiveMemberShip = unitOfWork.GetGenericRepository<Membership>().GetAll(x => x.MemberId == Member.Id).Where(x=>x.Status == "Active")
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

            var SeesionsIds = unitOfWork.GetGenericRepository<Booking>()
                .GetAll(x => x.MemberId == id)
                 .Select(x => x.SessionId);


            var hasMemberSession = unitOfWork.GetGenericRepository<Session>()
                .GetAll(x => SeesionsIds.Contains(x.Id) && x.StartDate > DateTime.Now).Any();

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
                var member = unitOfWork.GetGenericRepository<Member>().Get(Id);
                if (member == null) return false;
                var EmailExist = unitOfWork.GetGenericRepository<Member>().GetAll(x => x.Email == MemberToUpdate.Email && x.Id != Id).Any();
                var PhoneExist = unitOfWork.GetGenericRepository<Member>().GetAll(x => x.Phone == MemberToUpdate.Phone && x.Id != Id).Any();
                if (EmailExist || PhoneExist) return false;
                _mapper.Map(MemberToUpdate, member);

                unitOfWork.GetGenericRepository<Member>().Update(member);
                return unitOfWork.SaveChanges()> 0;
                

            }
            catch
            {
                return false;
            }
        }
    }
}
