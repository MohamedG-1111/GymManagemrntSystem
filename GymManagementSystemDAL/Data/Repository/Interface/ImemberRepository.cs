using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Model;

namespace GymManagementSystemDAL.Repository.Interface
{
    public interface ImemberRepository
    {
        IEnumerable<Member> GetAll();
        Member?GetMember(int id);

        int Add(Member member);

        int Update(Member member);  

        int Delete(Member member);

    }
}
