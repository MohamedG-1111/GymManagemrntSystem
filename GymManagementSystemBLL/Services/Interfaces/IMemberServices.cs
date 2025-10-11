using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemBLL.ViewModels;
using GymManagementSystemBLL.ViewModels.Member;

namespace GymManagementSystemBLL.Services.Interfaces
{
    public interface IMemberServices
    {
        IEnumerable<MemberViewModel> GetAllMembers();

        bool CreateMember(CreateViewMemeberModel MemberCreated);

        MemberViewModel ?MemberDetails(int Id);


        HealthRecordView? GetHealthRecord(int Id);

        public MemberToUpdateModelView? getMemberToUpdate(int Id);


        bool UpdateMember(int Id, MemberToUpdateModelView MemberToUpdate);

        bool RemoveMember(int id);

    }
}
