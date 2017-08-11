using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Charisma.Invoices.Application.Commands;
using Charisma.Invoices.Domain.InvoiceAggregate;
using Charisma.SharedKernel.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Charisma.SharedKernel.Domain.Interfaces;

namespace Charisma.Invoices.Api.Controllers
{
    [Route("api/[controller]")]
    public class InvoicesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICrudRepository<Invoice> _invoiceRepository;

        public InvoicesController(IMediator mediator, ICrudRepository<Invoice> invoiceRepository)
        {
            _mediator = mediator;
            _invoiceRepository = invoiceRepository;
        }


        // GET api/invoices
        [HttpGet]
        public Task<IEnumerable<Invoice>> Get()
        {
            return _invoiceRepository.GetAllAsync();
        }

        // GET api/invoices/7327223E-22EA-48DC-BC44-FFF6AB3B9489
        [HttpGet("{id}")]
        public Task<Invoice> Get(Guid id)
        {
            return _invoiceRepository.GetSingleAsync(id);
        }

        // POST api/invoices
        [HttpPost]
        public Task Post([FromBody]CreateInvoice command)
        {
            return _mediator.Send(command);
        }

        // PATCH api/invoices/7327223E-22EA-48DC-BC44-FFF6AB3B9489
        [HttpPatch("{id}")]
        public Task Patch(Guid id, [FromBody]UpdateInvoiceAmount command)
        {
            return _mediator.Send(command);
        }

        // DELETE api/invoices/7327223E-22EA-48DC-BC44-FFF6AB3B9489
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
