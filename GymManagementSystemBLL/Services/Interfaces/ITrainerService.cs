using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemBLL.ViewModels.Trainer;

namespace GymManagementSystemBLL.Services.Interfaces
{
    public interface ITrainerService
    {
        IEnumerable<TrainerModelView> GetTrainers();


        TrainerDetailsModel?GetDetails(int id);


        public TrainerToUpdateView? GetTrainerToUpdate(int id);


        public bool Update(int id, TrainerToUpdateView TrainerView);


        public bool Delete(int id);  

         bool CreateTrainer(CreateTrainerModel createTrainerModel);

    }
}
