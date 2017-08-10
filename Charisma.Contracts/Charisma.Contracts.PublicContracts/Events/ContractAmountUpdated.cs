﻿using System;
using Charisma.SharedKernel.Core;

namespace Charisma.Contracts.PublishedLanguage.Events
{
    public class ContractAmountUpdated : Event
    {
        public decimal NewAmount { get; }

        public ContractAmountUpdated(Guid id, decimal newAmount)
        {
            Id = id;
            NewAmount = newAmount;
        }
    }
}
