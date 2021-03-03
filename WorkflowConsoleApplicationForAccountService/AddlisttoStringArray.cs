using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkflowConsoleApplicationForAccountService
{
    public sealed class AddlisttoStringArray : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> Text { get; set; }
        public OutArgument<string[]> result { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            //List<string> textArray;
            string text = context.GetValue(this.Text);
            string[] result2 = text.Split(',');
            context.SetValue(this.result, result2);
        }
    }
}
