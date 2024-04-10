using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CriarEmbarqueAviao : IPlugin
{
    public void Execute(IServiceProvider serviceProvider)
    {
        ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

        IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

        IOrganizationServiceFactory serviceFactory =
            (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

        IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

        if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
        {
            Entity aviao = (Entity)context.InputParameters["Target"];

            int valorAssento = 645870000;

            for (int i = 0; i < 10; i++) 
            {
                Entity embarque = new Entity("academia_embarque");

                embarque["academia_assento"] = new OptionSetValue(valorAssento);
                embarque["academia_portao"] = new OptionSetValue(645870000);
                embarque["academia_aviao"] = new EntityReference("academia_aviao", aviao.Id);

                service.Create(embarque);
                valorAssento++;
            }

        }
    }
}