CLEAR
DEAC WIND ALL
RELE WIND ALL
ON ERROR DO ERRORHANDLER IN UTILITY
ON KEY LABEL ESCAPE DO GETOUT IN UTILITY
SET TALK OFF
SET STAT BAR Off
SET DECIMAL TO 18
IF ISCOLOR()
DEFINE WINDOW WGRAPH AT 0,0 SIZE 30,125;
   SHADOW PANEL MINIMIZE FLOAT GROW CLOSE;
   TITLE "** GRAPHICAL REPRESENTATION OF PRAYER TIMINGS V/S DAYS **";
   FOOTER "DESIGNED BY: MUHAMMAD REHAN QADRI";
   FONT "MS SANS SERIF",8;
   COLOR RGB (,100,,192,192,192);
   FILL FILE SYS(5)+SYS(2003)+"\PAPER13.BMP"
ELSE
DEFINE WINDOW WGRAPH AT 0,0 SIZE 30,125;
   SHADOW PANEL MINIMIZE FLOAT GROW CLOSE;
   TITLE "** GRAPHICAL REPRESENTATION OF PRAYER TIMINGS V/S DAYS **";
   FOOTER "DESIGNED BY: MUHAMMAD REHAN QADRI";
   FONT "MS SANS SERIF",8;
   COLOR RGB (,100,,192,192,192)
ENDIF
   ACTIVATE WINDOW WGRAPH
MOVE WINDOW WGRAPH CENTER


STORE WCOLS([WGRAPH])/WROWS([WGRAPH]) TO di


@ 0,10 SAY ALLTRIM(city) COLOR B+;
   FONT "TIMES NEW ROMAN",14;
   STYLE "BTU"

@ 2,10 TO WROWS([WGRAPH])-4,WCOLS([WGRAPH])-15 PEN 3

i=0
FOR cols=10 TO WCOLS([WGRAPH])-15 STEP 8
   i=i+1
   @ 2,cols TO WROWS([WGRAPH])-4,cols PEN 0,1
   IF i=2
      @ WROWS([WGRAPH])-3,cols-1.5 SAY "30" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=3
      @ WROWS([WGRAPH])-3,cols-1.5 SAY "60" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=4
      @ WROWS([WGRAPH])-3,cols-1.5 SAY "90" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=5
      @ WROWS([WGRAPH])-3,cols-1.5 SAY "120" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=6
      @ WROWS([WGRAPH])-3,cols-1.5 SAY "150" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=7
      @ WROWS([WGRAPH])-3,cols-1.5 SAY "180" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=8
      @ WROWS([WGRAPH])-3,cols-1.5 SAY "210" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=9
      @ WROWS([WGRAPH])-3,cols-1.5 SAY "240" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=10
      @ WROWS([WGRAPH])-3,cols-1.5 SAY "270" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=11
      @ WROWS([WGRAPH])-3,cols-1.5 SAY "300" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=12
      @ WROWS([WGRAPH])-3,cols-1.5 SAY "330" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=13
      @ WROWS([WGRAPH])-3,cols-1.5 SAY "360" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=1
      @ WROWS([WGRAPH])-3,cols-1.5 SAY "0" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   
ENDFOR

i=0
FOR rows=2 TO WROWS([WGRAPH])-6 STEP 2
   i=i+1
   @ rows,10 TO rows,WCOLS([WGRAPH])-15 PEN 0,1
   IF i=1
      @ rows-0.4,6 SAY "24" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=2
      @ rows-0.4,6 SAY "22" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=3
      @ rows-0.4,6 SAY "20" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=4
      @ rows-0.4,6 SAY "18" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=5
      @ rows-0.4,6 SAY "16" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=6
      @ rows-0.4,6 SAY "14" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=7
      @ rows-0.4,6 SAY "12" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=8
      @ rows-0.4,6 SAY "10" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=9
      @ rows-0.4,6 SAY "8" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=10
      @ rows-0.4,6 SAY "6" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=11
      @ rows-0.4,6 SAY "4" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
   IF i=12
      @ rows-0.4,6 SAY "2" FONT "TIMES NEW ROMAN",8;
         STYLE "BT" COLOR B+
   ENDIF
ENDFOR

@ WROWS([WGRAPH])-1,WCOLS([WGRAPH])/2-3 SAY "DAYS" FONT "TIMES NEW ROMAN",8;
   STYLE "BT" COLOR B+
@ WROWS([WGRAPH])/2-2,1 SAY "T" FONT "TIMES NEW ROMAN",8;
   STYLE "BT" COLOR B+
@ WROWS([WGRAPH])/2-1,1 SAY "I" FONT "TIMES NEW ROMAN",8;
   STYLE "BT" COLOR B+
@ WROWS([WGRAPH])/2,1 SAY "M" FONT "TIMES NEW ROMAN",8;
   STYLE "BT" COLOR B+
@ WROWS([WGRAPH])/2+1,1 SAY "E" FONT "TIMES NEW ROMAN",8;
   STYLE "BT" COLOR B+
