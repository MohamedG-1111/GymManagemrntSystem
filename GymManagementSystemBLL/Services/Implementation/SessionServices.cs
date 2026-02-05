using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.Session;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using GymManagementSystemDAL.Data.Repository.Interface;
using GymManagementSystemDAL.Model;

namespace GymManagementSystemBLL.Services.Implementation
{
    public class SessionServices : ISessionServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public SessionServices(IUnitOfWork unitOfWork,IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public UpdateSessionViewModel? GetSessionToUpdate(int SessionId)
        {
            var session = _unitOfWork.SessionRepository.Get(SessionId);
            if (session == null) return null;
            if(!ISSessionAvailableToUpdate(session)) return null;
           return mapper.Map<Session, UpdateSessionViewModel>(session);
        }

        public bool UpdateSession(UpdateSessionViewModel UpdatedSession, int Id)
        {
          
            try
            {
                var session = _unitOfWork.SessionRepository.Get(Id);
                if (session == null) return false;
                if (!ISSessionAvailableToUpdate(session)) return false;
                if (!InvaildDate(UpdatedSession.StartDate, UpdatedSession.EndDate)) return false;
                if(!IsTrainerExist(UpdatedSession.TrainerId)) return false;
                mapper.Map(UpdatedSession, session);
                session.UpdateAt=DateTime.Now;
                _unitOfWork.SessionRepository.Update(session);  
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CreateSession(CreateSessionViewModel createSessionViewModel)
        {
           if(!IsTrainerExist(createSessionViewModel.TrainerId)) return false;
           if(!IsCategoryExist(createSessionViewModel.CategoryId)) return false;
           if(!InvaildDate(createSessionViewModel.StartDate,createSessionViewModel.EndDate)) return false;
           if(createSessionViewModel.Capacity > 25 || createSessionViewModel.Capacity < 0) return false;
            var SessionEntity = mapper.Map<CreateSessionViewModel, Session>(createSessionViewModel);
            try
            {
                 _unitOfWork.GetGenericRepository<Session>().Add(SessionEntity);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public IEnumerable<SessionViewModel> GetAll()
        {
            var Sessions = _unitOfWork.SessionRepository.GetAllSessionsWithCategoryAndTrainers();
            if (Sessions == null || !Sessions.Any()) return [];

            var MappedSessions =mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(Sessions);
            foreach (var session in MappedSessions)
            {
                session.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSloted(session.Id);
            }
            return MappedSessions;
        }

        public SessionViewModel? GetById(int id)
        {
            var session = _unitOfWork.SessionRepository.GetSessionsWithCategoryAndTrainers(id);
            if (session == null) return null;
            var sessionMaped=mapper.Map<Session,SessionViewModel>(session);
            sessionMaped.AvailableSlots = sessionMaped.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSloted(sessionMaped.Id);
            return sessionMaped;
        }


        public bool RemoveSession(int SessionId)
        {
            try
            {
                var Session = _unitOfWork.SessionRepository.Get(SessionId);
                if (Session == null) return false;
                if (!ISSessionAvailableToDelete(Session)) return false;
                _unitOfWork.SessionRepository.Delete(Session);
                return _unitOfWork.SaveChanges() > 0;
            }catch (Exception ex)
            {
                return false;
            }

        }




        #region Helper
        private bool IsTrainerExist(int id)
        {
          return  _unitOfWork.GetGenericRepository<Trainer>().Get(id) is not null;
        }

        private bool IsCategoryExist(int id)
        {
            return _unitOfWork.GetGenericRepository<Category>().Get(id) is not null;
        }
        private bool InvaildDate(DateTime StartDate, DateTime EndDate)
        {
            return StartDate < EndDate && DateTime.Now < StartDate;
        }
        private bool ISSessionAvailableToUpdate(Session session)
        {
            if(session == null) return false;   
            if(session.EndDate < DateTime.Now) return false;
            if(session.StartDate <=  DateTime.Now) return false; 
            if(_unitOfWork.SessionRepository.GetCountOfBookedSloted(session.Id) > 0) return false;  
            return true;
        }

        private bool ISSessionAvailableToDelete(Session session)
        {
            if (session == null) return false;
            if (session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now) return false;
            if (session.StartDate > DateTime.Now) return false;
            if (_unitOfWork.SessionRepository.GetCountOfBookedSloted(session.Id) > 0) return false;
            return true;
        }

        public IEnumerable<TrainerSelectedViewModel> GetTrainerDropList()
        {
            var Trainers = _unitOfWork.GetGenericRepository<Trainer>().GetAll();
            return mapper.Map<IEnumerable<TrainerSelectedViewModel>>(Trainers);
        }

        public IEnumerable<CategorySelectedViewModel> GetCategoryDropList()
        {
            var Categories = _unitOfWork.GetGenericRepository<Category>().GetAll();
            return mapper.Map<IEnumerable<CategorySelectedViewModel>>(Categories);
        }




        #endregion
    }
}
