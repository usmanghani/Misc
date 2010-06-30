Procedure Aboutapp
clear
set readborder on
ON ERROR DO ERRORHANDLER IN UTILITY
ON KEY LABEL ESCAPE DO GETOUT IN UTILITY
set library to foxtools.fll
#DEFINE APP_NAME       [Alim Expert Package]
#DEFINE APP_VERSION    [2004]
#DEFINE APP_AUTHOR     [Engg. Muhammad Rehan Qadri]
#DEFINE APP_ADDRESS    [8/28 Arkay Square Ext.]
#DEFINE APP_CITYSTATE  [Shahrah-e-Liaquat Karachi]
#DEFINE APP_COPYDATE   [2004]
#DEFINE APP_TECH       [almadinasoftwares@yahoo.com]
#DEFINE APP_BITMAP     [edware.bmp]
Define Window About ;
    At 00.000, 00.000 ;
    Size 18.692, 47.667 ;
    Font [MS Sans Serif], 8 Style [B] ;
    Color RGB(000,000,000,192,192,192) ;
    System ;
    Title [About ] + App_Name + [ ] + App_Version
Move Window About Center
Activate Window About NoShow
*@ 00.692, 01.833 Say App_BitMap BitMap
@ 00.538, 5.000 Say [Product of Al Madina Software:]
@ 01.692, 5.167 To 01.692, 32.000 ;
    Pen 2 Color RGB(000,000,128,192,192,192)
@ 02.154, 5.000 Say App_Author
@ 03.154, 5.000 Say App_Address
@ 04.154, 5.000 Say App_CityState
@ 05.154, 5.000 Say App_Tech
@ 05.154, 37.000 Say Chr(169) + App_CopyDate
@ 07.231, 5.000 Say [Licensed to:]
@ 08.385, 5.167 To 08.385, 32.000 Pen 2
@ 08.846, 5.000 Say "All those who need it" ;
    Color RGB(128,000,000,192,192,192)
@ 11.231, 5.000 Say [Limitations:]
@ 12.385, 5.167 To 12.385, 32.000 Pen 2
@ 13.154, 5.000 Say "* Currently the software does not support"
@ 14.154, 5.000 Say "Zawil Arham Branch of Islamic Inheritance"
*@ 15.154, 5.000 Say "system date is correct."
@ 16.154, 5.000 Say "* Also the software does not support"
@ 17.154, 5.000 Say "last two conditions of Rud"
@ 01.769, 35.333 Get JNDunBut ;
    Function [* \?OK] ;
    Size 01.769, 10.000 ;
    Default 0
Read ;
    Cycle ;
    Modal ;
    Deactivate .T.
Release Window About

Procedure ref
clear
set readborder on
ON ERROR DO ERRORHANDLER IN UTILITY
ON KEY LABEL ESCAPE DO GETOUT IN UTILITY
set library to foxtools.fll
Define Window About ;
    At 00.000, 00.000 ;
    Size 10.692, 47.667 ;
    Font [MS Sans Serif], 8 Style [B] ;
    Color RGB(000,000,000,192,192,192) ;
    System ;
    Title "About References Used"
Move Window About Center
Activate Window About NoShow
*@ 00.692, 01.833 Say App_BitMap BitMap
@ 00.538, 3.000 Say [Bahar-e-Shariat (Part 20) By:]
@ 01.692, 3.167 To 01.692, 32.000 ;
    Pen 2 Color RGB(000,000,128,192,192,192)
@ 2.538, 3.000 Say [Hazrat Allama Maulana Amjad Ali A'azmi]
@ 8.769, 35.333 Get JNDunBut ;
    Function [* \?OK] ;
    Size 01.769, 10.000 ;
    Default 0
Read ;
    Cycle ;
    Modal ;
    Deactivate .T.
Release Window About

Procedure ack
clear
set readborder on
ON ERROR DO ERRORHANDLER IN UTILITY
ON KEY LABEL ESCAPE DO GETOUT IN UTILITY
set library to foxtools.fll
Define Window About ;
    At 00.000, 00.000 ;
    Size 18.692, 47.667 ;
    Font [MS Sans Serif], 8 Style [B] ;
    Color RGB(000,000,000,192,192,192) ;
    System ;
    Title "Acknowledgment"
Move Window About Center
Activate Window About NoShow
*@ 00.692, 01.833 Say App_BitMap BitMap
@ 00.538, 3.000 Say [Acknowledgements:]
@ 01.692, 3.167 To 01.692, 32.000 ;
    Pen 2 Color RGB(000,000,128,192,192,192)
@ 2.5, 3.000 Say "Hazrat Allama Maulana Zia-ul-Mustafa A'azmi"
@ 4.5, 3.000 Say "Hazrat Allama Maulana Ata-ul-Mustafa A'azmi"
@ 6.5, 3.000 Say "Hazrat Allama Maulana Abul Qasim Qadri"
@ 8.5, 3.000 Say "Hazrat Allama Muhammad Waseem Attari"
@ 10.5, 3.000 Say "Muhammad Usman Abdul Ghaffar"
@ 12.5, 3.000 Say "Muhammad Faisal Abu Talib"
@ 14.5, 3.000 Say "Muhammad Idrees Barkati"
@ 16.5, 3.000 Say "Haji Gul-e-Anwar and Family"




@ 15.769, 35.333 Get JNDunBut ;
    Function [* \?OK] ;
    Size 01.769, 10.000 ;
    Default 0
Read ;
    Cycle ;
    Modal ;
    Deactivate .T.
Release Window About
