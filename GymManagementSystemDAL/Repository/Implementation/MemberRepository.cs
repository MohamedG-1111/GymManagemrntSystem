using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Data;
using GymManagementSystemDAL.Model;
using GymManagementSystemDAL.Repository.Interface;

namespace GymManagementSystemDAL.Repository.Implementation
{
    public class MemberRepository : ImemberRepository
    {
        private readonly AppDbContext _context;

        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }
        public int Add(Member member)
        {
            _context.Members.Add(member);
            return _context.SaveChanges();
        }

        public int Delete(int Id)
        {
            var mem = _context.Members.Find(Id);
            if (mem == null) return 0;
            _context.Members.Remove(mem);
            return _context.SaveChanges();
        }

        public IEnumerable<Member> GetAll()
        {
            return _context.Members.ToList();
        }

        public Member? GetMember(int id)
        {
                return _context.Members.Find(id);
        }

        public int Update(Member member)
        {
            _context.Members.Update(member);
            return _context.SaveChanges();  
        }
    }
}
