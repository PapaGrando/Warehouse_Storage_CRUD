using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Storage.Core.Interfaces;
using Storage.Core.Models;
using Storage.WebApi.DTO;

namespace Storage.WebApi.Controllers
{
    public abstract class StorageBaseController<DTO, CT> : ControllerBase where DTO : class, IBaseDTO
    {
        protected readonly IUnitOfWorkAsync Uw;
        protected readonly ILogger Logger;
        protected readonly IMapper Mapper;

        public StorageBaseController(IUnitOfWorkAsync uw, ILogger<CT> logger, IMapper mapper)
        {
            Uw = uw;
            Logger = logger;
            Mapper = mapper;
        }
        public abstract Task<IEnumerable<DTO>> GetAll();
        public abstract Task<IEnumerable<DTO>> GetListWithParameters(QuerySettings query);
        public abstract Task<ActionResult<object>> Get(int id);
        public abstract Task<ActionResult> Post(DTO value);
        public abstract Task<ActionResult> Put(int id, DTO value);
        public abstract Task<ActionResult> Delete(int id);
    }
}
