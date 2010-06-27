#pragma once

struct MyCvImage
{

	IplImage* image;
	MyCvImage ( )
	{
		image = NULL;
	}

	MyCvImage ( IplImage* img )
	{
		
		image = NULL;
		if ( img ) image = cvCloneImage ( img );
		

	}
	MyCvImage ( const MyCvImage& ci )
	{
		
		image = NULL;
		if ( ci.image ) 
			image = cvCloneImage ( ci.image );

	}
	MyCvImage ( const char* filename )
	{
		
		image = NULL;
		if ( filename )
			image = cvLoadImage ( filename );

	}
	~MyCvImage ( )
	{
		if ( image ) 
			cvReleaseImage ( &image );

	}
	void Release ( )
	{
		if ( image ) 
			cvReleaseImage ( &image );
	}

	operator IplImage* ( )
	{
		return image;
	}
	IplImage* operator -> ( )
	{
		return image;
	}
	MyCvImage& operator = ( const MyCvImage& ci )
	{
		if ( ci.image )
		{
			if ( image ) 
				cvReleaseImage ( &image );
			image = cvCloneImage ( ci.image );

		}

		return *this;
	}
	MyCvImage& operator = ( const IplImage* img )
	{
		if ( img ) 
		{
			if ( image ) 
				cvReleaseImage ( &image );
			image = cvCloneImage ( img ) ;
		}

		return *this;
	}

	IplImage* operator == ( int value )
	{
		return image;
	}

};