FOR ti= 1 TO 73
   GOTO ti
   DO CASE
   CASE BAR()=2
      FOR ti= 1 TO 365 STEP 5
         GOTO ti
         TIME=dmssubh
         ab='FAJR TIMINGS'
         
         @ 0,40 SAY ab FONT "TIMES NEW ROMAN",14 STYLE "BTU";
            COLOR B+
         @ ((24-TIME))+2-0.2,10+(days*(8/30))-0.25 TO ((24-TIME))+2+0.2,(10+(days*(8/30)))+0.25 PEN 4 COLOR R+ STYLE "99Q"
      ENDFOR
      
   CASE BAR()=3
      FOR ti= 1 TO 365 STEP 5
         GOTO ti
         TIME=dmstulu
         ab='SUNRISE TIMINGS'
         
         @ 0,40 SAY ab FONT "TIMES NEW ROMAN",14 STYLE "BTU";
            COLOR B+
         @ ((24-TIME))+2-0.2,10+(days*(8/30))-0.25 TO ((24-TIME))+2+0.2,(10+(days*(8/30)))+0.25 PEN 4 COLOR R+ STYLE "99Q"
      ENDFOR
      
   CASE BAR()=4
      FOR ti= 1 TO 365 STEP 5
         GOTO ti
         
         IF dmszawal<6
            TIME=12+dmszawal
         ELSE
            TIME=dmszawal
         ENDIF
         
         ab='DAHWA-E-KUBRA TIMINGS'
         
         @ 0,40 SAY ab FONT "TIMES NEW ROMAN",14 STYLE "BTU";
            COLOR B+
         @ ((24-TIME))+2-0.2,10+(days*(8/30))-0.25 TO ((24-TIME))+2+0.2,(10+(days*(8/30)))+0.25 PEN 4 COLOR R+ STYLE "99Q"
      ENDFOR
      
   CASE BAR()=5
      FOR ti= 1 TO 365 STEP 5
         GOTO ti
         IF dmsnahar<6
            TIME=12+dmsnahar
         ELSE
            TIME=dmsnahar
         ENDIF
         ab='ZOHAR TIMINGS'
         
         @ 0,40 SAY ab FONT "TIMES NEW ROMAN",14 STYLE "BTU";
            COLOR B+
         @ ((24-TIME))+2-0.2,10+(days*(8/30))-0.25 TO ((24-TIME))+2+0.2,(10+(days*(8/30)))+0.25 PEN 4 COLOR R+ STYLE "99Q"
      ENDFOR
      
   CASE BAR()=6
      FOR ti= 1 TO 365 STEP 5
         GOTO ti
         TIME=12+dmsshafi
         ab='ASR-E-SHAFI TIMINGS'
         
         @ 0,40 SAY ab FONT "TIMES NEW ROMAN",14 STYLE "BTU";
            COLOR B+
         @ ((24-TIME))+2-0.2,10+(days*(8/30))-0.25 TO ((24-TIME))+2+0.2,(10+(days*(8/30)))+0.25 PEN 4 COLOR R+ STYLE "99Q"
      ENDFOR
      
   CASE BAR()=7
      FOR ti= 1 TO 365 STEP 5
         GOTO ti
         TIME=12+dmshanfi
         ab='ASR-E-HANFI TIMINGS'
         
         @ 0,40 SAY ab FONT "TIMES NEW ROMAN",14 STYLE "BTU";
            COLOR B+
         @ ((24-TIME))+2-0.2,10+(days*(8/30))-0.25 TO ((24-TIME))+2+0.2,(10+(days*(8/30)))+0.25 PEN 4 COLOR R+ STYLE "99Q"
      ENDFOR
      
   CASE BAR()=8
      FOR ti= 1 TO 365 STEP 5
         GOTO ti
         TIME=12+dmsgroob
         ab='SUNSET TIMINGS'
         
         @ 0,40 SAY ab FONT "TIMES NEW ROMAN",14 STYLE "BTU";
            COLOR B+
         @ ((24-TIME))+2-0.2,10+(days*(8/30))-0.25 TO ((24-TIME))+2+0.2,(10+(days*(8/30)))+0.25 PEN 4 COLOR R+ STYLE "99Q"
      ENDFOR
      
   CASE BAR()=9
      FOR ti= 1 TO 365 STEP 5
         GOTO ti
         TIME=12+dmsisha
         ab='ISHA TIMINGS'
         
         @ 0,40 SAY ab FONT "TIMES NEW ROMAN",14 STYLE "BTU";
            COLOR B+
         IF DMSISHA!=0
         @ ((24-TIME))+2-0.2,10+(days*(8/30))-0.25 TO ((24-TIME))+2+0.2,(10+(days*(8/30)))+0.25 PEN 4 COLOR R+ STYLE "99Q"
         ENDIF
      ENDFOR
      
   ENDCASE
   @ 0,40 SAY ab FONT "TIMES NEW ROMAN",14 STYLE "BTU";
      COLOR B+
   @ ((24-TIME))+2-0.2,10+(days*(8/30))-0.25 TO ((24-TIME))+2+0.2,(10+(days*(8/30)))+0.25 PEN 4 COLOR R+ STYLE "99Q"
ENDFOR

WAIT "PRESS ANY KEY" WIND
SET DECIMAL TO 2
CLOS ALL
CLEAR
DEAC WIND WGRAPH
RELE WIND WGRAPH
RETURN

