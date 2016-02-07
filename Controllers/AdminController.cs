using CMS.Membership;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Xml; 

namespace CustomApp.Controllers
{
    public class AdminController : ApiController
    {
        [HttpGet]
        public string getAdmin()
        {
            return "Admin service is running";
        }
        [HttpPost]
        public HttpResponseMessage Admin(InputFormModel userinput)
        {
             

            // Creates the REST request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(userinput.RESTEndpoint);//"http://localhost:8090/rest/cms.eventlog/?format=json");

            // Sets the HTTP method of the request
            request.Method = "GET";
            string usernamepassword = userinput.UserName + ":" + userinput.Password;
            string base64usercredential = Convert.ToBase64String(Encoding.GetEncoding("utf-8").GetBytes(usernamepassword));
            // Authorizes the request using Basic authentication
            request.Headers.Add("Authorization: Basic " + base64usercredential);

            // Submits data for POST or PUT requests
           /* if (request.Method == "POST" || request.Method == "PUT")
            {
                request.ContentType = "text/xml";

                Byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(requestData);
                request.ContentLength = bytes.Length;

                using (Stream writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }*/

            // Gets the REST response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            try
            {
                // Stores the description of the response status
               
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                      
                        return Request.CreateErrorResponse(HttpStatusCode.OK, reader.ReadToEnd());
                    }
                    
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, response.StatusDescription);
                }
            }
            catch(Exception ee)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ee);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new Exception("Something went wrong"));
        }
    }
    public class InputFormModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RESTEndpoint { get; set; }
    }
}
