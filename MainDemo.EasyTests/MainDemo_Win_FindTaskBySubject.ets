#DropDB MainDemo
#Application MainDemoWin
#Application MainDemoBlazor
#Application MainDemoBlazor_iPadEmulation
#Application MainDemoBlazor_iPhoneX

*FillForm
 User Name = Sam
*Action Log In

*Action Navigation(Tasks)
*Action New
*FillForm
 Subject = New task  
*Action Save and Close

*CheckTable
 Columns = Subject
 Row = Fix breakfast
 Row = New task
 Row = Review reports

*Action Find Task by Subject(Fix)
*CheckFieldValues
 Subject = Fix breakfast

