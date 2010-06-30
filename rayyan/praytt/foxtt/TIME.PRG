CLEAR
CLOS ALL
SET EXACT OFF
SET DELETE ON
SET SAFETY OFF
SET CONFIRM ON
SET DECIMAL TO 8
SET DATE BRIT
SET CENT ON
SET FIXED ON
ON ERROR DO ERRORHANDLER IN UTILITY
ON KEY LABEL ESCAPE DO GETOUT IN UTILITY
IF ISCOLOR()
DEFINE WINDOW time AT 0,0 SIZE 36-1,125-1;
   SHADOW PANEL MINIMIZE FLOAT GROW;
   TITLE "** SELECT LOCATION **";
   FOOTER "DESIGNED BY: MUHAMMAD REHAN QADRI";
   FONT "MS SANS SERIF",8;
   COLOR RGB (,,255,192,192,192);
   FILL FILE SYS(5)+SYS(2003)+"\marb009.BMP"
ELSE
DEFINE WINDOW time AT 0,0 SIZE 36-1,125-1;
   SHADOW PANEL MINIMIZE FLOAT GROW;
   TITLE "** SELECT LOCATION **";
   FOOTER "DESIGNED BY: MUHAMMAD REHAN QADRI";
   FONT "MS SANS SERIF",8;
   COLOR RGB (,,255,192,192,192)
ENDIF
   ACTIVATE WINDOW time
MOVE WINDOW time CENTER
USE loctest IN 0 ORDER TAG city ALIAS loc
IF CHOICE=1
USE rayyan IN 0 ORDER TAG days ALIAS ray
ELSE
USE rayyan1 IN 0 ORDER TAG days ALIAS ray
ENDIF
USE xyz IN 0 ORDER TAG city ALIAS xyz
GOTO TOP
SELECT loc
SCATTER MEMVAR BLANK
@ 1.846,38 SAY "Location Selector";
   FONT "arial",24 STYLE "bt" COLOR W
@ 1.692,38.2 SAY "Location Selector";
   FONT "arial",24 STYLE "bt" COLOR B
@ 5,2 TO 30,123  PEN 2,20 STYLE "16";
   COLOR RGB(128,128,128,,,)
@ 8,10 SAY "PREVIOUS PRIMARY LOCATION " FONT "ARIAL",12 STYLE "BT" COLOR B
@ 11,3 SAY "PRESS F2 TO SELECT LOCATION NAME OR JUST TYPE THE NAME OF LOCATION" FONT "ARIAL",11;
   STYLE "BT" COLOR R+
@ 14,10 SAY "SELECT NEW LOCATION: " FONT "ARIAL",12 STYLE "BT" COLOR B
@ 21,3 SAY "IMPORTANT INSTRUCTION: " FONT "VERDANA",10 STYLE "BT" COLOR R+
@ 22.5,40 SAY "IF YOU KNOW THE LOCATION NAME ENTER IT MANUALLY" FONT "VERDANA",10 STYLE "BT" COLOR B+
@ 24,3 SAY "OR JUST ENTER STARTING CHARACTERS AND PRESS F2. LEAVING THE SPACE BLANK" FONT "VERDANA",10 STYLE "BT" COLOR B+
@ 25.5,3 SAY "AND PRESSING F2 WILL DISPLAY ALL CITIES. AFTER SELECTING DESIRED CITY" FONT "VERDANA",10 STYLE "BT" COLOR B+
@ 27,3 SAY "FROM LIST PRESS ESCAPE (ESC)" FONT "VERDANA",10 STYLE "BT" COLOR B+
@ 8,80 GET xyz.city WHEN .F.;
   PICTURE "@!K" FONT "ARIAL",12 STYLE "B";
   MESSAGE "ENTER SAVING TIME IN MINUTES FOR EACH PRAYER";
   COLOR  B+/,RGB(,,,255,255,255)
@ 14,80 GET m.city;
   VALID NOT EMPTY (m.city) AND SEEK(m.city) AND CLEAR();
   FONT "ARIAL",14 STYLE "B" PICTURE "@!K";
   ERROR "PRESS F2 TO SELECT LOCATION NAME";
   COLOR  B+/,RGB(,,,255,255,255);
   WHEN disp_pop()

IF CHOICE=2
@ 18,10 SAY "ENTER THE YEAR" FONT "ARIAL",12 STYLE "BT" COLOR B
@ 18,80 GET MYEAR DEFA YEAR(DATE()) FONT "MS SANS SERIF",14 STYLE "B" PICTURE "@!K";
  VALID NOT EMPTY (MYEAR);
  ERROR "YOU MUST ENTER THE YEAR";
  COLOR  B+/,RGB(,,,255,255,255)
ENDIF

@ 30.5,35 GET m.choic DEFAULT " " SIZE 2,15,4;
   FUNCTION "*HN \<GENERATE;\<EXIT";
   FONT "MS SANS SERIF",12 STYLE "BT";
   VALID chk_choice()
READ

*!*****************************************************************************
*!
*!       Function: DISP_POP
*!
*!      Called by: QIBLA.PRG                         
*!               : TIME.PRG                          
*!               : CHK_CHOICE()       (function in INFORM.PRG)
*!
*!          Calls: DISP_CITY          (procedure in QIBLA.PRG)
*!
*!*****************************************************************************
FUNCTION disp_pop
ON KEY LABEL F2 DO SELECTLOCATION
RETURN .T.

FUNCTION disp_pop1
IF .T.
   DEFINE POPUP city_list FROM 0,0 TO 20,60;
      PROMPT FIELD ALLTRIM(city)+" "+ALLTRIM(country);
      SCROLL MARGIN;
      MESSAGE "PRESS THE FIRST LETTER OF THE LOCATION TO JUMP TO THE LOCATION INDEX.";
      COLOR W+/B,RGB(0,128,128,192,192,192)
   ON KEY LABEL f2 ACTIVATE POPU city_list REST
   ON SELE POPUP city_list DO disp_city
   RETURN .T.
ENDIF

*!*****************************************************************************
*!
*!      Procedure: DISP_CITY
*!
*!      Called by: DISP_POP()         (function in QIBLA.PRG)
*!
*!*****************************************************************************
PROCEDURE disp_city
m.city=city
SHOW GET city
ON KEY
DEAC POPU city_list
RELE POPU city_list
RETURN

*!*****************************************************************************
*!
*!       Function: CLEAR
*!
*!*****************************************************************************
FUNCTION CLEAR
@ 11,3 TO 13,122 CLEAR

