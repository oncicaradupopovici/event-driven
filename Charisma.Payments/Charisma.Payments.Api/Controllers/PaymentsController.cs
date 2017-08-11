using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Charisma.Contracts.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using Charisma.SharedKernel.Application.Interfaces;

namespace Charisma.Payments.Api.Controllers
{
    [Route("api/[controller]")]
    public class PaymentsController : Controller
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POSt api/payments/5/pay
        [HttpPost("{id}")]
        public Task Pay(int id, [FromBody]PayPayment command)
        {
            return _mediator.Send(command);
        }
    }
}
