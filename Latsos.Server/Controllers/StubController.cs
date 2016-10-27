using System;
using System.Web.Http;
using Latsos.Core;
using Latsos.Shared;
using Latsos.Shared.Request;
using Latsos.Shared.Response;
using Microsoft.Owin.Logging;

namespace Latsos.Web.Controllers
{
    public class StubController : ApiController
    {
        private readonly IBehaviorRepository _repository;
        private readonly ILogger _logger;

        public StubController(IBehaviorRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [Route("Stubs")]
        public IHttpActionResult List()
        {
            return Ok(_repository.GetAll());
        }
        
        [HttpPost]
        [Route("Stubs")]
        public IHttpActionResult Add(StubRegistration request)
        {
            _logger.WriteInformation(request.ToString());
            _repository.Register(request);
            return Ok( );
        }
        [HttpDelete]
        [Route("Stubs/{id}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            _repository.Unregister(id);
            return Ok( );
        }
        [HttpDelete]
        [Route("Stubs")]
        public IHttpActionResult Clear()
        {
            _repository.RemoveAll();
            return Ok();
        }
    }
}