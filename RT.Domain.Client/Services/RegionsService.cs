using System.Collections.Generic;
using System.Threading.Tasks;
using RT.Core;
using RT.Core.DB;
using RT.Domain.Models;

namespace RT.Domain.Services
{
    public interface IRegionsService : IDependency
    {
        Task<IEnumerable<Region>> GetRegionsByParentId(int? parentId);
        Task<Region> GetRegionById(int regionId);
    }
    public class RegionsService : IRegionsService
    {
        private readonly IAsyncRepository<Region> _regionRepository;

        public RegionsService(IAsyncRepository<Region> regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<IEnumerable<Region>> GetRegionsByParentId(int? parentId)
        {
            return await _regionRepository.Fetch(r => r.ParentId == parentId);
        }

        public async Task<Region> GetRegionById(int regionId)
        {
            return await _regionRepository.Get(regionId);
        }
    }
}