*!*****************************************************************************
*!
*!       Function: CHK_CHOICE
*!
*!      Called by: INFORM.PRG                        
*!               : TIME.PRG                          
*!
*!          Calls: DISP_POP()         (function in QIBLA.PRG)
*!               : BELL               (procedure in INFORM.PRG)
*!               : CALC               (procedure in QIBLA.PRG)
*!
*!*****************************************************************************
FUNCTION chk_choice
DO CASE
CASE m.choic="GENERATE"
   DO CALC
CASE m.choic="EXIT"
   CLEAR
   CLOS ALL
   DEACT WIND time
   RELE WIND time
   RETURN
ENDCASE

*!*****************************************************************************
*!
*!      Procedure: CALC
*!
*!      Called by: CHK_CHOICE()       (function in INFORM.PRG)
*!
*!          Calls: SUB()              (function in QIBLA.PRG)
*!               : DEG()              (function in QIBLA.PRG)
*!               : DMS()              (function in QIBLA.PRG)
*!               : CIRCLE             (procedure in QIBLA.PRG)
*!               : CALC1              (procedure in TIME.PRG)
*!
*!*****************************************************************************
PROCEDURE CALC
DEAC POPU city_list
RELE POPU city_list
ON KEY
DEAC WIND time
RELE WIND time

IF ISCOLOR()
DEFINE WINDOW time AT 0,0 SIZE 30-1,125-1;
   SHADOW PANEL MINIMIZE FLOAT GROW;
   TITLE "** PRAYER TIME TABLE GENERATOR FOR SELECTED LOCATION **";
   FOOTER "DESIGNED BY: MUHAMMAD REHAN QADRI";
   FONT "MS SANS SERIF",8;
   COLOR RGB(,,,192,192,192);
   FILL FILE SYS(5)+SYS(2003)+"\MARB009.BMP"
ELSE
DEFINE WINDOW time AT 0,0 SIZE 30-1,125-1;
   SHADOW PANEL MINIMIZE FLOAT GROW;
   TITLE "** PRAYER TIME TABLE GENERATOR FOR SELECTED LOCATION **";
   FOOTER "DESIGNED BY: MUHAMMAD REHAN QADRI";
   FONT "MS SANS SERIF",8;
   COLOR RGB(,,,192,192,192)
ENDIF
ACTIVATE WINDOW time
MOVE WINDOW time CENTER


@ 2,(WCOLS()-TXTWIDTH("THIS TIME TABLE GENERATOR MAY TAKE FEW MINUTES",'ARIAL',12,'BT')* ;
	FONTMETRIC(6,'ARIAL',12)/FONTMETRIC(6))/2-8  ;
	SAY "THIS TIME TABLE GENERATOR MAY TAKE FEW MINUTES" FONT 'ARIAL',12 STYLE "BT"
@ 4,(WCOLS()-TXTWIDTH("TO GENERATE PRAYER TIME TABLE FOR YOU SELECTED LOCATION",'ARIAL',12,'BT')* ;
	FONTMETRIC(6,'ARIAL',12)/FONTMETRIC(6))/2-8  ;
	SAY "TO GENERATE PRAYER TIME TABLE FOR YOU SELECTED LOCATION" FONT 'ARIAL',12 STYLE "BT"
@ 8,(WCOLS()-TXTWIDTH("CONFIRM YOUR DATA BEFORE CONTINUING OTHERWISE CHOSE EXIT",'ARIAL',12,'BT')* ;
	FONTMETRIC(6,'ARIAL',12)/FONTMETRIC(6))/2-8  ;
	SAY "CONFIRM YOUR DATA BEFORE CONTINUING OTHERWISE CHOSE EXIT" FONT 'ARIAL',12 STYLE "BT" COLOR R

@ 12,18 SAY "NAME OF LOCATION:" FONT "ARIAL",12 STYLE "BT"
@ 12,80 SAY ALLTRIM(m.city)+" "+ALLTRIM(loc.country) FONT "ARIAL",12 STYLE "BT" COLOR B
@ 14,18 say "LATITUDE:   "+latitude FONT "ARIAL",12 STYLE "BT"
@ 14,80 say "LONGITUDE:  "+longitude FONT "ARIAL",12 STYLE "BT"
@ 16,18 say "GMT            : "+GREEN FONT "ARIAL",12 STYLE "BT"

STORE 1 TO mchoice

@ 20,8 GET mchoice FUNCTION '*BH BESM.BMP;CANCEL.BMP';
   SIZE 7,40,30
READ

DO CASE
CASE mchoice=1
   CLEAR
   DO calc1
CASE mchoice=2
   CLEAR
   CLOS ALL
   DEAC WIND time
   RELE WIND time
   RETURN
ENDCASE

*!*****************************************************************************
*!
*!      Procedure: CALC1
*!
*!      Called by: CALC               (procedure in QIBLA.PRG)
*!
*!          Calls: SUB()              (function in QIBLA.PRG)
*!               : DMS()              (function in QIBLA.PRG)
*!               : DEG()              (function in QIBLA.PRG)
*!               : NISF()             (function in TIME.PRG)
*!               : NISFUL             (procedure in TIME.PRG)
*!
*!           Uses: CHECK.DBF          
*!               : SUMMARY.DBF        
*!
*!*****************************************************************************
PROCEDURE calc1
ZCONS=0
DECLARE sheet[365,19]		&&ARRAY TO HOLD GENERATED DATA TO MAP TO A TABLE
GOTO TOP IN ray
SELECT loc
SCATTER MEMVAR

startsec=SECONDS()

IF CHOICE=2
DO EOT_DCL2					&&AUTOGENERATE ALMANAC TABLE
CLEAR
ENDIF

