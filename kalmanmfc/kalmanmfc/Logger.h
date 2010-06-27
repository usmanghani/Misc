#include "kalmanmfcdlg.h"
class Logger
{

public:

	Logger ( );
	Logger ( CkalmanmfcDlg* logtarget );
	void LogString ( const char* str );
	void LogStringW ( const wchar_t* wstr );
	void SetLogTarget ( CkalmanmfcDlg* logtarget );
	void ClearLog ( );
	Logger& operator << ( string& rhs );

private:
	CkalmanmfcDlg* dlg;

};


extern Logger logger;
