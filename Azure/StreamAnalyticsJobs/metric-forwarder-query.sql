WITH TimerRates AS ( 
  SELECT
        metric.SystemId,
        metric.ServiceId,
        metric.DeploymentId,
        metric.InstanceId,
        metric.EventEnqueuedUtcTime,      
        CASE
            WHEN timerRate.PropertyName = 'Mean' THEN CONCAT(metric.Name, '.rate.avg')
            WHEN timerRate.PropertyName = 'FifteenMinute' THEN CONCAT(metric.Name, '.rate.15min')
            WHEN timerRate.PropertyName = 'FiveMinute' THEN CONCAT(metric.Name, '.rate.5min')
            ELSE CONCAT(metric.Name, '.rate.1min')
        END AS Name,
        timerRate.PropertyValue AS Value,
        1 AS MetricType
    FROM Metrics as metric
    CROSS APPLY GetRecordProperties(metric.Rate) AS timerRate
    WHERE metric.MetricType = 5
),

TimerHistogram AS ( 
  SELECT
        metric.SystemId,
        metric.ServiceId,
        metric.DeploymentId,
        metric.InstanceId,
        metric.EventEnqueuedUtcTime,
        CASE
            WHEN timerHistogram.PropertyName = 'Last' THEN CONCAT(metric.Name, '.hst.last')
            WHEN timerHistogram.PropertyName = 'Mean' THEN CONCAT(metric.Name, '.hst.avg')
            WHEN timerHistogram.PropertyName = 'Median' THEN CONCAT(metric.Name, '.hst.median')
            ELSE CONCAT(metric.Name, '.hst.stddev')
        END AS Name,
        timerHistogram.PropertyValue AS Value,
        1 AS MetricType
    FROM Metrics as metric
    CROSS APPLY GetRecordProperties(metric.Histogram) AS timerHistogram
    WHERE metric.MetricType = 5
),

ResultMetrics AS (
    SELECT * FROM TimerRates
    UNION
    SELECT * FROM TimerHistogram
    UNION
    SELECT
        SystemId,
        ServiceId,
        DeploymentId,
        InstanceId,
        EventEnqueuedUtcTime,
        Name,
        Value,
        MetricType
   FROM Metrics 
   WHERE MetricType = 1 OR MetricType = 2 OR MetricType = 3
) 

SELECT  CONCAT(SystemId, '|', ServiceId) AS PartitionKey,
        CONCAT(Name, '|', InstanceId) AS RowKey,
        SystemId,
        ServiceId,
        DeploymentId,
        InstanceId,
        EventEnqueuedUtcTime AS 'EventEnqueuedUtcTime',
        Name,
        Value
  FROM ResultMetrics
WHERE (MetricType = 1 OR MetricType = 2 OR MetricType = 3)
  AND SystemId = 'DevConDev' 