CLEAR
SELECT LOC
FOR outer=1 TO 8			&&OUTER LOOP TO REPEAT CALCULATIONS FOR EIGHT TIMINGS
   stsec=SECONDS()
   FOR i=1 TO 365
      
      GOTO i IN ray
      IF NOT (outer=7 OR outer=8)
         *CALCULATION OF BUDE FOQANI
         
         DO CASE
            CASE CHOICE=1
            IF m.la_dir=ray.dec_dir
               foqani=ALLTRIM(STR(sub(m.latitude,ray.decline),6,E))
            ENDIF
         
            IF m.la_dir!=ray.dec_dir
               foqani=ALLTRIM(STR(ADD(m.latitude,ray.decline),6,E))
            ENDIF

            
            CASE CHOICE=2

            DO CASE
            CASE OUTER=1 OR OUTER=3 OR OUTER=5 OR OUTER=6
            IF m.la_dir=ray.dec_dir18
               foqani=ALLTRIM(STR(sub(m.latitude,ray.decline18),6,E))
            ELSE         
               foqani=ALLTRIM(STR(ADD(m.latitude,ray.decline18),6,E))
            ENDIF
            OTHERWISE
            IF m.la_dir=ray.dec_dir6
               foqani=ALLTRIM(STR(sub(m.latitude,ray.decline6),6,E))
            ELSE         
               foqani=ALLTRIM(STR(ADD(m.latitude,ray.decline6),6,E))
            ENDIF
         	ENDCASE
		 ENDCASE         
         
         DO CASE
         CASE outer=1 or outer=2
            kokab="90.50"
            
         CASE outer=3 or outer=4
            kokab="108.00"
            
         CASE outer=5
            kokab=ALLTRIM(STR(dms(VAL((ALLTRIM(STR(RTOD(ATAN(TAN(DTOR(deg(VAL(foqani))))+2)),6,2))))),6,2))
            
         CASE outer=6
            kokab=ALLTRIM(STR(dms(VAL((ALLTRIM(STR(RTOD(ATAN(TAN(DTOR(deg(VAL(foqani))))+1)),6,2))))),6,2))
            
         ENDCASE
         
         
         *CALCULATION OF MEEZAN
         meezan=ALLTRIM(STR(ADD(foqani,kokab),6,E))
         
         *CALCULATION OF NISFUL MAJMU
         nisful=nisf(meezan)
         
         *CALCULATION OF HASIL UT TAFRIQ
         tafriq=ALLTRIM(STR(sub(kokab,nisful),6,E))
         
         *CALCULATION OF MAJMUA ARBA'A
         secarz= LOG10(1/COS(DTOR(deg(VAL(m.latitude)))))

      IF CHOICE=1
         	secmale= LOG10(1/COS(DTOR(deg(VAL(ray.decline)))))
      ELSE
         DO CASE
         	CASE OUTER=1 OR OUTER=3 OR OUTER=5 OR OUTER=6
         		secmale= LOG10(1/COS(DTOR(deg(VAL(ray.decline18)))))
         	CASE OUTER=2 OR OUTER=4
         		secmale= LOG10(1/COS(DTOR(deg(VAL(ray.decline6)))))
         ENDCASE
      ENDIF
         sinnisf= LOG10(SIN(DTOR(deg(VAL(nisful)))))
         sintafriq= LOG10(SIN(DTOR(deg(VAL(tafriq)))))
         
         majmua=secarz+secmale+sinnisf+sintafriq
         
         *CALCULATION OF JBGROOB TIME

