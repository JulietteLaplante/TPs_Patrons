Pour generer la solution, il faut tout d'abord générer dans l'ordre :
	1 le projet RPCSDK
	2 UserSDK, StockSDK, BillSDK
	3 le reste

Si les reférence de dll ne fonctionne pas directement voici les dependances:

	BillManager: BillSdk.dll, StockSDK.dll, UserSDK.dll
	BillSDK: RPCSDK.dll, StockSDK.dll, UserSDK
	E-Commerce: BillSdk.dll, StockSDK.dll, UserSDK.dll
	RPCSDK: none
	StockManger: StockSDK.dll
	StockSDK: RPCSDK.dll
	UserManager: UserSDK.dll
	UserSDK: RPCSDK.dll


Fait par Yoann Haffner, Laplante Juliette, Castano Nicolas