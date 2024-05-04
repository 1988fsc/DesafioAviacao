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

            int valorAssento = 565930000;

            for (int i = 0; i < 10; i++) 
            {
                Entity embarque = new Entity("cre80_embarque");

                embarque["cre80_assento"] = new OptionSetValue(valorAssento);
                embarque["cre80_portao"] = new OptionSetValue(565930000);
                embarque["cre80_aviao"] = new EntityReference("cre80_aviao", aviao.Id);

                service.Create(embarque);
                valorAssento++;
            }

        }
    }
}