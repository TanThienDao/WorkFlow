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
    public sealed class CodeActivityLogin : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> Username { get; set; }
        public InArgument<string> Password { get; set; }
        public OutArgument<bool[]> Result { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            //string text = context.GetValue(this.Text);
            string username = context.GetValue(this.Username);
            string password = context.GetValue(this.Password);
            bool[] result = LoginService(username, password);
            context.SetValue(this.Result, result);


        }
        public class AccountUser
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        // a class for Json file roon this is an array of Joson.
        public class RootAccount_inJson
        {
            public AccountUser[] UsersArray { get; set; } // array of account user for Json format, this Json root is a list.

        }
        //UserAccountArray.json
        public bool[] LoginService(string username, string password)
        {
            bool loginAccount = true;// bool check if login suceess 
            bool exiteAccount = false;// check if account is exit
            bool[] result = new bool[2];// return bool value as array size 2 
            AccountUser loginUser = new AccountUser(); // create object login user
            RootAccount_inJson RootJson = new RootAccount_inJson(); // create root array fot json
            List<AccountUser> ListAccount = new List<AccountUser>(); // create a list of account user for convert. 

            String pathJson = HttpRuntime.AppDomainAppPath + "\\UserAccountArray.json"; // find the path for Json data file.
            string Json;
            try
            {
                string JsonData = File.ReadAllText(pathJson); // read the Json path  file to string
                RootJson = JsonConvert.DeserializeObject<RootAccount_inJson>(JsonData); //convert it to a array object of account user
                //check if the data is exits account. 
                if (RootJson.UsersArray != null)
                {
                    ListAccount = RootJson.UsersArray.ToList<AccountUser>(); // conver object root to list of acount user
                    foreach (AccountUser item in ListAccount)// scant throught the list 
                    {
                        if (item.username == username) // find the username 
                        {
                            exiteAccount = true; // turn on the acount exit
                            //result[0] = true;
                            if (password == item.password) // check the the password it it correct by decrypt password.
                            {
                                loginAccount = true;
                                //result[1] = true;
                            }
                            else
                            {
                                loginAccount = false;
                                //result[1] = false;
                            }
                        }
                    }
                }



            }
            catch (Exception ec)
            {

            }
            result[0] = exiteAccount;
            result[1] = loginAccount;
            return result;
        }
    }
}