CONCHECK=1
IF majmua>0
CONCHECK=0
ZCONS=10
majmua=majmua*(-1)
ENDIF

         IF OUTER=1 OR OUTER=3 OR OUTER=5 OR OUTER=6
         IF I=133 AND OUTER=3
         SET DECIMAL TO 6
         ENDIF
         jbgrubm=((RTOD(ASIN(10^((((majmua)+20)/2)-10)))*8)/60)           && SIN
         jbgrub=dms((RTOD(ASIN(10^((((majmua)+20)/2)-10)))*8)/60)
         ELSE 
         jbgrubm=((RTOD(ACOS(10^((((majmua)+20)/2)-10)))*8)/60)
         jbgrub=dms((RTOD(ACOS(10^((((majmua)+20)/2)-10)))*8)/60)
         ENDIF
         jbhr=SUBSTR(ALLTRIM(STR(jbgrub,10,6)),1,AT(".",ALLTRIM(STR(jbgrub,10,6)))-1)
         jbmin=SUBSTR(ALLTRIM(STR(jbgrub,10,6)),AT(".",ALLTRIM(STR(jbgrub,10,6)))+1,2)
         jbsec=SUBSTR(ALLTRIM(STR(jbgrub,10,6)),AT(".",ALLTRIM(STR(jbgrub,10,6)))+3,2)
         
         jbtime=jbhr+":"+jbmin+":"+jbsec
         
         *CALCULATION OF BALADI GROOB TIME
         IF ray.sign2="1"
            bhr=VAL(jbhr)
            IF OUTER=1 OR OUTER=3 OR OUTER=5 OR OUTER=6
            bmin=VAL(jbmin)+VAL(SUBSTR(ray.sun_SET,1,(AT(".",ray.sun_SET))-1))
            bsec=VAL(jbsec)+VAL(SUBSTR(ray.sun_set,AT(".",ray.sun_set)+1,2))
            ELSE
            bmin=VAL(jbmin)+VAL(SUBSTR(ray.sun_RISE,1,(AT(".",ray.sun_RISE))-1))
            bsec=VAL(jbsec)+VAL(SUBSTR(ray.sun_rise,AT(".",ray.sun_rise)+1,2))
            ENDIF
         ENDIF
         
         IF ray.sign2="-1"
            bhr=VAL(jbhr)
            bhr=VAL(jbhr)
            IF OUTER=1 OR OUTER=3 OR OUTER=5 OR OUTER=6
            bmin=VAL(jbmin)-VAL(SUBSTR(ray.sun_SET,1,(AT(".",ray.sun_SET))-1))
            bsec=VAL(jbsec)-VAL(SUBSTR(ray.sun_set,AT(".",ray.sun_set)+1,2))
            ELSE
            bmin=VAL(jbmin)-VAL(SUBSTR(ray.sun_RISE,1,(AT(".",ray.sun_RISE))-1))
            bsec=VAL(jbsec)-VAL(SUBSTR(ray.sun_rise,AT(".",ray.sun_rise)+1,2))
            ENDIF
         ENDIF
         
         
		 DO CASE
         CASE bsec<0 AND bsec=>-60
            bsec=60+bsec
            bmin=bmin-1
                  
         CASE bsec<-60 AND bsec=>-120
            bsec=120+bsec
            bmin=bmin-2
         
         ENDCASE
         DO WHILE .NOT. BSEC<60
            bsec=bsec-60
            bmin=bmin+1
         ENDDO         
         
         DO CASE
         CASE bmin<0 AND bmin=>-60
            bmin=60+bmin
            bhr=bhr-1
                  
         CASE bmin<-60 AND bmin=>-120
            bmin=120+bmin
            bhr=bhr-2
         ENDCASE         
         
         DO WHILE .NOT. BMIN<60
            bmin=bmin-60
            bhr=bhr+1
         ENDDO
         
         btime=ALLTRIM(STR(bhr))+":"+ALLTRIM(STR(bmin))+":"+ALLTRIM(STR(bsec))
         bhr=ALLTRIM(STR(bhr))
         bmin=ALLTRIM(STR(bmin))
         bsec=ALLTRIM(STR(bsec))
         
         *CALCULATION OF MURAWWAJ GROOB TIME
         IF m.sign="+1"
            mhr=VAL(bhr)
            mmin=VAL(bmin)+VAL(SUBSTR(m.diff,1,(AT(":",m.diff))-1))
            msec=VAL(bsec)+VAL(SUBSTR(m.diff,AT(":",m.diff)+1,2))
         ENDIF
         
         IF m.sign="-1"
            mhr=VAL(bhr)
            mmin=VAL(bmin)-VAL(SUBSTR(m.diff,1,(AT(":",m.diff))-1))
            msec=VAL(bsec)-VAL(SUBSTR(m.diff,AT(":",m.diff)+1,2))
         ENDIF
         
         DO CASE
         
         CASE msec<0 AND msec=>-60
            msec=60+msec
            mmin=mmin-1
         
         
         CASE msec<-60 AND msec=>-120
            msec=120+msec
            mmin=mmin-2
         
         ENDCASE
         
         DO WHILE .NOT. MSEC<60
            msec=msec-60
            mmin=mmin+1
         ENDDO
         
         DO CASE
         
         CASE mmin<0 AND mmin=>-60
            mmin=60+mmin
            mhr=mhr-1
         
         
         CASE mmin<-60 AND mmin=>-120
            mmin=120+mmin
            mhr=mhr-2
         
         ENDCASE
         
         DO WHILE .NOT. MMIN<60
            mmin=mmin-60
            mhr=mhr+1
         ENDDO
         
         IF mmin<10
            mdmstime=VAL(ALLTRIM(STR(mhr))+"."+"0"+ALLTRIM(STR(mmin))+ALLTRIM(STR(msec)))
            mtime=ALLTRIM(STR(mhr))+":"+"0"+ALLTRIM(STR(mmin))+":"+ALLTRIM(STR(msec))
         ELSE
            mdmstime=VAL(ALLTRIM(STR(mhr))+"."+ALLTRIM(STR(mmin))+ALLTRIM(STR(msec)))
            mtime=ALLTRIM(STR(mhr))+":"+ALLTRIM(STR(mmin))+":"+ALLTRIM(STR(msec))
         ENDIF
         
      ENDIF
      
      IF CHOICE=1
	      sheet[I,1]=CTOD(ALLTRIM(STR(DAY(RAY.DATE)))+"/"+ALLTRIM(STR(MONTH(RAY.DATE)))+"/"+ALLTRIM(STR(YEAR(DATE()))))
      ELSE
	      SHEET[I,1]=RAY.DATE
      ENDIF

      sheet[I,2]=ray.days
      sheet[I,3]=m.city
      DO CASE
      CASE outer=1
         sheet[I,4]=mtime				&& GROOB
         sheet[I,5]=mdmstime
      CASE OUTER=2
         sheet[I,6]=MTIME				&& TULU
         sheet[I,7]=mdmstime
      CASE outer=3
         IF CONCHECK=1
         sheet[I,8]=mtime				&& ISHA
         sheet[I,9]=mdmstime
         ELSE
         sheet[I,8]="********"				&& ISHA
         sheet[I,9]="0.000000"
         ENDIF
      CASE outer=4   
         IF CONCHECK=1
         sheet[I,10]=mtime				&& SUBH-E-SADIQ
         sheet[I,11]=mdmstime
         ELSE
         sheet[I,10]="********"				&& SUBH-E-SADIQ
         sheet[I,11]="0.000000"
         ENDIF
         
      CASE outer=5
         sheet[I,12]=mtime				&& ASR HANFI
         sheet[I,13]=mdmstime
      CASE outer=6
         sheet[I,14]=mtime				&& ASR SHAFE'E
         sheet[I,15]=mdmstime
      CASE outer=7
         DO nisful
      CASE outer=8
         if ZCONS!=10
         DO nisful
         endif
      ENDCASE
      @ 0,20 SAY "NAME OF LOCATION:" FONT "VERDANA",10 STYLE "BT"
      @ 0,80 SAY ALLTRIM(m.city)+" "+ALLTRIM(loc.country) FONT "VERDANA",10 STYLE "BT"
      @ 1,20 say "LATITUDE:   "+m.latitude FONT "VERDANA",10 STYLE "BT"
      @ 1,80 say "LONGITUDE:  "+m.longitude FONT "VERDANA",10 STYLE "BT"
	  @ 3,(WCOLS()-TXTWIDTH("PRAYER TIME TABLE GENERATOR",'ARIAL',16,'BT')* ;
	  FONTMETRIC(6,'ARIAL',16)/FONTMETRIC(6))/2-4  ;
	  SAY "PRAYER TIME TABLE GENERATOR" FONT 'ARIAL',16 STYLE "BT" COLOR B
      @ 10,3 SAY "APPLICATION IS GENERATING PRAYER TIME TABLE FOR YOUR SELECTED LOCATION";
         FONT "VERDANA",10 STYLE "BT"
      IF i=1
         @ 5,0 TO 10,WCOLS([time]) CLEAR
      ENDIF
      
      @ 5,0 TO 6.5,INT(i/2.9) COLOR N/B* PATTERN 1 PEN 2
      @ 7.5,(WCOLS([time])/2)-2 SAY INT((i/365)*100) FONT "TIMES NEW ROMAN",14 STYLE "B" PICTURE "999" COLOR
      
      per=INT((i/365)*100)
      
