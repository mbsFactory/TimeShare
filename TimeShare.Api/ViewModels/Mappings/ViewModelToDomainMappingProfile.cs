using AutoMapper;
using TimeShare.Model;
using TimeShare.Model.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TimeShare.Api.ViewModels.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ScheduleViewModel, Schedule>();
            // verificare il mapping
               //.ForMember(s => s.Creator, map => map.UseValue(null))
               //.ForMember(s => s.Attendees, map => map.UseValue(new List<Attendee>()));

            CreateMap<UserViewModel, User>();
        }

        //protected override void Configure()
        //{
            
        //}
    }
}
