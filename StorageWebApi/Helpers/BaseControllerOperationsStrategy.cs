using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Storage.Core.Interfaces;
using Storage.DataBase.Exceptions;
using WarehouseCRUD.Storage.Helpers;

namespace Storage.Core.Helpers
{
    public class BaseControllerOperationsStrategy<DTO>
        where DTO : class, IBaseDTO
    {
        protected readonly IUnitOfWorkAsync Uw;
        protected readonly ILogger Logger;
        protected readonly IMapper Mapper;
        protected readonly ControllerBase Controller;

        public BaseControllerOperationsStrategy(
            IUnitOfWorkAsync uw,
            ILogger logger,
            IMapper mapper,
            ControllerBase controller)
        {
            Uw = uw;
            Logger = logger;
            Mapper = mapper;
            Controller = controller;
        }

        public virtual async Task<IEnumerable<DTO>> BasicGetAll<T>(Func<Task<EntityListRepoData<T>>> actionWithRepo)
        {
            var data = await actionWithRepo.Invoke();

            Controller.AddHeadersToResponse<ProductCategoryDTO>(
                (nameof(data.TotalCount), data.TotalCount),
                (nameof(data.CountInList), data.CountInList));

            return data.Entities?.Select(x => Mapper.Map<DTO>(x)).ToArray();
        }

        public virtual async Task<ActionResult> BasicDelete(Func<Task> actionWithRepo)
        {
            await actionWithRepo.Invoke();
            await Uw.Commit();

            return Controller.Ok();
        }
        public virtual async Task<ActionResult> BasicPut(Func<Task> actionWithRepo)
        {

            await actionWithRepo.Invoke();
            await Uw.Commit();

            return Controller.Ok();
        }

        public virtual async Task<ActionResult> BasicPost(DTO value, Func<Task<object>> actionWithRepo,
            string createdAtActionMethodName)
        {
            if (value is null)
                return Controller.BadRequest();

            value.Id = 0;
            DTO outVal;
            object result;

            result = await actionWithRepo.Invoke();
            await Uw.Commit();

            try
            {
                outVal = Mapper.Map<DTO>(result);

                //TODO :пофиксить роутинг CreatedAtAction к методу GET
                return Controller.Created(createdAtActionMethodName, outVal);
            }
            catch
            {
                return Controller.Ok("created, automapper DTO parse error");
            }
        }
    }
}
