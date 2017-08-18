using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Charisma.Payments.Application.Commands;
using Charisma.Payments.Domain.PayableAggregate;
using Microsoft.AspNetCore.Mvc;
using Charisma.SharedKernel.Application.Interfaces;
using Charisma.SharedKernel.Domain.Interfaces;

namespace Charisma.Payments.Api.Controllers
{
    [Route("api/[controller]")]
    public class PayablesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICrudRepository<Payable> _payableRepository;

        public PayablesController(IMediator mediator, ICrudRepository<Payable> payableRepository)
        {
            _mediator = mediator;
            _payableRepository = payableRepository;
        }

        // GET api/payables
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var payables =  await _payableRepository.GetAllAsync();
            return Ok(payables);
        }

        // GET api/payables/7327223E-22EA-48DC-BC44-FFF6AB3B9489
        [HttpGet("{id}")]
        public Task<Payable> Get(Guid id)
        {
            return _payableRepository.GetSingleAsync(id);
        }

        // POSt api/payables/6AF6F8C8-117C-45C0-BB88-F49C13B8DE8D/pay
        [HttpPost("{id}/pay")]
        public Task Pay(int id, [FromBody]PayPayable command)
        {
            return _mediator.Send(command);
        }
    }
}
