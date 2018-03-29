using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldStatelessService.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System;
    using System.ServiceModel.Activation;
    using System.Web;
    using System.Net.Http;
    using System.Net;
    using System.IO;

    //[RoutePrefix("helloworld")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class DefaultController : ApiController
    {
        // GET api/values
        [Route("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [Route("values/{id}")]
        public string Get(int id)
        {
            string ret = Convert.ToString(id);
            return ret;
        }

        const string StoragePath = @"D:\WebApiTest";
        [HttpPost]
        [Route("api/uploadFile")]
        public async Task<HttpResponseMessage> PostAsync()
        {
            if (Request.Content.IsMimeMultipartContent())
            {
                var streamProvider = new MultipartFormDataStreamProvider(Path.Combine(StoragePath, "Upload"));
                await Request.Content.ReadAsMultipartAsync(streamProvider);
                foreach (MultipartFileData fileData in streamProvider.FileData)
                {
                    if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
                    {
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
                    }
                    string fileName = fileData.Headers.ContentDisposition.FileName;
                    if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                    {
                        fileName = fileName.Trim('"');
                    }
                    if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                    {
                        fileName = Path.GetFileName(fileName);
                    }
                    if (File.Exists(Path.Combine(StoragePath, fileName)))
                    {
                        File.Delete(Path.Combine(StoragePath, fileName));
                    }
                    File.Move(fileData.LocalFileName, Path.Combine(StoragePath, fileName));
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Uploaded Successfully!!");
        }


        // PUT api/values/5
        [Route("values/{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [Route("values/{id}")]
        public void Delete(int id)
        {
        }
    }
}