*      IF per=>51
*         *IF I/2.9>=(WCOLS([time])/2)+2
*         @ 8.3,(WCOLS([time])/2)-2 SAY INT((i/365)*100) FONT "TIMES NEW ROMAN",14 STYLE "B" PICTURE "999";
*            COLOR B*/GR+
*      ENDIF
      
      @ 7.5,(WCOLS([time])/2)+5 SAY "%" FONT "TIMES NEW ROMAN",14 STYLE "BT" COLOR B*
      
      per=INT((i/365)*100)
      
      DO CASE
      CASE outer=1
         @ 12,5 SAY SUBSTR("\|/-", INT(i%5) ,1) FONT "MS SANS SERIF",12 STYLE "B" COLOR RGB(0,,240,192,192,192)
         @ 12,11 SAY "PROCESSING SUNSET TIMINGS";
            FONT "VERDANA",10 STYLE "BT";
            COLOR B
            IF outer=1 AND per=100
            @ 12,0 TO 13.5,WCOLS([time]) CLEAR
            @ 11.5,5 SAY SYS(5)+SYS(2003)+"\OK.BMP" BITMAP STYLE "T"
            @ 12,11 SAY "PROCESS COMPLETE";
               FONT "VERDANA",10 STYLE "BT";
               COLOR B
            
			ENDIF
      CASE outer=2
         @ 13.5,5 SAY SUBSTR("\|/-", INT(i%5) ,1) FONT "MS SANS SERIF",12 STYLE "B" COLOR RGB(0,,240,192,192,192)
         @ 13.5,11 SAY "PROCESSING SUNRISE TIMINGS";
            FONT "VERDANA",10 STYLE "BT";
            COLOR B
         
         	IF outer=2 AND per=100			
            @ 13.5,0 TO 15,WCOLS([time]) CLEAR
            @ 13,5 SAY SYS(5)+SYS(2003)+"\OK.BMP" BITMAP STYLE "T"
            @ 13.5,11 SAY "PROCESS COMPLETE";
               FONT "VERDANA",10 STYLE "BT";
               COLOR B
			ENDIF
         
      CASE outer=3
         @ 15,5 SAY SUBSTR("\|/-", INT(i%5) ,1) FONT "MS SANS SERIF",12 STYLE "B" COLOR RGB(0,,240,192,192,192)
         @ 15,11 SAY "PROCESSING ISHA TIMINGS";
            FONT "VERDANA",10 STYLE "BT";
            COLOR B
         
         	IF outer=3 AND per=100
            @ 15,0 TO 16.6,WCOLS([time]) CLEAR
            @ 14.5,5 SAY SYS(5)+SYS(2003)+"\OK.BMP" BITMAP STYLE "T"
            @ 15,11 SAY "PROCESS COMPLETE";
               FONT "VERDANA",10 STYLE "BT";
               COLOR B
			ENDIF
         
      CASE outer=4
         @ 16.5,5 SAY SUBSTR("\|/-", INT(i%5) ,1) FONT "MS SANS SERIF",12 STYLE "B" COLOR RGB(0,,240,192,192,192)
         @ 16.5,11 SAY "PROCESSING MORNING TIMINGS";
            FONT "VERDANA",10 STYLE "BT";
            COLOR B
         
         	IF outer=4 AND per=100
            @ 16.5,0 TO 18,WCOLS([time]) CLEAR
            @ 16,5 SAY SYS(5)+SYS(2003)+"\OK.BMP" BITMAP STYLE "T"
            @ 16.5,11 SAY "PROCESS COMPLETE";
               FONT "VERDANA",10 STYLE "BT";
               COLOR B
			ENDIF
         
      CASE outer=5
         @ 18,5 SAY SUBSTR("\|/-", INT(i%5) ,1) FONT "MS SANS SERIF",12 STYLE "B" COLOR RGB(0,,240,192,192,192)
         @ 18,11 SAY "PROCESSING ASR-E-HANFI TIMINGS";
            FONT "VERDANA",10 STYLE "BT";
            COLOR B
         
         IF outer=5 AND per=100
            @ 18,0 TO 19.5,WCOLS([time]) CLEAR
            @ 17.5,5 SAY SYS(5)+SYS(2003)+"\OK.BMP" BITMAP STYLE "T"
            @ 18,11 SAY "PROCESS COMPLETE";
               FONT "VERDANA",10 STYLE "BT";
               COLOR B
         ENDIF
         
      CASE outer=6
         @ 19.5,5 SAY SUBSTR("\|/-", INT(i%5) ,1) FONT "MS SANS SERIF",12 STYLE "B" COLOR RGB(0,,240,192,192,192)
         @ 19.5,11 SAY "PROCESSING ASR-E-SAHFE'E TIMINGS";
            FONT "VERDANA",10 STYLE "BT";
            COLOR B
         
         IF outer=6 AND per=100
            @ 19.5,0 TO 21.5,WCOLS([time]) CLEAR
            @ 19,5 SAY SYS(5)+SYS(2003)+"\OK.BMP" BITMAP STYLE "T"
            @ 19.5,11 SAY "PROCESS COMPLETE";
               FONT "VERDANA",10 STYLE "BT";
               COLOR B
         ENDIF
         
      CASE outer=7
         @ 21,5 SAY SUBSTR("\|/-", INT(i%5) ,1) FONT "MS SANS SERIF",12 STYLE "B" COLOR RGB(0,,240,192,192,192)
         @ 21,11 SAY "PROCESSING ZOHAR TIMINGS";
            FONT "VERDANA",10 STYLE "BT";
            COLOR B
         
         IF outer=7 AND per=100
            @ 21,0 TO 23.5,WCOLS([time]) CLEAR
            @ 20.5,5 SAY SYS(5)+SYS(2003)+"\OK.BMP" BITMAP STYLE "T"
            @ 21,11 SAY "PROCESS COMPLETE";
               FONT "VERDANA",10 STYLE "BT";
               COLOR B
         ENDIF
         
      CASE outer=8
         @ 22.5,5 SAY SUBSTR("\|/-", INT(i%5) ,1) FONT "MS SANS SERIF",12 STYLE "B" COLOR RGB(0,,240,192,192,192)
         @ 22.5,11 SAY "PROCESSING DAHWA-E-KUBRA TIMINGS";
            FONT "VERDANA",10 STYLE "BT";
            COLOR B
         
         IF outer=8 AND per=100
            @ 22.5,0 TO 25,WCOLS([time]) CLEAR
            @ 22,5 SAY SYS(5)+SYS(2003)+"\OK.BMP" BITMAP STYLE "T"
            @ 22.5,11 SAY "PROCESS COMPLETE";
               FONT "VERDANA",10 STYLE "BT";
               COLOR B
         ENDIF
         
      ENDCASE
   ENDFOR
   
   ensec=SECONDS()
   totsec=ensec-stsec
   @ WROWS([time])-1,WCOLS([time])/2-30 TO WROWS([time]),WCOLS([time]) CLEAR
   @ WROWS([time])-1,WCOLS([time])/2-20 SAY "PROCESS COMPLETED IN "+ALLTRIM(STR(totsec,6,2))+" SECONDS";
      FONT "VERDANA",8 STYLE "BT" COLOR B
   
