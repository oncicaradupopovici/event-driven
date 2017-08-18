using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Charisma.Contracts.ReadModel.Entities;
using Charisma.SharedKernel.Application.Interfaces;
using Charisma.Contracts.Application.Commands;
using Charisma.SharedKernel.Domain.Interfaces;

namespace Charisma.Contracts.Api.Controllers
{
    [Route("api/[controller]")]
    public class ContractsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICrudRepository<ContractReadModel> _contractReadModelRepository;

        public ContractsController(IMediator mediator, ICrudRepository<ContractReadModel> contractReadModelRepository)
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

        // POST api/contracts/7327223E-22EA-48DC-BC44-FFF6AB3B9489/lines
        [HttpPost("{id}/lines")]
        public async Task Post([FromBody]AddContractLine command)
        {
            await _mediator.Send(command);
        }

        // POST api/contracts/7327223E-22EA-48DC-BC44-FFF6AB3B9489/validate
        [HttpPost("{id}/validate")]
        public async Task Post([FromBody]ValidateContract command)
        {
            await _mediator.Send(command);
        }

    }
}
