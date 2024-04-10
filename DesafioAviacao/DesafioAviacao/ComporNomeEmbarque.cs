using DesafioAviacao;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ComporNomeEmbarque : IPlugin
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
            Entity embarque = (Entity)context.InputParameters["Target"];

            var assento = embarque.GetAttributeValue<OptionSetValue>("academia_assento").Value;
            var enumAssento = (AviaoEnum)assento;
            string assentoLabel = enumAssento.ToString();

            var portao = embarque.GetAttributeValue<OptionSetValue>("academia_portao").Value;
            var enumPortao = (PortaoEnum)portao;
            string portaoLabel = enumPortao.ToString();

            embarque["academia_name"] = $" Portão: {portaoLabel} | Assento: {assentoLabel}";


        }
    }
}