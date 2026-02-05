using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GymManagementSystemBLL.ViewModels.Member;
using GymManagementSystemBLL.ViewModels.Plan;
using GymManagementSystemBLL.ViewModels.Session;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using GymManagementSystemBLL.ViewModels.Trainer;
using GymManagementSystemDAL.Model;
using GymManagementSystemDAL.Model.Enums;
using Microsoft.Data.SqlClient;

namespace GymManagementSystemBLL
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            MapTrainer();
            MapsSession();
            MapMember();
            MapPlan();
        }


        private void MapTrainer()
        {
            CreateMap<CreateTrainerModel, Trainer>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address()
                {
                    City = src.City,
                    Streat = src.Street,
                    BuildingNumber = src.BuildingNumber,
                }));

            CreateMap<Trainer, TrainerModelView>()
                 .ForMember(dest => dest.Specialties,
                 opt => opt.MapFrom(src => $"{src.Address.BuildingNumber} - {src.Address.Streat} - {src.Address.City}"));

            CreateMap<Trainer, TrainerDetailsModel>()
                 .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber}" +
                 $" - {src.Address.Streat} - {src.Address.City}"))
                 .ForMember(dest => dest.DataOfDirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()))

                .ForMember(dest => dest.specilization, opt => opt.MapFrom(src => src.specialties.ToString()));

            CreateMap<Trainer, TrainerToUpdateView>()
                .ForMember(dest => dest.Streat, opt => opt.MapFrom(src => src.Address.Streat))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.Specilization, opt => opt.MapFrom(src => src.specialties.ToString()));
            CreateMap<TrainerToUpdateView, Trainer>().ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
            {
                BuildingNumber = src.BuildingNumber,
                Streat = src.Streat,
                City = src.City
            }));


        }
        private void MapsSession()
        {
            CreateMap<Session, SessionViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.Name))
                .ForMember(dest => dest.AvailableSlots, opt => opt.Ignore());

            CreateMap<UpdateSessionViewModel, Session>().ReverseMap();
            CreateMap<Session, CreateSessionViewModel>().ReverseMap();
            CreateMap<Trainer, TrainerSelectedViewModel>();
            CreateMap<Category, CategorySelectedViewModel>();
        }

        private void MapMember()
        {
            CreateMap<CreateViewMemeberModel, Member>()
                  .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                  {
                      BuildingNumber = src.BuildingNumber,
                      City = src.City,
                      Streat = src.Streat
                  })).ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => src.HealthRecordVm));


            CreateMap<HealthRecordView, HealthRecord>().ReverseMap();

            CreateMap<Member, MemberViewModel>()
           .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.gender.ToString()))
        .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()))
            .ForMember(dest => dest.address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber} - {src.Address.Streat} - {src.Address.City}"));

            CreateMap<Member, MemberToUpdateModelView>()
            .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.Streat, opt => opt.MapFrom(src => src.Address.Streat));

            CreateMap<MemberToUpdateModelView, Member>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Phote, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.City = src.City;
                    dest.Address.Streat = src.Streat;
                    dest.UpdateAt = DateTime.Now;
                });
        }



        private void MapPlan()
        {
            CreateMap<Plan, PlanToUpdate>();
            CreateMap<PlanViewModel, Plan>()
                .ForMember(dest => dest.DurationDays, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.UpdateAt = DateTime.Now;
                }).ReverseMap();

            
        }
    }
}
