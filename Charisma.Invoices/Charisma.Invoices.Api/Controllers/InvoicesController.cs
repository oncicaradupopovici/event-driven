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
        private readonly ICommandHandler<CreateInvoice> _createInvoiceHandler;
        private readonly ICommandHandler<UpdateInvoiceAmount> _updateInvoiceAmountHandler;
        private readonly ICrudRepository<Invoice> _invoiceRepository;

        public InvoicesController(ICommandHandler<CreateInvoice> createInvoiceHandler, ICommandHandler<UpdateInvoiceAmount> updateInvoiceAmountHandler, ICrudRepository<Invoice> invoiceRepository)
        {
            _createInvoiceHandler = createInvoiceHandler;
            _updateInvoiceAmountHandler = updateInvoiceAmountHandler;
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
        public async Task Post([FromBody]CreateInvoice command)
        {
            await _createInvoiceHandler.HandleAsync(command);
        }

        // PATCH api/invoices/7327223E-22EA-48DC-BC44-FFF6AB3B9489
        [HttpPatch("{id}")]
        public async Task Patch(Guid id, [FromBody]UpdateInvoiceAmount command)
        {
            await _updateInvoiceAmountHandler.HandleAsync(command);
        }

        // DELETE api/invoices/7327223E-22EA-48DC-BC44-FFF6AB3B9489
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
