using System.Web.Http;
using EasyStub.Common;
using EasyStub.Core;
using Microsoft.Owin.Logging;

namespace EasyStub.Server.Controllers
{
    
    public class RegistrationsController : ApiController
    {
        private readonly IBehaviorRepository _repository;
        private readonly ILogger _logger;

        public RegistrationsController(IBehaviorRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [Route("Registrations")]
        public IHttpActionResult List()
        {
            return Ok(_repository.GetAll());
        }
        
        [HttpPost]
        [Route("Registrations")]
        public IHttpActionResult Add(StubRegistration request)
        {
            _logger.WriteInformation(request.ToString());
            if (_repository.Register(request))
            {
                return Ok();
            }
            {
                return Conflict();
            }
        }
        [HttpDelete]
        [Route("Registrations/{id}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            _repository.Unregister(id);
            return Ok( );
        }
        [HttpDelete]
        [Route("Registrations")]
        public IHttpActionResult Clear()
        {
            _repository.RemoveAll();
            return Ok();
        }
    }
}