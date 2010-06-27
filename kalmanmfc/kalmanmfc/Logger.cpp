#include "stdafx.h"
#include "kalmanmfcdlg.h"
#include "logger.h"

Logger::Logger ( )
{
	dlg = NULL;
}
Logger::Logger ( CkalmanmfcDlg* logtarget )
{
	dlg = logtarget;

}
void Logger::SetLogTarget ( CkalmanmfcDlg* logtarget )
{
	dlg = logtarget;

}
void Logger::LogString ( const char* str )
{
	USES_CONVERSION;
	dlg->LogString (A2W(str));
}
void Logger::LogStringW ( const wchar_t* wstr )
{
	dlg->LogString ( wstr );

}


void Logger::ClearLog ( )
{
	dlg->ClearLog  ();

}

Logger& Logger::operator <<(std::string &rhs) 
{
	
	LogString ( rhs.c_str() ) ;
	return *this;
	
}
