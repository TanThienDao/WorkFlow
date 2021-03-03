using Newtonsoft.Json;
using System;
using System.Activities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace cse445_Project2_a3_New
{
    public sealed class CodeActivityAccount : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> inUsername { get; set; }
        public InArgument<string> inPassword { get; set; }
        public OutArgument<Boolean> outArgument { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            //string text = context.GetValue(this.Text);
            string username = context.GetValue<string>(this.inUsername);
            string password = context.GetValue<string>(this.inPassword);
            bool check = AccontService(username, password);
            context.SetValue(this.outArgument, check);
        }

        public class AccountUser
        {
            public string username;
            public string password;
        }
        public class RootAccount_inJson
        {
            public AccountUser[] UsersArray { get; set; }
        }
        public bool AccontService(string username, string password)
        {
            bool createdAccount = false;// bool check if acount created
            bool exiteAccount = false; // bool check if account already created
            AccountUser newUser = new AccountUser(); // object for user create
            RootAccount_inJson RootJson = new RootAccount_inJson();
            List<AccountUser> ListAccount = new List<AccountUser>();
            string Json;


            String pathJson = HttpRuntime.AppDomainAppPath + "\\UserAccountArray.json"; // FInd the path for Json file that store User info
            //String pathJson = HttpContext.Current.Request.ApplicationPath;// ( @"\\UserStoreDataJson.json");
            try
            {
                string JsonData = File.ReadAllText(pathJson); // Read all data in Json file int to string
                RootJson = JsonConvert.DeserializeObject<RootAccount_inJson>(JsonData);// convert string data to Json root
                // check if there are exit account in the Root 
                if (RootJson.UsersArray != null)
                {
                    ListAccount = RootJson.UsersArray.ToList<AccountUser>();// teanser to a list of string for easy to convert and scan for check
                    foreach (AccountUser item in ListAccount)// scan the list account 
                    {
                        if (item.username == username) // find the username, mean account exit in the data.
                        {
                            exiteAccount = true;
                        }
                    }
                }
                // condition when account does not in the data then create new account.
                if (!exiteAccount)
                {

                    newUser.username = username;
                    newUser.password = password;
                    //newUser.password = encrypted_password;
                    ListAccount.Add(newUser); // add new user in the list 
                    RootJson.UsersArray = ListAccount.ToArray<AccountUser>(); // convert list to root object
                    Json = JsonConvert.SerializeObject(RootJson, Formatting.Indented); // write back to Json format 
                    File.WriteAllText(pathJson, Json); // write back to Json file store in system 
                    createdAccount = true;
                }


            }
            catch (Exception ec) { }
            return createdAccount;
        }
    }
}
