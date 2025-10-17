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
            MapsMemnber();
            MapPlan();
            MapTrainer();





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

            CreateMap<Trainer,TrainerModelView>()
                .ForMember(dest=>dest.Specilization,opt=>opt.MapFrom(src=>src.specialties.ToString()));

            CreateMap<Trainer, TrainerDetailsModel>()
                 .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber}" +
                 $" - {src.Address.Streat} - {src.Address.City}"))
                 .ForMember(dest => dest.DataOfDirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()));

            CreateMap<Trainer, TrainerToUpdateView>()
                .ForMember(dest => dest.Streat, opt => opt.MapFrom(src => src.Address.Streat))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.Specilization, opt => opt.MapFrom(src => src.specialties.ToString()));
            CreateMap<TrainerToUpdateView, Trainer>()
    .ForPath(dest => dest.Address.City, opt => opt.MapFrom(src => src.City))
    .ForPath(dest => dest.Address.Streat, opt => opt.MapFrom(src => src.Streat))
    .ForPath(dest => dest.Address.BuildingNumber, opt => opt.MapFrom(src => src.BuildingNumber))
    .AfterMap((src, dest) =>
    {
        dest.UpdateAt = DateTime.Now;
    });


        }
        private void MapsSession()
        {
            CreateMap<Session, SessionViewModel>()
        .ForMember(dest => dest.CategoryName, Options => Options.MapFrom(src => src.Category.Name))
        .ForMember(dest => dest.TrainerName, Options => Options.MapFrom(src => src.Trainer.Name))
        .ForMember(dest => dest.AvailableSlots, Options => Options.Ignore());
            CreateMap<CreateSessionViewModel, Session>().ReverseMap();
        }
        private void MapsMemnber()
        {
            CreateMap<CreateViewMemeberModel, Member>()
               .ForMember(dest => dest.Address, Options => Options.MapFrom(src => src))
               .ForMember(dest => dest.HealthRecord, opts => opts.MapFrom(src => src.HealthRecordVm));

            CreateMap<HealthRecordView, HealthRecord>().ReverseMap();


            CreateMap<CreateViewMemeberModel, Address>()
     .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
     .ForMember(dest => dest.Streat, opt => opt.MapFrom(src => src.Streat))
     .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.BuildingNumber));


            CreateMap<Member, MemberToUpdateModelView>()
                .ForMember(dest => dest.Streat, Opt => Opt.MapFrom(src => src.Address.Streat))
                .ForMember(dest => dest.City, Opt => Opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.BuildingNumber, Opt => Opt.MapFrom(src => src.Address.BuildingNumber));

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

            CreateMap<Member, MemberViewModel>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.gender.ToString()))
                .ForMember(dest => dest.DateOfBirth, Opt => Opt.MapFrom(src => src.DateOfBirth.ToShortDateString()))
                .ForMember(dest => dest.address, opt => opt.MapFrom(src => $" {src.Address.BuildingNumber} - {src.Address.Streat} - {src.Address.City}"));
        }


        private void MapPlan()
        {
            CreateMap<Plan, PlanViewModel>().ReverseMap();
            CreateMap<Plan, PlanToUpdate>();
            CreateMap<PlanViewModel, Plan>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.UpdateAt = DateTime.Now;
                });
            
        }
    }
}