ENDFOR
SELECT xyz
SET SAFETY OFF
ZAP
APPEND FROM ARRAY sheet
REPLACE groob WITH "0"+groob FOR VAL(LEFT(groob,AT(":",groob)))<10
REPLACE tulu WITH "0"+tulu FOR VAL(LEFT(tulu,AT(":",tulu)))<10
REPLACE isha WITH "0"+isha FOR VAL(LEFT(isha,AT(":",isha)))<10
REPLACE subh_sadiq WITH "0"+subh_sadiq FOR VAL(LEFT(subh_sadiq,AT(":",subh_sadiq)))<10
REPLACE asr_hanfi WITH "0"+asr_hanfi FOR VAL(LEFT(asr_hanfi,AT(":",asr_hanfi)))<10
REPLACE asr_shafi WITH "0"+asr_shafi FOR VAL(LEFT(asr_shafi,AT(":",asr_shafi)))<10
REPLACE n_nahar WITH "0"+n_nahar FOR VAL(LEFT(n_nahar,AT(":",n_nahar)))<10
REPLACE zawal WITH "0"+zawal FOR VAL(LEFT(zawal,AT(":",zawal)))<10
REPLACE MONTH WITH UPPER(ALLTRIM(CMONTH(DATE))) ALL
REPLACE groob WITH SUBSTR(groob,1,AT(":",groob,2))+"0"+RIGHT(groob,2) FOR VAL(RIGHT(groob,2))<10
REPLACE tulu WITH SUBSTR(tulu,1,AT(":",tulu,2))+"0"+RIGHT(tulu,2) FOR VAL(RIGHT(tulu,2))<10
REPLACE isha WITH SUBSTR(isha,1,AT(":",isha,2))+"0"+RIGHT(isha,2) FOR VAL(RIGHT(isha,2))<10
REPLACE subh_sadiq WITH SUBSTR(subh_sadiq,1,AT(":",subh_sadiq,2))+"0"+RIGHT(subh_sadiq,2) FOR VAL(RIGHT(subh_sadiq,2))<10
REPLACE asr_hanfi WITH SUBSTR(asr_hanfi,1,AT(":",asr_hanfi,2))+"0"+RIGHT(asr_hanfi,2) FOR VAL(RIGHT(asr_hanfi,2))<10
REPLACE asr_shafi WITH SUBSTR(asr_shafi,1,AT(":",asr_shafi,2))+"0"+RIGHT(asr_shafi,2) FOR VAL(RIGHT(asr_shafi,2))<10

IF ZCONS=10
FOR ZF=1 TO 365
GOTO ZF IN LOC
set decimal to 4
REPLACE ZAWAL WITH ZAWAL(GROOB,SUBH_SADIQ) record zf
REPLACE DMSZAWAL WITH val((substr(zawal,1,at(":",zawal)-1))+"."+substr(zawal,at(":",zawal)+1,2)+substr(zawal,at(":",zawal,2)+1)) record zf
IF SUBH_SADIQ="0**"
REPLACE SUBH_SADIQ WITH N_NAHAR, DMSSUBH WITH DMSNAHAR RECORD ZF
ENDIF
ENDFOR
ENDIF

*TO DISPLAY TIME ONLY IN MINUTES
*replace subh_sadiq with substr(subh_sadiq,1,at(":",subh_sadiq,2)-1) all
*replace groob with substr(groob,1,at(":",groob,2)-1) all
*replace tulu with substr(tulu,1,at(":",tulu,2)-1) all
*replace isha with substr(isha,1,at(":",isha,2)-1) all
*replace ASR_HANFI with substr(ASR_HANFI,1,at(":",ASR_HANFI,2)-1) all
*replace ASR_SHAFI with substr(ASR_SHAFI,1,at(":",ASR_SHAFI,2)-1) all
*COPY TO check.dbf FOR days%5=0
*COPY TO summary.dbf FOR DAY(DATE)%5=0 OR (DAY(DATE)=28 AND MONTH(DATE)=2)
endsec=SECONDS()
totalsec=endsec-startsec
@ WROWS([time])-1,WCOLS([time])/2-30 TO WROWS([time]),WCOLS([time]) CLEAR
WAIT "PRAYER TIME TABLE FOR "+ALLTRIM(M.CITY)+ " GENERATED IN "+ALLTRIM(STR(totalsec/60,6,2))+" MINUTES" WIND TIMEOUT 3
CLEAR
CLEAR
DEAC WIND time
RELE WIND time

*!*****************************************************************************
*!
*!       Function: SUB
*!
*!      Called by: QIBLA.PRG                         
*!               : CALC               (procedure in QIBLA.PRG)
*!               : CALC1              (procedure in TIME.PRG)
*!
*!*****************************************************************************
FUNCTION sub
PARAMETER A , B

IF VAL(B)>VAL(A)
   dummy=B
   B=A
   A=dummy
ENDIF

C=INT(VAL(SUBSTR(A,AT(".",A)+1,2)))
D=INT(VAL(SUBSTR(B,AT(".",B)+1,2)))



e1=INT(VAL(LEFT(A,AT(".",A)-1)))
e2=INT(VAL(LEFT(B,AT(".",B)-1)))


IF (C<D)
   C=C+60
   e1=e1-1
ENDIF

F=(C-D)

PUBLIC E
IF F<10
   result=ALLTRIM(STR(ABS(e1-e2)))+"."+"0"+ALLTRIM(STR(F))
   E=2
ELSE
   result=ALLTRIM(STR(ABS(e1-e2)))+"."+ALLTRIM(STR(F))
   E=2
ENDIF


*RESULT=ALLTRIM(STR(ABS(E1-E2)))+"."+ALLTRIM(STR(F))
result=VAL(result)

RETURN result


*!*****************************************************************************
*!
*!       Function: ADD
*!
*!*****************************************************************************
FUNCTION ADD
PARA A,B
*C=VAL(RIGHT(A,2))
*D=VAL(RIGHT(B,2))


C=INT(VAL(SUBSTR(A,AT(".",A)+1,2)))
D=INT(VAL(SUBSTR(B,AT(".",B)+1,2)))


e1=INT(VAL(LEFT(A,AT(".",A)-1)))
e2=INT(VAL(LEFT(B,AT(".",B)-1)))


F=(C+D)

IF F=>60
   e1=e1+1
   F=F-60
ENDIF

PUBLIC E
IF F<10
   result=ALLTRIM(STR(ABS(e1+e2)))+"."+"0"+ALLTRIM(STR(F))
   E=2
