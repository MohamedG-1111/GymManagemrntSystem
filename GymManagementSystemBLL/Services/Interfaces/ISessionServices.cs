using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemBLL.ViewModels.Session;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using GymManagementSystemDAL.Data.Repository.Implementation;
using GymManagementSystemDAL.Data.Repository.Interface;
using GymManagementSystemDAL.Model;

namespace GymManagementSystemBLL.Services.Interfaces
{
    public interface ISessionServices
    {
        public IEnumerable<SessionViewModel> GetAll();

        public SessionViewModel GetById(int id);

        public bool CreateSession(CreateSessionViewModel createSessionViewModel);

        public UpdateSessionViewModel? GetSessionToUpdate(int SessionId);
        public bool UpdateSession(UpdateSessionViewModel updateSessionViewModel,int Id); 
        
        public bool RemoveSession(int SessionId);   
    }
}
