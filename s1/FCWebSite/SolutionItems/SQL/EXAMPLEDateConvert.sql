use FCWeb;

SELECT
	g.Id, 
	g.GameDate AS 'Local',
	DATEADD(second, DATEDIFF(second, GETDATE(), GETUTCDATE()), g.GameDate) AS 'UTC'
FROM [Game] g
WHERE g.roundId = 141;

--SELECT DATEADD(second, DATEDIFF(second, GETDATE(), GETUTCDATE()), YOUR_DATE);