ELSE
   result=ALLTRIM(STR(ABS(e1+e2)))+"."+ALLTRIM(STR(F))
   E=2
ENDIF


result=VAL(result)

RETURN result


*!*****************************************************************************
*!
*!       Function: DMS
*!
*!      Called by: CALC               (procedure in QIBLA.PRG)
*!               : CALC1              (procedure in TIME.PRG)
*!
*!*****************************************************************************
FUNCTION dms
PARA ans
ans=ALLTRIM(STR(ans,14,10))
POINT=AT(".",ans)
hr=VAL(SUBSTR(ans,1,POINT))
min1=VAL(SUBSTR(ans,POINT,2))
MIN=min1*60
sec1=VAL(SUBSTR(ans,POINT))-min1
sec=sec1*3600

IF sec>=60
   bacha=INT((INT(sec))/60)
   bacha1=INT(bacha)*60
   sec=sec-bacha1
   MIN=MIN+bacha
ENDIF

hr=ALLTRIM(STR(hr))
IF MIN<10
   MIN="0"+ALLTRIM(STR(MIN))
ELSE
   MIN=ALLTRIM(STR(MIN))
ENDIF

IF sec<10
   sec="0"+ALLTRIM(STR(sec))
ELSE
   sec=ALLTRIM(STR(sec))
ENDIF
dms=hr+"."+MIN+sec
dms=VAL(dms)
RETURN dms

*!*****************************************************************************
*!
*!       Function: DEG
*!
*!      Called by: QIBLA.PRG                         
*!               : CALC               (procedure in QIBLA.PRG)
*!               : CALC1              (procedure in TIME.PRG)
*!
*!*****************************************************************************
FUNCTION deg
PARA ans
ans=ALLTRIM(STR(ans,14,10))
POINT=AT(".",ans)
hr=VAL(SUBSTR(ans,1,POINT))
min1=VAL(SUBSTR(ans,POINT+1,2))
MIN=min1/60
sec1=VAL(SUBSTR(ans,POINT+3,2))
sec=sec1/3600
abc=MIN+sec
deg=hr+abc
RETURN deg

FUNCTION nisf
PARA abcd
LEFT=INT(VAL(LEFT(abcd,AT(".",abcd)-1)))
divide=INT(LEFT/2)
rem=INT(LEFT-(divide*2))
rem2=rem*60
C=((INT(VAL(SUBSTR(abcd,AT(".",abcd)+1,2)))+rem2)/2)

DO WHILE .NOT. C<60
   C=C-60
   divide=divide+1
ENDDO

IF C<9.5
   C="0"+ALLTRIM(STR(C))
   nisf1=ALLTRIM(STR(divide))+"."+C
ELSE
   nisf1=ALLTRIM(STR(divide))+"."+ALLTRIM(STR(C))
ENDIF
RETURN nisf1

*!*****************************************************************************
*!
*!      Procedure: NISFUL
*!
*!      Called by: CALC1              (procedure in TIME.PRG)
*!
*!*****************************************************************************
PROCEDURE nisful

mdmstulu=space(8)
DO CASE
CASE outer=7
   mdmstulu=sheet[I,6]
CASE outer=8
   mdmstulu=sheet[I,10]
ENDCASE

mdmstime=sheet[I,4]

if val(right(mdmstulu,2))<10
mdmstulu=(substr(MDMSTULU,1,at(":",MDMSTULU)-1))+"."+substr(MDMSTULU,at(":",MDMSTULU)+1,2)+"0"+substr(MDMSTULU,at(":",MDMSTULU,2)+1)
else
mdmstulu=(substr(MDMSTULU,1,at(":",MDMSTULU)-1))+"."+substr(MDMSTULU,at(":",MDMSTULU)+1,2)+substr(MDMSTULU,at(":",MDMSTULU,2)+1)
endif

if val(right(mdmstime,2))<10
mdmstime=(substr(mdmstime,1,at(":",mdmstime)-1))+"."+substr(mdmstime,at(":",mdmstime)+1,2)+"0"+substr(mdmstime,at(":",mdmstime,2)+1)
else
mdmstime=(substr(mdmstime,1,at(":",mdmstime)-1))+"."+substr(mdmstime,at(":",mdmstime)+1,2)+substr(mdmstime,at(":",mdmstime,2)+1)
endif

mdmstulu=val(mdmstulu)
mdmstime=val(mdmstime)


POINT=AT(".",ALLTRIM(STR(mdmstulu,8,4)))

tsec=SUBSTR(ALLTRIM(STR(mdmstulu,8,4)),POINT+3)
tmin=SUBSTR(ALLTRIM(STR(mdmstulu,8,4)),POINT+1,2)
thr=SUBSTR(ALLTRIM(STR(mdmstulu,8,4)),1,POINT-1)

POINT=AT(".",ALLTRIM(STR(mdmstime,8,4)))


gsec=SUBSTR(ALLTRIM(STR(mdmstime,8,4)),POINT+3)
gmin=SUBSTR(ALLTRIM(STR(mdmstime,8,4)),POINT+1,2)
ghr=SUBSTR(ALLTRIM(STR(mdmstime,8,4)),1,POINT-1)

jama=mdmstime+mdmstulu
jama=ALLTRIM(STR(jama,8,4))

POINT=AT(".",jama)

nsec=VAL(tsec)+VAL(gsec)
nmin=VAL(tmin)+VAL(gmin)
nhr=VAL(thr)+VAL(ghr)

DO WHILE .NOT. NSEC<60
   nsec=nsec-60
   nmin=nmin+1
ENDDO

DO WHILE .NOT. NMIN<60
   nmin=nmin-60
   nhr=nhr+1
ENDDO

summin=0
sumsec=0

IF NHR%2=1
hrd=nhr/2
SUMMIN=30
nhrd=ALLTRIM(STR(INT(HRD)))
ELSE
HRD=NHR/2
nhrd=ALLTRIM(STR(INT(HRD)))
ENDIF

IF NMIN%2=1
MIND=NMIN/2
SUMSEC=30
ELSE
MIND=NMIN/2
ENDIF

secd=nsec/2

IF SUMMIN=30
nmind=ALLTRIM(STR(INT(MIND+30)))
else
nmind=ALLTRIM(STR(INT(MIND)))
ENDIF

IF SUMSEC=30
nsecd=ALLTRIM(STR(INT(secD+30)))
ELSE
nsecd=ALLTRIM(STR(INT(secD)))
ENDIF

