using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Storage.Core.Interfaces;
using Storage.DataBase.Exceptions;
using Storage.WebApi.DTO;

namespace Storage.WebApi.Helpers
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

        public virtual async Task<ActionResult> BasicDelete(Func<Task> actionWithRepo)
        {
            try
            {
                await actionWithRepo.Invoke();
                await Uw.Commit();
            }
            catch (NoCascadeDeletionException<DTO> ex)
            {
                return Controller.Conflict(ex.Message);
            }
            catch (NotFound<DTO> ex)
            {
                return Controller.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);

                return Controller.StatusCode(500);
            }

            return Controller.Ok();
        }
        public virtual async Task<ActionResult> BasicPut(Func<Task> actionWithRepo)
        {
            try
            {
                await actionWithRepo.Invoke();
                await Uw.Commit();
            }
            catch (NotFound<DTO> ex)
            {
                return Controller.NotFound();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return Controller.StatusCode(500);
            }
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

            try
            {
                result = await actionWithRepo.Invoke();
                await Uw.Commit();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return Controller.BadRequest();
            }

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
