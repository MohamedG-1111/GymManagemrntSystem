using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.Trainer;
using GymManagementSystemDAL.Data.Repository.Implementation;
using GymManagementSystemDAL.Model;
using GymManagementSystemDAL.Model.Enums;

namespace GymManagementSystemBLL.Services.Implementation
{
    public class TrainerService : ITrainerService
    {
        private readonly UnitOfWork unitOfWork;

        public TrainerService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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
            return new TrainerDetailsModel()
            {
                Name = trainer.Name,
                Phone = trainer.Phone,
                Address = $"{trainer.Address.BuildingNumber} - {trainer.Address.Streat} - {trainer.Address.City}",
                DataOfDirth=trainer.DateOfBirth.ToShortDateString(),
            };
        }

        public IEnumerable<TrainerModelView> GetTrainers()
        {
           var Trainers=unitOfWork.GetGenericRepository<Trainer>().GetAll();
            return Trainers.Select(x => new TrainerModelView()
            {
                Name = x.Name,
                Phone = x.Phone,
                Email = x.Email,
                Specilization = x.specialties.ToString(),
            });
        }

        public TrainerToUpdateView? GetTrainerToUpdate(int id)
        {
            var trainer = unitOfWork.GetGenericRepository<Trainer>().Get(id);
            if (trainer == null) return null;
            return new TrainerToUpdateView()
            {
                Name = trainer.Name,
                Phone = trainer.Phone,
                Email = trainer.Email,
                City = trainer.Address.City,
                Streat = trainer.Address.Streat,
                BuildingNumber = trainer.Address.BuildingNumber,
                Specilization = trainer.specialties.ToString()

            };
        }

        public bool Update(int id, TrainerToUpdateView TrainerView)
        {
            var Repo = unitOfWork.GetGenericRepository<Trainer>();
                var trainer=Repo.Get(id);
            if (trainer == null) return false;
            if (IsEmailExist(trainer.Email) || IsPhoneExist(trainer.Phone)) return false;
            try
            {
                trainer.Email = TrainerView.Email;
                trainer.Phone = TrainerView.Phone;
                trainer.Address.City = TrainerView.City;
                trainer.Address.Streat = TrainerView.Streat;
                trainer.Address.BuildingNumber = TrainerView.BuildingNumber;
                trainer.specialties = Enum.Parse<Specialties>(TrainerView.Specilization);
                trainer.UpdateAt = DateTime.Now;
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
            var trainer = new Trainer()
            {
                Name = createTrainerModel.Name,
                Phone = createTrainerModel.Phone,
                Email = createTrainerModel.Email,
                gender = Enum.Parse<Gender>(createTrainerModel.Gender),
                Address=new Address()
                {
                    City= createTrainerModel.City,
                    Streat= createTrainerModel.Street,
                    BuildingNumber= createTrainerModel.BuildingNumber,  

                },
                DateOfBirth=createTrainerModel.DateOfBirth, 
                specialties = Enum.Parse<Specialties>(createTrainerModel.Specialization),
                CreatedAt=DateTime.Now,
            };
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
