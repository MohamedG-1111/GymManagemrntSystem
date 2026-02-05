using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.Analytical;
using GymManagementSystemDAL.Data.Repository.Interface;
using GymManagementSystemDAL.Model;

namespace GymManagementSystemBLL.Services.Implementation
{
    public class AnalyticalService : IAnalyticalService
    {
        private readonly IUnitOfWork unitOfWork;

        public AnalyticalService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public AnalyticalViewModel? GetAnalytical()
        {
            return new AnalyticalViewModel()
            {
                TotalMembers = unitOfWork.GetGenericRepository<Member>().GetAll().Count(),
                TotalTrainers = unitOfWork.GetGenericRepository<Trainer>().GetAll().Count(),
                ActiveMembers = unitOfWork.GetGenericRepository<Membership>().GetAll().Where(x => x.Status == "Active").Count(),
                OngoingSessions = unitOfWork.GetGenericRepository<Session>().GetAll(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Count(),

                UpcomingSessions = unitOfWork.GetGenericRepository<Session>().GetAll(x => x.StartDate > DateTime.Now).Count(),
                CompletedSessions=unitOfWork.GetGenericRepository<Session>().GetAll(x=>x.EndDate < DateTime.Now).Count()
            };
        }
    }
}
