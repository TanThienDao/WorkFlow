using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkflowConsoleApplicationForAccountService
{
    public sealed class CodeActivityIINWorkflowApp : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> defaultName { get; set; }
        public OutArgument<string> enterName { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            Console.WriteLine("Welcome to Acound and login Service.");
            Console.WriteLine("This service will store your user name and password in Json file, and you can used that acound to login.");
            Console.WriteLine("Please Enter your name: ");
            string yourname = Console.ReadLine();
            if(yourname=="")
            {
                string name = context.GetValue(this.defaultName);
                yourname = name;
            }
            string helloName = "Hello User: " + yourname;
            context.SetValue(this.enterName, helloName);
            
        }
    }
}
