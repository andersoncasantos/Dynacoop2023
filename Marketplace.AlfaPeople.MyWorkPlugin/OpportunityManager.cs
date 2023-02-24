using Marketplace.AlfaPeople.ConsoleApplication.Controller;
using Marketplace.AlfaPeople.ConsoleApplication.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.AlfaPeople.MyWorkPlugin
{
    public class OpportunityManager : IPlugin
    {
        public IOrganizationService Service { get; set; }
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            Service= serviceFactory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));



            Entity opportunity = new Entity();
            bool? incrementOrDecrement = null;
            SetVariables(context, out opportunity, out incrementOrDecrement);
            ExecuteOpportunityProcessy(context, opportunity, incrementOrDecrement);
        }

        private void ExecuteOpportunityProcessy(IPluginExecutionContext context, Entity opportunity, bool? incrementOrDecrement)
        {
            EntityReference accountReference = opportunity.Contains("parentaccountid") ? (EntityReference)opportunity["parentaccountid"] : null;


            if (accountReference != null)
            {

                Entity oppAccount = UpdateAccount(incrementOrDecrement, accountReference);

                if (context.MessageName == "Update")
                {
                    Entity opportunityPostImage = (Entity)context.PostEntityImages["PostImage"];
                    EntityReference postAccountReference = (EntityReference)opportunityPostImage["parentaccountid"];
                    UpdateAccount(true, postAccountReference);

                }

            }
        }

        private Entity UpdateAccount(bool? incrementOrDecrement, EntityReference accountReference)
        {
            ContaController contaController = new ContaController(this.Service);
            Entity oppAccount = contaController.GetAccountById(accountReference.Id, new string[] { "mtv_total_opp" });
            contaController.IncrementOrDecrementValueOfOpp(oppAccount, incrementOrDecrement);
            return oppAccount;
        }

        private void SetVariables(IPluginExecutionContext context, out Entity opportunity, out bool? incrementOrDecrement)
        {
            if (context.MessageName == "Create")
            {
                opportunity = (Entity)context.InputParameters["Target"];
                incrementOrDecrement = true;
            }

            else
            {
                opportunity = (Entity)context.PreEntityImages["PreImage"];
                incrementOrDecrement = false;
            }
        }
    }
}

