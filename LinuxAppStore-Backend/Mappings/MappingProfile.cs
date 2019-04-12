using AutoMapper;
using LinuxAppStore_Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinuxAppStore_Backend.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Data.Entity.LinuxApp, LinuxAppModel>();
            CreateMap<Data.Entity.LinuxApp, Data.Entity.LinuxApp>()
            .ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}
