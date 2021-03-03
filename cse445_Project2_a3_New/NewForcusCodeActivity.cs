using Newtonsoft.Json.Linq;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace cse445_Project2_a3_New
{
    public sealed class NewForcusCodeActivity : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string[]> Text { get; set; }
        public OutArgument<string[]> resultNew { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
           
            string[] text = context.GetValue(this.Text);
            string[] result = NewsForcusService(text);
            context.SetValue(this.resultNew, result);
        }

        public string[] NewsForcusService(string[] text)
        {
            
            string apiKey2 = "FAB9BC417BFC4C1085C8101B6BAD9EF8";
            // scan data from the web
            using (var webClient = new WebClient())
            {
                // get a string prevesentation of Json

                string[] result2 = null;
                List<JToken> resultList2 = new List<JToken>(); // create list of Jtoken 
                List<string> listURL2 = new List<string>();// create list of string
                //scant the topic need to search
                for (int j = 0; j < text.Length; j++)
                {
                    // get Json from the API
                    string raw = webClient.DownloadString("https://api.scaleserp.com/search?api_key=" + apiKey2 + "&q=" + text[j] + "&hl=en&scholar_year_min=&scholar_year_max=&search_type=scholar&output=json");
                    JObject o2 = JObject.Parse(raw);// parse Json
                    IEnumerable<JToken> linkurl2 = o2.SelectTokens("$.scholar_results[*].link"); // get data from the root Json to list of article URL

                    //scan the Json and convert it to list of Json token
                    foreach (JToken item in linkurl2)
                    {
                        //Console.WriteLine(item);
                        resultList2.Add(item);
                        //listURL= (List<string>)resultList[0].s
                    }
                    // scan list of Json token and convert to list of string 
                    for (int i = 0; i < resultList2.Count(); i++)
                    {
                        //Console.WriteLine(resultList[i]);
                        string value = resultList2[i].Value<string>().ToString();
                        listURL2.Add(value);
                        //listURL = (string[])resultList[i].Value<string>().ToList();
                        //result = resultList["scholar_results"][i].;

                    }

                    result2 = listURL2.ToArray(); // convert list t array result
                }
                return result2;
            }
        }
    }
}
