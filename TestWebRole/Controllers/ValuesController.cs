using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TestWebRole.Infrastructure;

namespace TestWebRole.Controllers
{
	public class ValuesController : ApiController
	{
		public ValuesController()
		{
			client = new StorageClient();
		}

		[SwaggerOperation("GetAll")]
		public IEnumerable<string> Get()
		{
			var guid = Guid.NewGuid();
			client.Insert(guid.ToString(), "CV");
			client.Get(guid.ToString());

			Thread.Sleep(200);

			return new string[] {"value1", "value2"};
		}

		// GET api/values/5
		[SwaggerOperation("GetById")]
		[SwaggerResponse(HttpStatusCode.OK)]
		[SwaggerResponse(HttpStatusCode.NotFound)]
		public string Get(int id)
		{
			var guid = Guid.NewGuid();

			client.Insert(guid.ToString(), "CV");
			client.Insert(guid.ToString(), "CV");

			return "value";
		}

		// POST api/values
		[SwaggerOperation("Create")]
		[SwaggerResponse(HttpStatusCode.Created)]
		public void Post([FromBody]string value)
		{
		}

		// PUT api/values/5
		[SwaggerOperation("Update")]
		[SwaggerResponse(HttpStatusCode.OK)]
		[SwaggerResponse(HttpStatusCode.NotFound)]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		[SwaggerOperation("Delete")]
		[SwaggerResponse(HttpStatusCode.OK)]
		[SwaggerResponse(HttpStatusCode.NotFound)]
		public void Delete(int id)
		{
		}

		private StorageClient client;
	}
}