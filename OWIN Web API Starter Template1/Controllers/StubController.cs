using System;
using System.Web.Http;
using Latsos.Core;
using Latsos.Shared;

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
        [Route("Stub/Peek")]
        public IHttpActionResult Peek()
        {
            return Ok(_repository.GetAll());
        }

        [HttpPost]
        [Route("Stub/Register")]
        public IHttpActionResult Register(BehaviorRegistrationRequest request)
        {
            _repository.Add(request);
            return Ok();
        }
        [HttpPost]
        [Route("Stub/ClearAll")]
        public IHttpActionResult ClearAll()
        {
            _repository.RemoveAll();
            return Ok();
        }
    }
}