using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkflowConsoleApplicationForAccountService
{
    public sealed class PrintArray : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string[]> Array { get; set; }
        public OutArgument<string> resu { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            string[] text = context.GetValue(this.Array);
            string result="";
            for(int i=0;i<text.Length;i++)
            {
                result = text[i] +" , ";
            }
            context.SetValue(this.resu, result);
        }
    }
}
