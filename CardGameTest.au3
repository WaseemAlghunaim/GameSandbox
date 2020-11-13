#Region Main

#RequireAdmin
#include <System.au3>
#include <Scan.au3>



Main()
Func Main()
	System_Out("Program inilized", True)
	MsgBox("", "Player1_Deck", "Place the mouse over player 1 card deck and press enter")
	$P1 = MouseGetPos()
	MsgBox("", "Player2_Deck", "Place the  mouse over player 2 card deck")
	$P2 = MouseGetPos()
	While True
		MouseClick("Left", $P1[0], $P1[1], 1, 5) ; click p1
		MouseClick("Left", $P2[0], $P2[1], 1, 5) ; click p2
	WEnd
EndFunc   ;==>Main




#EndRegion Main
