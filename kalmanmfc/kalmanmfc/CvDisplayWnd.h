#pragma once

struct CvDisplayWnd 
{


	char* wndName;
	CvDisplayWnd ( const char* name, int flags = CV_WINDOW_AUTOSIZE )
	{
		wndName = _strdup(name);
		cvNamedWindow ( name , flags );

	}

	void ShowImage ( MyCvImage img ) 
	{
		cvShowImage ( wndName , img );

	}
	void ShowImage ( CvArr* img )
	{
		cvShowImage ( wndName, img );
	}

	~CvDisplayWnd ( )
	{
		free ( (void*)wndName );
		cvDestroyWindow ( wndName );

	}


};
