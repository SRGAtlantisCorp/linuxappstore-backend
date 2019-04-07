using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinuxAppStore_Backend.Data;
using LinuxAppStore_Backend.Data.Entity;
using LinuxAppStore_Backend.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LinuxAppStore_Backend.Controllers
{
    [Route("api/")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class ApiController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        private readonly IConfiguration _configuration;

        public ApiController(
            DataContext context,
            IMapper mapper,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        // GET api/apps
        [HttpGet("Apps")]
        public IEnumerable<LinuxAppModel> Apps(int? type)
        {
            var list = new List<LinuxAppModel>();

            if (type.HasValue)
            {
                list.AddRange(_context.LinuxApps.Where(x => x.Type == type.Value).ProjectTo<LinuxAppModel>(_mapper.ConfigurationProvider));
            } else
            {
                list.AddRange(_context.LinuxApps.ProjectTo<LinuxAppModel>(_mapper.ConfigurationProvider));
            }

            return list;
        }

        [HttpPost("AppImages")]
        public async Task<IActionResult> AppImages([FromBody] LinuxAppPostModel model)
        {
            if (model == null || model.Apps == null)
            {
                return BadRequest();
            }

            var apiKey = _configuration.GetValue<string>("ApiKey");

            if (apiKey != model.ApiKey) {
                return BadRequest();
            }

            var entities = model.Apps.AsQueryable().ProjectTo<LinuxApp>(_mapper.ConfigurationProvider);
            
            // TODO: need a way to update app images if it already exists
            _context.LinuxApps.AddRange(entities);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // GET api/recentlyadded
        [HttpGet("RecentlyAdded")]
        public async Task<IActionResult> RecentlyAdded(int? type, int? limit)
        {
            var query = await _context.GetVwRecentlyAdded();

            if (type.HasValue)
            {
                query = query.Where(x => x.Type == type.Value);
            }

            if (limit.HasValue)
            {
                query = query.Take(limit.Value);
            }

            return Ok(query);
        }

        // GET api/recentlyadded
        [HttpGet("RecentlyUpdated")]
        public async Task<IActionResult> RecentlyUpdated(int? type, int? limit)
        {
            var query = await _context.GetVwRecentlyUpdated();

            if (type.HasValue)
            {
                query = query.Where(x => x.Type == type.Value);
            }

            if (limit.HasValue)
            {
                query = query.Take(limit.Value);
            }

            return Ok(query);
        }

    }
}
