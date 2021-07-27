;#DropDB TicariSetEasyTest

#Application TicariSetWin
#Application TicariSetWeb

*FillForm
 User Name = Admin
*Action Log In

#IfDef TicariSetWin

*Action About...
 
*Action OK

#EndIf

#IfDef TicariSetWeb
*Action Navigation(My Details)
#EndIf
