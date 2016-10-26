using System;
using System.Web.Http;
using Latsos.Core;
using Latsos.Shared;
using Latsos.Shared.Request;
using Latsos.Shared.Response;

namespace Latsos.Web.Controllers
{
    public class StubController : ApiController
    {
        private readonly IBehaviorRepository _repository;

        public StubController(IBehaviorRepository repository)
        {
            _repository = repository;
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