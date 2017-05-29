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
),

AggregatedMetrics AS (
    SELECT
        M.SystemId,
        M.ServiceId,
        M.DeploymentId,
        'cloudservice' AS InstanceId,
        System.Timestamp as EventEnqueuedUtcTime,
        CASE
            WHEN S.AggregatingFunction = 'SUM' THEN CONCAT(M.Name, '.sum')
            WHEN S.AggregatingFunction = 'AVG' THEN CONCAT(M.Name, '.avg')
            ELSE M.Name
        END AS Name,
        CASE
            WHEN S.AggregatingFunction = 'SUM' THEN SUM(M.Value)
            WHEN S.AggregatingFunction = 'AVG' THEN AVG(M.Value)
            ELSE COUNT(*)
        END AS Value,
        1 AS MetricType
    FROM ResultMetrics M 
    JOIN Settings S 
        ON M.Name = S.MetricName
    AND M.SystemId = S.SystemId
    AND M.ServiceId = S.ServiceId
    GROUP BY
        M.SystemId,
        M.ServiceId,
        M.DeploymentId,
        M.Name,
        S,
        TumblingWindow(Duration(minute, 1))
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
 INTO PerfCounters
  FROM AggregatedMetrics
WHERE SystemId = 'kpc.sde' 
   OR SystemId = 'kpcbeta.sde'
   OR SystemId = 'kpc.safekids' 
   OR SystemId = 'kpcbeta.safekids'

