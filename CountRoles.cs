
using System;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace calcAge
{    public sealed partial class calcAge : CodeActivity
    {
        // Inputty / Outputty
        [RequiredArgument]
        [Input("Date of Birth")]
        public InArgument<DateTime> dob { get; set; }
        [Output("Age")]
        public OutArgument<int> Res { get; set; }
        protected override void Execute(CodeActivityContext executionContext)
        {
            //Build the connection
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory =
            executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            //Do some counting
            if (dob != null)
            {
                DateTime dobInput = this.dob.Get(executionContext);
                int age = 0;
                age = DateTime.Now.Year - dobInput.Year;
                if (DateTime.Now.DayOfYear < dobInput.DayOfYear)
                {
                    age = age - 1;
                }
                //Send it back through the warp
                Res.Set(executionContext, age);
            }
        }
    }
}

 

