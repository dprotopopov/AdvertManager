using System.Collections.Generic;
using System.Threading.Tasks;
using RT.Core;
using RT.Core.DB;
using RT.Domain.Models;

namespace RT.Domain.Services
{

    public interface IRubricsService : IDependency
    {
        Task<IEnumerable<Rubric>> GetRubricsByParentId(int? parentId);
        Task<Rubric> GetRubricById(int rubricId);
    }
    public class RubricsService : IRubricsService
    {
        private readonly IAsyncRepository<Rubric> _rubricRepository;

        public RubricsService(IAsyncRepository<Rubric> rubricRepository)
        {
            _rubricRepository = rubricRepository;
        }

        public async Task<IEnumerable<Rubric>> GetRubricsByParentId(int? parentId)
        {
            return await _rubricRepository.Fetch(r => r.ParentId == parentId);
        }

        public async Task<Rubric> GetRubricById(int rubricId)
        {
            return await _rubricRepository.Get(rubricId);
        }
    }
}
