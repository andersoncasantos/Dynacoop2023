﻿using Marketplace.AlfaPeople.ConsoleApplication.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.AlfaPeople.ConsoleApplication.Controller
{
    public class ContaController
    {
        public IOrganizationService ServiceClient { get; set; }

        public Conta Conta { get; set; }

        public ContaController(IOrganizationService crmServiceCliente)
        {
            ServiceClient = crmServiceCliente;
            this.Conta = new Conta(ServiceClient);
        }
        public ContaController(CrmServiceClient crmServiceCliente)
        {
            ServiceClient = crmServiceCliente;
            this.Conta = new Conta(ServiceClient);
        }

        public Guid Create()
        {
            Conta conta = new Conta(this.ServiceClient);
            return conta.Create();
        }

        public bool Update(Guid accountId, string telephone1)
        {
            return Conta.Update(accountId, telephone1);
        }

        public bool Delete(Guid accountId)
        {
            return Conta.Delete(accountId);
        }

        public Entity GetAccountById(Guid id)
        {
            return Conta.GetAccountById(id);
        }

        public Entity GetAccountById(Guid id, string[] columns)
        {
            return Conta.GetAccountById(id, columns);
        }

        public Entity GetAccountByName(string name)
        {
            return Conta.GetAccountByName(name);
        }

        public Entity GetAccountByContactName(string name, string[] columns)
        {
            return Conta.GetAccountByContactName(name, columns);
        }

        public Entity GetAccountByTelephone(string telephone)
        {
            return Conta.GetAccountByTelephone(telephone);
        }

        public EntityCollection GetAccountByLike(string like)
        {
            return Conta.GetAccountByLike(like);
        }

        public void IncrementOrDecrementValueOfOpp(Entity oppAccount, bool? incrementOrDecrement)
        {
            Conta.IncrementOrDecrementValueOfOpp(oppAccount, incrementOrDecrement);
        }

        public void UpsertMultipleRequest(EntityCollection entityCollection)
        {
            Conta.UpsertMultipleRequest(entityCollection);
        }
    }
}
