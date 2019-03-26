using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinuxAppStore_Backend.Data;
using LinuxAppStore_Backend.Model;
using Microsoft.AspNetCore.Mvc;

namespace LinuxAppStore_Backend.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ApiController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ApiController(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET api/apps
        [HttpGet("Apps")]
        public IEnumerable<LinuxAppModel> Apps()
        {
            var entities = _context.LinuxApps.ToList();

            var list = _context.LinuxApps.ProjectTo<LinuxAppModel>(_mapper.ConfigurationProvider).AsEnumerable();

            return list;
        }
    }
}
