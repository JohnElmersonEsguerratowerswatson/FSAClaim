# FSAClaim
FSA Calculation
Use case
Employee login 
Employee View FsaClaim
Employee CLAIM(Create) FsaClaim
Employee Update FsaClaim
Employee Delete FsaClaim
Employee Approve FsaClaim 	Role=Admin
Employee Deny FsaClaim 	Role=Admin

Rules:
i.e. general: [FSA Limit] = 5000 cannot be exceeded on the [Year Coverage] = 2022
1.	Claim cannot exceed [FSA Remaining] and [FSA Limit]
2.	[Receipt Date] cannot be outside [Year Coverage]
3.	User can update/delete pending claims
4.	Show all user transactions in the claims table



UI Requirements:
Login Page
	Username
	Password
	Login Button
Claims View
Alert user Rules(2) that He/She can only claim the [FSA Remaining]
Alert user Rules(2) that He/She has reached maximum claims.
Include Fields in Claims Table View: Date Submitted | Status | Receipt Date | Receipt Number | Receipt Amount | Claim Amount | Total Claim Amount | Reference Number
