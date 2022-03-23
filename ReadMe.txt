使用方式：
將MailList.xlsx用SQL Server匯入和匯出精靈匯入MS SQL，並命名為[SendMailData]，再執行程式。

其中Excel欄位名程為[mail]。

如需自訂Mail寄送畫面，請自行修改 SendMailByExcel.Mail.SendMail 中 resourceName 參數的html頁面（或自己新增一頁）。