nhrd=val(nhrd)
nmind=val(NMind)
nsecd=val(NSecd)

DO WHILE .NOT. NSECD<60
   nsecd=nsecd-60
   nmind=nmind+1
ENDDO

DO WHILE .NOT. NMIND<60
   nmind=nmind-60
   nhrd=nhrd+1
ENDDO

nhrd=alltrim(str(nhrd))
nmind=alltrim(str(nmind))
nsecd=alltrim(str(nsecd))

DO CASE
CASE VAL(nmind)<10
   nmind="0"+nmind


CASE VAL(nsecd)<10
   nsecd="0"+nsecd
ENDCASE

nahar=nhrd+"."+nmind+nsecd

nahar2=VAL(nahar)+6

nahar1=ALLTRIM(STR(nahar2,8,4))
POINT=AT(".",nahar1)
nsec=SUBSTR(nahar1,POINT+3)
nmin=SUBSTR(nahar1,POINT+1,2)
nhr=SUBSTR(nahar1,1,POINT-1)

nahar=nhr+":"+nmin+":"+nsec

DO CASE
CASE outer=7
   sheet[I,16]=nahar
   sheet[I,17]=nahar2
   
CASE outer=8
   sheet[I,18]=nahar
   sheet[I,19]=nahar2
ENDCASE

function zawal
para ATULU,Agroob
*   mdmstulu=3.4532
*   mdmstime=8.1020


mdmstulu=(substr(ATULU,1,at(":",ATULU)-1))+"."+substr(ATULU,at(":",ATULU)+1,2)+substr(ATULU,at(":",ATULU,2)+1)
mdmstime=(substr(AGROOB,1,at(":",AGROOB)-1))+"."+substr(AGROOB,at(":",AGROOB)+1,2)+substr(AGROOB,at(":",AGROOB,2)+1)
mdmstulu=val(mdmstulu)
mdmstime=val(mdmstime)

POINT=AT(".",ALLTRIM(STR(mdmstulu,8,4)))

tsec=SUBSTR(ALLTRIM(STR(mdmstulu,8,4)),POINT+3)
tmin=SUBSTR(ALLTRIM(STR(mdmstulu,8,4)),POINT+1,2)
thr=SUBSTR(ALLTRIM(STR(mdmstulu,8,4)),1,POINT-1)

POINT=AT(".",ALLTRIM(STR(mdmstime,8,4)))


gsec=SUBSTR(ALLTRIM(STR(mdmstime,8,4)),POINT+3)
gmin=SUBSTR(ALLTRIM(STR(mdmstime,8,4)),POINT+1,2)
ghr=SUBSTR(ALLTRIM(STR(mdmstime,8,4)),1,POINT-1)

jama=mdmstime+mdmstulu
jama=ALLTRIM(STR(jama,8,4))

POINT=AT(".",jama)

nsec=VAL(tsec)+VAL(gsec)
nmin=VAL(tmin)+VAL(gmin)
nhr=VAL(thr)+VAL(ghr)

DO WHILE .NOT. NSEC<60
   nsec=nsec-60
   nmin=nmin+1
ENDDO

DO WHILE .NOT. NMIN<60
   nmin=nmin-60
   nhr=nhr+1
ENDDO

summin=0
sumsec=0

IF NHR%2=1
hrd=nhr/2
SUMMIN=30
nhrd=ALLTRIM(STR(INT(HRD)))
ELSE
HRD=NHR/2
nhrd=ALLTRIM(STR(INT(HRD)))
ENDIF

IF NMIN%2=1
MIND=NMIN/2
SUMSEC=30
ELSE
MIND=NMIN/2
ENDIF

secd=nsec/2

IF SUMMIN=30
nmind=ALLTRIM(STR(INT(MIND+30)))
else
nmind=ALLTRIM(STR(INT(MIND)))
ENDIF

IF SUMSEC=30
nsecd=ALLTRIM(STR(INT(secD+30)))
ELSE
nsecd=ALLTRIM(STR(INT(secD)))
ENDIF

nhrd=val(nhrd)
nmind=val(NMind)
nsecd=val(NSecd)

DO WHILE .NOT. NSECD<60
   nsecd=nsecd-60
   nmind=nmind+1
ENDDO

DO WHILE .NOT. NMIND<60
   nmind=nmind-60
   nhrd=nhrd+1
ENDDO

nhrd=alltrim(str(nhrd))
nmind=alltrim(str(nmind))
nsecd=alltrim(str(nsecd))

DO CASE
CASE VAL(nmind)<10
   nmind="0"+nmind

CASE VAL(nsecd)<10
   nsecd="0"+nsecd
ENDCASE

nahar=nhrd+"."+nmind+nsecd

nahar2=VAL(nahar)+6

nahar1=ALLTRIM(STR(nahar2,8,4))
POINT=AT(".",nahar1)
nsec=SUBSTR(nahar1,POINT+3)
nmin=SUBSTR(nahar1,POINT+1,2)
nhr=SUBSTR(nahar1,1,POINT-1)

nahar=nhr+":"+nmin+":"+nsec

return nahar

PROCEDURE SELECTLOCATION
ON KEY
DEFINE WINDOW SELECTLOCATION AT 0,0 SIZE 25,90;
   NONE;
   GROW;
   FLOAT;
   ZOOM;
   CLOSE;
   MINIMIZE;
   TITLE "GO TO YOUR DESIRED LOCATION AND PRESS ESCAPE TO SELECT THE LOCATION";
   FOOTER "***DESIGNED BY MUHAMMAD REHAN QADRI***";
   FONT "ARIAL",10
ACTIVATE WINDOW SELECTLOCATION
MOVE WINDOW SELECTLOCATION CENTER
IF EMPTY(M.CITY)
	BROWS FIELDS CITY,COUNTRY, GREEN :H="GMT";
	NOEDIT NODELE NOAPPEND FONT "VERDANA",10 STYLE "B"
ELSE
	BROWS FIELDS CITY,COUNTRY, GREEN :H="GMT";
	NOEDIT NODELE NOAPPEND FOR LEFT(CITY,LEN(ALLTRIM(M.CITY)))=ALLTRIM(M.CITY);
	FONT "VERDANA",10 STYLE "B"
ENDIF
M.CITY=CITY
SHOW GET M.CITY
ON KEY LABEL ESCAPE DO GETOUT IN UTILITY
DEAC WIND SELECTLOCATION
RELE WIND SELECTLOCATION
RETURN
**********************************************
