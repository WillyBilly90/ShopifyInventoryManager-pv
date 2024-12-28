using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Resources;

namespace ShopifyInventorymanager.GraphQLClient
{
    internal class GraphQLNETClient
    {
        public object Execute(string path, string apikey, string query)
        {
            // For Athunetication create private app
            // Once you create private App Use the API Key and password within the private App

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
            request.Headers.Add("X-Shopify-Access-Token:" + apikey);
            request.ContentType = "application/graphql";
            request.Method = "POST";

            request.Headers.Add("Authorization", "Basic ");

            if (query != null)
            {

                if (!String.IsNullOrEmpty(query))
                {
                    using (var ms = new MemoryStream())
                    {
                        using (var writer = new StreamWriter(request.GetRequestStream()))
                        {
                            writer.Write(query);
                            writer.Close();
                        }
                    }
                }
            }

            string result = null;
            string errorMessage = null;
            try
            {
                var response = (HttpWebResponse)request.GetResponse();

                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    result = reader.ReadToEnd();
                    reader.Close();
                }



            }
            catch (WebException ex)
            {
                // Any non 200 status code server errors
                using (var stream = ex.Response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {

                        errorMessage = reader.ReadToEnd();
                    }
                }


            }
            catch (Exception ex)
            {
                // Some general error like server was never reached
                errorMessage = ex.Message;
            }

            if (string.IsNullOrWhiteSpace(result))
                return JsonConvert.DeserializeObject(errorMessage);

            return JsonConvert.DeserializeObject(result);


        }
    }
}
