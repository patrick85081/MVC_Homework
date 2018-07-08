CREATE VIEW 客戶信息
	AS SELECT [t0].[客戶名稱], (
    SELECT COUNT(*)
    FROM [客戶銀行資訊] AS [t1]
    WHERE [t1].[客戶Id] = [t0].[Id]
    ) AS [銀行數量], (
    SELECT COUNT(*)
    FROM [客戶聯絡人] AS [t2]
    WHERE [t2].[客戶Id] = [t0].[Id]
    ) AS [聯絡人數量]
FROM [客戶資料] AS [t0]
