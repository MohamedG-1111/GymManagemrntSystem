using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.Trainer;
using GymManagementSystemDAL.Data.Repository.Implementation;
using GymManagementSystemDAL.Data.Repository.Interface;
using GymManagementSystemDAL.Model;
using GymManagementSystemDAL.Model.Enums;

namespace GymManagementSystemBLL.Services.Implementation
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TrainerService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public bool Delete(int id)
        {
            var Repo = unitOfWork.GetGenericRepository<Trainer>();
           var trainer= Repo.Get(id);
            if(trainer == null || HasActiveSeesion(id)) return false;
            try
            {
                Repo.Delete(trainer);
                return unitOfWork.SaveChanges()> 0;
            }
            catch
            {
                return false;
            }
           
        }
       


        public TrainerDetailsModel? GetDetails(int id)
        {
            var trainer= unitOfWork.GetGenericRepository<Trainer>().Get(id);
            if (trainer == null) return null;
           return mapper.Map<TrainerDetailsModel>(trainer); 
        }

        public IEnumerable<TrainerModelView> GetTrainers()
        {
           var Trainers=unitOfWork.GetGenericRepository<Trainer>().GetAll();
            return mapper.Map<IEnumerable<TrainerModelView>>(Trainers);  
        }

        public TrainerToUpdateView? GetTrainerToUpdate(int id)
        {
            var trainer = unitOfWork.GetGenericRepository<Trainer>().Get(id);
            if (trainer == null) return null;
            return mapper.Map<TrainerToUpdateView>(trainer);
        }

        public bool Update(int id, TrainerToUpdateView TrainerView)
        {
            var Repo = unitOfWork.GetGenericRepository<Trainer>();
                var trainer=Repo.Get(id);
            if (trainer == null) return false;
            var EmailExist = unitOfWork.GetGenericRepository<Member>().GetAll(x => x.Email == TrainerView.Email && x.Id != id).Any();
            var PhoneExist = unitOfWork.GetGenericRepository<Member>().GetAll(x => x.Phone == TrainerView.Phone && x.Id != id).Any();
            if (EmailExist || PhoneExist) return false; try
            {
                mapper.Map(TrainerView, trainer);
                Repo.Update(trainer);
                return unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;   
            }
        }

        private bool HasActiveSeesion(int id)
        {
           var trainerSession=unitOfWork.GetGenericRepository<Session>().GetAll(x=>x.TrainerId==id
           && x.StartDate> DateTime.Now);
            return trainerSession.Any();
        }

        public bool IsEmailExist(string Email)
        {
            return unitOfWork.GetGenericRepository<Trainer>().GetAll(x => x.Email == Email).Any();
        }
        public bool IsPhoneExist(string Phone)
        {
            return unitOfWork.GetGenericRepository<Trainer>().GetAll(x => x.Phone == Phone).Any();
        }

        public bool CreateTrainer(CreateTrainerModel createTrainerModel)
        {
            var Repo=unitOfWork.GetGenericRepository<Trainer>();    
            if (createTrainerModel == null) return false;
            var trainer=mapper.Map<Trainer>(createTrainerModel);
            try
            {
                Repo.Add(trainer);
                return unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
