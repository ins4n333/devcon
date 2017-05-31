SELECT  CONCAT(SystemId, '|', ServiceId) AS PartitionKey,
        CONCAT(Name, '|', InstanceId) AS RowKey,
        SystemId,
        ServiceId,
        DeploymentId,
        InstanceId,
        EventEnqueuedUtcTime AS 'EventEnqueuedUtcTime',
        Name,
        Value
        Into powerBiCpu
  FROM metricshub
WHERE Name = 'cpu'or Name= 'memory'

SELECT  CONCAT(SystemId, '|', ServiceId) AS PartitionKey,
        CONCAT(Name, '|', InstanceId) AS RowKey,
        SystemId,
        ServiceId,
        DeploymentId,
        InstanceId,
        EventEnqueuedUtcTime AS 'EventEnqueuedUtcTime',
        Name,
        Value
        Into powerBiIIS
  FROM metricshub
WHERE Name like '%asp%'
