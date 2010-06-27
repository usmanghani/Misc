// kalmanmfc.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// CkalmanmfcApp:
// See kalmanmfc.cpp for the implementation of this class
//

class CkalmanmfcApp : public CWinApp
{



public:
	CkalmanmfcApp();

// Overrides
public:
	virtual BOOL InitInstance();
	CString currdir;

// Implementation

	DECLARE_MESSAGE_MAP()
	
};

extern CkalmanmfcApp theApp;
