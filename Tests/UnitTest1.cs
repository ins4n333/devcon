using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAndLoadTestProject1
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public async Task TestMethod1()
		{
			// arrange
			string url = "http://../api/values";

			// act
			var response = await GetAsync(url);

			// assert
			Assert.IsTrue(response == HttpStatusCode.OK);
		}

		private async Task<HttpStatusCode> GetAsync(string url)
		{
			using (HttpClient client = new HttpClient())
			using (HttpResponseMessage response = await client.GetAsync(url))
			using (HttpContent content = response.Content)
			{
				// ... Read the string.
				await content.ReadAsStringAsync();

				return response.StatusCode;
			}
		}
	}
}