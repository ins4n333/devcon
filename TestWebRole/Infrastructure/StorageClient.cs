using System.Collections.Generic;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using TestWebRole.Common;

namespace TestWebRole.Infrastructure
{
	public class StorageClient
	{
		public StorageClient()
		{
			var stoageAccount = CloudStorageAccount.Parse(SettingsReader.GetFromCloud("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString"));
			var cloudTableClient = stoageAccount.CreateCloudTableClient();
			cloudTable = cloudTableClient.GetTableReference(tableName);
			cloudTable.CreateIfNotExists();
		}

		public void Insert(string partitionKey, string rowKey)
		{
			var insertOperation = TableOperation.Insert(new TableEntity(partitionKey, rowKey));
			cloudTable.Execute(insertOperation);
		}

		public IEnumerable<DynamicTableEntity> Get(string partitionKey)
		{
			var query = new TableQuery().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));

			return cloudTable.ExecuteQuery(query);
		}

		private CloudTable cloudTable;

		private string tableName = "TestTable";
	}
}