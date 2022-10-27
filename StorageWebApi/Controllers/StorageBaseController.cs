using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Storage.Core.Helpers;
using Storage.Core.Interfaces;
using Storage.Core.Models;

namespace Storage.Core.Controllers
{
    public abstract class StorageBaseController<DTO, CT> : ControllerBase
        where DTO : class, IBaseDTO
    {
        protected readonly IUnitOfWorkAsync Uw;
        protected readonly ILogger Logger;
        protected readonly IMapper Mapper;
        protected readonly BaseControllerOperationsStrategy<DTO> BaseControllerOperations;

        public StorageBaseController(IUnitOfWorkAsync uw, ILogger<CT> logger, IMapper mapper, ControllerBase controller = null)
        {
            Uw = uw;
            Logger = logger;
            Mapper = mapper;
            BaseControllerOperations =
                new BaseControllerOperationsStrategy<DTO>(Uw, Logger, Mapper, controller ?? this);
        }
        public abstract Task<IEnumerable<DTO>> GetAll();
        public abstract Task<IEnumerable<DTO>> GetListWithParameters(QuerySettings query);
        public abstract Task<ActionResult<object>> Get(int id);
        public abstract Task<ActionResult> Post(DTO value);
        public abstract Task<ActionResult> Put(int id, DTO value);
        public abstract Task<ActionResult> Delete(int id);
    }
}
