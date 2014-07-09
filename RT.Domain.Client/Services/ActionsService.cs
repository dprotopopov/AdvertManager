using System.Collections.Generic;
using System.Threading.Tasks;
using RT.Core;
using RT.Core.DB;
using RT.Domain.Models;

namespace RT.Domain.Services
{
    /// <summary>
    /// Интерфейс сервиса работы с действиями
    /// </summary>
    public interface IActionsService : IDependency
    {
        /// <summary>
        /// Получить действия
        /// </summary>
        /// <returns>Списое действий</returns>
        Task<IEnumerable<Action>> GetActions();
        /// <summary>
        /// Получить действие по ИД
        /// </summary>
        /// <param name="actionId">ИД действия</param>
        /// <returns>Действие</returns>
        Task<Action> GetActionById(int actionId);
    }
    /// <summary>
    /// Сервис работы с действиями
    /// </summary>
    public class ActionsService : IActionsService
    {
        private readonly IAsyncRepository<Action> _actionRepository;

        /// <summary>
        /// Конструктор сервиса работы с действиями
        /// </summary>
        public ActionsService(IAsyncRepository<Action> actionRepository)
        {
            _actionRepository = actionRepository;
        }

        /// <summary>
        /// Получить действия
        /// </summary>
        /// <returns>Списое действий</returns>
        public async Task<IEnumerable<Action>> GetActions()
        {
           return await _actionRepository.Fetch();
        }

        /// <summary>
        /// Получить действие по ИД
        /// </summary>
        /// <param name="actionId">ИД действия</param>
        /// <returns>Действие</returns>
        public async Task<Action> GetActionById(int actionId)
        {
            return await _actionRepository.Get(actionId);
        }
    }
}
