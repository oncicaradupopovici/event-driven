using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Charisma.SharedKernel.ReadModel.Interfaces;
using Charisma.Contracts.ReadModel.Entities;
using Charisma.SharedKernel.Application.Interfaces;
using Charisma.Contracts.Application.Commands;

namespace Charisma.Contracts.Api.Controllers
{
    [Route("api/[controller]")]
    public class ContractsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IReadModelRepository<ContractReadModel> _contractReadModelRepository;

        public ContractsController(IMediator mediator, IReadModelRepository<ContractReadModel> contractReadModelRepository)
        {
            _mediator = mediator;
            _contractReadModelRepository = contractReadModelRepository;
        }


        // GET api/contracts
        [HttpGet]
        public Task<IEnumerable<ContractReadModel>> Get()
        {
            return _contractReadModelRepository.GetAllAsync();
        }

        // GET api/contracts/7327223E-22EA-48DC-BC44-FFF6AB3B9489
        [HttpGet("{id}")]
        public Task<ContractReadModel> Get(Guid id)
        {
            return _contractReadModelRepository.GetSingleAsync(id);
        }

        // POST api/contracts
        [HttpPost]
        public async Task Post([FromBody]CreateContract command)
        {
            await _mediator.Send(command);
        }

        // PUT api/contracts/7327223E-22EA-48DC-BC44-FFF6AB3B9489
        [HttpPatch("{id}")]
        public async Task Patch(Guid id, [FromBody]UpdateContractAmount command)
        {
            await _mediator.Send(command);
        }

        // DELETE api/contracts/7327223E-22EA-48DC-BC44-FFF6AB3B9489
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
