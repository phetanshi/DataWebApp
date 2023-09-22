using AutoMapper;
using Ps.EfCoreRepository.SqlServer;

namespace DataWebApp.Api.Services.Definitions
{
    public abstract class ServiceBase
    {
        public ServiceBase(IRepository repository, ILogger logger, IConfiguration config)
        {
            Repository = repository;
            Logger = logger;
            Config = config;
        }
        public ServiceBase(IRepository repository, ILogger logger, IConfiguration config, IHttpContextAccessor context)
        {
            Repository = repository;
            Logger = logger;
            Config = config;
            AppHttpContext = context;
        }
        public ServiceBase(IRepository repository, ILogger logger, IConfiguration config, IMapper mapper, IHttpContextAccessor context)
        {
            Repository = repository;
            Logger = logger;
            Config = config;
            Mapper = mapper;
            AppHttpContext = context;
        }

        public IRepository Repository { get; }
        public ILogger Logger { get; }
        public IConfiguration Config { get; }
        public IMapper Mapper { get; }
        public IHttpContextAccessor AppHttpContext { get; }

        protected string GetLoginUserId()
        {
            if (AppHttpContext == null)
                return "Not-Login";

            return AppHttpContext.HttpContext.User.Identity.Name ?? "Not-Login";
        }
    }
}
