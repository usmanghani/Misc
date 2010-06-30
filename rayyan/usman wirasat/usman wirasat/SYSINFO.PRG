Procedure SysInfo
set library to foxtools.fll
Private ;
    LNPage ;
    JNDunBut ;
    LCAppDate ;
    LCSys ;
    LCUsr ;
    LCGDI ;
    LCVideo ;
    JCTitle
Store [PROGRAM INFORMATION] To JCTitle
Store 2 To LNPage
Store 3 To JNDunBut
#DEFINE APP_DATE {09/01/2004}
#DEFINE APP_VERSION '2004'
#DEFINE APP_TECH 'almadinsoftwares@yahoo.com'

Define Window SysInfo ;
    At 00.000, 00.000 ;
    Size 11.688, 56.625 ;
    Title JCTitle ;
    System ;
    Font [MS Sans Serif], 8 Style [B] ;
    Color RGB(000,000,000,192,192,192)
Move Window SysInfo Center
Activate Window SysInfo NoShow
@ 01.188, 01.000 To 07.000, 55.750 ;
    Color RGB(255,255,255,192,192,192) Pen 2
@ 01.188, 01.000 To 01.188, 55.750 ;
    Color RGB(128,128,128,192,192,192) Pen 1
@ 01.250, 01.000 To 01.250, 55.625 ;
    Color RGB(128,128,128,192,192,192) Pen 1
@ 01.188, 01.000 To 07.000, 01.000 ;
    Color RGB(128,128,128,192,192,192) Pen 1
@ 01.188, 01.125 To 06.938, 01.125 ;
    Color RGB(128,128,128,192,192,192) Pen 1
@ 07.775, 23.000 Say [Page 1 of 4] ;
    Color RGB(000,000,128,192,192,192)
Store CMonth(App_Date) + [ ] + ;
    LTrim(Str(Day(App_Date))) + [, ] + ;
    LTrim(Str(Year(App_Date))) To LCAppDate
=InfoPage(1)
@ 09.375, 06.000 Get JNDunBut ;
    Function [*HN <<  \<Back;\?\<Done;\<Next   >>] ;
    Valid InfoPage(JNDunBut) ;
    Size 01.923, 12.000, 04.333
Show Window SysInfo
LNHandleNum = RegFn([GetFreeSystemResources], [I], [I])
Store PadL(CallFn(LNHandleNum, 0), 2, [0]) + [% free] To LCSys
Store PadL(CallFn(LNHandleNum, 2), 2, [0]) + [% free] To LCUsr
Store PadL(CallFn(LNHandleNum, 1), 2, [0]) + [% free] To LCGDI
Store VdeoMode() To LCVideo
Do Case
Case LCVideo = 16
    Store [16] To LCVideo
Case LCVideo = 256
    Store [256] To LCVideo
Case LCVideo = 65536
    Store [64K] To LCVideo
Case LCVideo = 16777216
    Store [16M] To LCVideo
Otherwise
    Store [???] To LCVideo
EndCase

Read ;
    Cycle ;
    Modal ;
    Object JNDunBut
Release Window SysInfo

***********************************************************************
Function InfoPage
***********************************************************************
Parameter PNPageNum
Do Case
Case PNPageNum = 1
  Store IIf(LNPage = 1, 4, LNPage - 1) To LNPage
Case PNPageNum = 3
  Store IIf(LNPage = 4, 1, LNPage + 1) To LNPage
Otherwise
  Clear Read
  Return
Endcase
@ 01.313, 01.250 Clear To 06.875, 55.500
@ 07.775, 23.000 Say [Page ] + LTrim(Str(LNPage)) + ;
  [ of 4] Color RGB(000,000,128,192,192,192)
Do Case
Case LNPage = 1
  Store [Program Information] To JCTitle
  @ 01.540, 25.000 - TxtWidth([Program Name:  ]) Say ;
    [Program Name:  ] + 'Alim Expert Package'

  @ 02.875, 25.000 - TxtWidth([Program Version:  ]) Say ;
    [Program Version:  ] + App_Version
  @ 04.188, 25.000 - TxtWidth([Revision Date:  ]) ;
    Say [Revision Date:  ] + LCAppDate
  @ 05.500, 25.000 - TxtWidth([Tech Support:  ]) Say ;
    [Tech Support:  ] + App_Tech
Case LNPage = 2
  Store [Operating System Information] To JCTitle
  @ 01.540, 28.000 - TxtWidth([Processor Type:  ]) Say ;
    [Processor Type:  ] + Substr(Sys(17), 3)
  @ 02.875, 28.000 - TxtWidth([Operating System:  ]) Say ;
    [Operating System:  ] + SubStr(OS(), 1, At([ ], OS())) + ;
    SubStr(OS(), At([ ], OS()) + 2)
  @ 04.188, 28.000 - TxtWidth([Windows Version:  ]) Say ;
    [Windows Version:  ] + IIf([95] $ OS(1), [Windows 95], OS(1))
  @ 05.500, 28.000 - TxtWidth([Display Mode:  ]) Say ;
    [Display Mode:  ] + LTrim(Str(SysMetric(1))) + [ x ] + ;
    LTrim(Str(SysMetric(2))) + [ x ] + LCVideo
Case LNPage = 3
  Store [Memory Information] To JCTitle
  @ 01.540, 28.000 - TxtWidth([Memory In Use:  ]) Say ;
    [Memory In Use:  ] + LTrim(Transform(Val(Sys(1001)), ;
    [99,999,999])) + [ bytes]
  @ 02.875, 28.000 - TxtWidth([System Resources:  ]) Say ;
    [System Resources:  ] + LCSys
  @ 04.188, 28.000 - TxtWidth([User Resources:  ]) Say ;
    [User Resources:  ] + LCUsr
  @ 05.500, 28.000 - TxtWidth([GDI Resources:  ]) Say ;
    [GDI Resources:  ] + LCGDI
Case LNPage = 4
  Store [Disk Information] To JCTitle
  @ 01.540, 28.000 - TxtWidth([Current Drive:  ]) Say ;
    [Current Drive:  ] + Sys(5)
  @ 02.875, 28.000 - TxtWidth([Total Disk Size:  ]) Say ;
    [Total Disk Size:  ] + LTrim(Transform(Val(Sys(2020)), ;
    [999,999,999,999])) + [ bytes]
  @ 04.188, 28.000 - TxtWidth([Used Disk Space:  ]) Say ;
    [Used Disk Space:  ] + LTrim(Transform(Val(Sys(2020)) - ;
    DiskSpace(), [999,999,999,999])) + [ bytes]
  @ 05.500, 28.000 - TxtWidth([Space Available:  ]) Say ;
    [Space Available:  ] + LTrim(Transform(DiskSpace(), ;
    [999,999,999,999])) + [ bytes]
EndCase
Modify Window SysInfo Title JCTitle

***********************************************************************
Function VdeoMode
***********************************************************************

Private ;
    JNHandleDC ;
    JNHandleNum ;
    JNColors ;
    JNBits ;
    SFJNPlanes ;
    JNReleaseDC
JNHandleDC = RegFn([GetDC], [I], [I])
JNHandleNum = CallFn(JNHandleDC, 0)
JNColors = RegFn([GetDeviceCaps], [II], [I])
JNBits = CallFn(JNColors, JNHandleNum, 12)
JNPlanes = CallFn(JNColors, JNHandleNum, 14)
JNReleaseDC = RegFn([ReleaseDC], [II], [I])
=CallFn(JNReleaseDC, 0, JNHandleNum)
Return 2 ^ (JNPlanes * JNBits)
