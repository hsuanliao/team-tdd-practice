需求: 查詢某時間區間, 並回傳此區間的總預算金額
特別說明:
	1. DB裡存放了每月的預算總金額
		欄位: YearMonth (text) => ex: "201901"
		      Amount (monty) => ex: 310			  
	2. 查詢的日期區間, 若起始時間 > 結束時間 則回傳 0	
	3. 每月的預算, 換算成每天的預算, 只允許被"整除"
	
設計:
	1. Class: BudgetService
	   Method: decimal Query(DateTime startDate, DateTime endDate);
	   
	2. Class: BudgetRepository
	   Method: IList<Budget> GetAll();