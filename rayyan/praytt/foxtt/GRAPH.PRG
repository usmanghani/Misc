CLEAR
DEAC WIND ALL
RELE WIND ALL
SET TALK OFF
sET STAT BAR Off
ON ERROR DO ERRORHANDLER IN UTILITY
ON KEY LABEL ESCAPE DO GETOUT IN UTILITY
SET DECIMAL TO 18
IF ISCOLOR()
DEFINE WINDOW WGRAPH AT 0,0 SIZE 30,125;
   SHADOW PANEL MINIMIZE FLOAT GROW;
   TITLE "** GRAPHICAL REPRESENTATION OF PRAYER TIMINGS V/S DAYS **";
   FOOTER "DESIGNED BY: MUHAMMAD REHAN QADRI";
   FONT "MS SANS SERIF",8;
   COLOR RGB (,100,,192,192,192);
   FILL FILE SYS(5)+SYS(2003)+"\PAPER13.BMP"
ELSE
DEFINE WINDOW WGRAPH AT 0,0 SIZE 30,125;
   SHADOW PANEL MINIMIZE FLOAT GROW;
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
FOR rows=2 TO WROWS([WGRAPH])-4 STEP 2
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
FOR COUNT=1 TO 8
   IF COUNT=1
      FOR ti= 1 TO 365 STEP 5
         GOTO ti
         TIME=12+val((substr(groob,1,at(":",groob)-1))+"."+substr(groob,at(":",groob)+1,2)+substr(groob,at(":",groob,2)+1))
         @ ((24-TIME))+2-0.2,10+(days*(8/30))-0.25 TO ((24-TIME))+2+0.2,(10+(days*(8/30)))+0.25 STYLE "99Q" PEN 4 COLOR R+
      ENDFOR
   ENDIF
   
   
   IF COUNT=2
      FOR ti= 1 TO 365 STEP 5
         GOTO ti
         TIME=val((substr(tulu,1,at(":",tulu)-1))+"."+substr(tulu,at(":",tulu)+1,2)+substr(tulu,at(":",tulu,2)+1))
         @ ((24-TIME))+2-0.2,10+(days*(8/30))-0.25 TO ((24-TIME))+2+0.2,(10+(days*(8/30)))+0.25 STYLE "99Q" PEN 4 COLOR B+
      ENDFOR
   ENDIF
   
   
   IF COUNT=3
       FOR ti= 1 TO 365 STEP 5
         GOTO ti
         TIME=val((substr(subh_sadiq,1,at(":",subh_sadiq)-1))+"."+substr(subh_sadiq,at(":",subh_sadiq)+1,2)+substr(subh_sadiq,at(":",subh_sadiq,2)+1))
         IF DMSSUBH!=DMSNAHAR
         @ ((24-TIME))+2-0.2,10+(days*(8/30))-0.25 TO ((24-TIME))+2+0.2,(10+(days*(8/30)))+0.25 STYLE "99Q" PEN 4 COLOR GB+
         ENDIF
      ENDFOR
   ENDIF
   

   IF COUNT=4
      FOR ti= 1 TO 365 STEP 5
         GOTO ti
            TIME=12+val((substr(isha,1,at(":",isha)-1))+"."+substr(isha,at(":",isha)+1,2)+substr(isha,at(":",isha,2)+1))
         IF DMSISHA!=0
         @ ((24-TIME))+2-0.2,10+(days*(8/30))-0.25 TO ((24-TIME))+2+0.2,(10+(days*(8/30)))+0.25 STYLE "99Q" PEN 4 COLOR RB*
         ENDIF
      ENDFOR
   ENDIF
   
   
   IF COUNT=5
      FOR ti= 1 TO 365 STEP 5
         GOTO ti
         TIME=12+val((substr(asr_hanfi,1,at(":",asr_hanfi)-1))+"."+substr(asr_hanfi,at(":",asr_hanfi)+1,2)+substr(asr_hanfi,at(":",asr_hanfi,2)+1))
         @ ((24-TIME))+2-0.2,10+(days*(8/30))-0.25 TO ((24-TIME))+2+0.2,(10+(days*(8/30)))+0.25 STYLE "99Q" PEN 4 COLOR W+
      ENDFOR
   ENDIF
   
   
   IF COUNT=6
      FOR ti= 1 TO 365 STEP 5
         GOTO ti
         TIME=12+val((substr(asr_shafi,1,at(":",asr_shafi)-1))+"."+substr(asr_shafi,at(":",asr_shafi)+1,2)+substr(asr_shafi,at(":",asr_shafi,2)+1))
         @ ((24-TIME))+2-0.2,10+(days*(8/30))-0.25 TO ((24-TIME))+2+0.2,(10+(days*(8/30)))+0.25 STYLE "99Q" PEN 4 COLOR GR+
      ENDFOR
   ENDIF
   
   
   IF COUNT=7
      FOR ti= 1 TO 365 STEP 5
         GOTO ti
         IF dmsnahar<6
            TIME=12+val((substr(n_nahar,1,at(":",n_nahar)-1))+"."+substr(n_nahar,at(":",n_nahar)+1,2)+substr(n_nahar,at(":",n_nahar,2)+1))
         ELSE
            TIME=val((substr(n_nahar,1,at(":",n_nahar)-1))+"."+substr(n_nahar,at(":",n_nahar)+1,2)+substr(n_nahar,at(":",n_nahar,2)+1))
         ENDIF
         
         @ ((24-TIME))+2-0.2,10+(days*(8/30))-0.25 TO ((24-TIME))+2+0.2,(10+(days*(8/30)))+0.25 STYLE "99Q" PEN 4 COLOR G+
      ENDFOR
   ENDIF
   
   
   IF COUNT=8
      FOR ti= 1 TO 365 STEP 5
         GOTO ti
         IF dmszawal<6
            TIME=12+val((substr(zawal,1,at(":",zawal)-1))+"."+substr(zawal,at(":",zawal)+1,2)+substr(zawal,at(":",zawal,2)+1))
         ELSE
            TIME=val((substr(zawal,1,at(":",zawal)-1))+"."+substr(zawal,at(":",zawal)+1,2)+substr(zawal,at(":",zawal,2)+1))
         ENDIF
         @ ((24-TIME))+2-0.2,10+(days*(8/30))-0.25 TO ((24-TIME))+2+0.2,(10+(days*(8/30)))+0.25 STYLE "99Q" PEN 4 COLOR RB+
      ENDFOR
   ENDIF
   
ENDFOR

@ 2,WCOLS([WGRAPH])-14 TO 24,WCOLS([WGRAPH]) DOUBLE PEN 2 COLOR G*
@ 2.5,WCOLS([WGRAPH])-12.5 SAY "LEGENDS" FONT "TIMES NEW ROMAN",8 STYLE "BTU" COLOR B+
@ 4,WCOLS([WGRAPH])-14 TO 4,WCOLS([WGRAPH]) PEN 2 COLOR G*
@ 12-0.2-6,WCOLS([WGRAPH])-13 TO 12+0.2-6,WCOLS([WGRAPH])-12 STYLE "99Q" PEN 2 PATTERN 1 COLOR RB*
@ 11.5-6,WCOLS([WGRAPH])-10 SAY "ISHA" FONT "TIMES NEW ROMAN",8 STYLE "BT" COLOR B+
@ 14-0.2-6,WCOLS([WGRAPH])-13 TO 14+0.2-6,WCOLS([WGRAPH])-12 STYLE "99Q" PEN 2 PATTERN 1 COLOR R+
@ 13.5-6,WCOLS([WGRAPH])-10 SAY "GROOB" FONT "TIMES NEW ROMAN",8 STYLE "BT" COLOR B+
@ 16-0.2-6,WCOLS([WGRAPH])-13 TO 16+0.2-6,WCOLS([WGRAPH])-12 STYLE "99Q" PEN 2 PATTERN 1 COLOR W+
@ 15.5-6,WCOLS([WGRAPH])-10 SAY "ASR[H]" FONT "TIMES NEW ROMAN",8 STYLE "BT" COLOR B+
@ 18-0.2-6,WCOLS([WGRAPH])-13 TO 18+0.2-6,WCOLS([WGRAPH])-12 STYLE "99Q" PEN 2 PATTERN 1 COLOR GR+
@ 17.5-6,WCOLS([WGRAPH])-10 SAY "ASR[S]" FONT "TIMES NEW ROMAN",8 STYLE "BT" COLOR B+
@ 20-0.2-6,WCOLS([WGRAPH])-13 TO 20+0.2-6,WCOLS([WGRAPH])-12 STYLE "99Q" PEN 2 PATTERN 1 COLOR G+
@ 19.5-6,WCOLS([WGRAPH])-10 SAY "ZOHAR" FONT "TIMES NEW ROMAN",8 STYLE "BT" COLOR B+
@ 22-0.2-6,WCOLS([WGRAPH])-13 TO 22+0.2-6,WCOLS([WGRAPH])-12 STYLE "99Q" PEN 2 PATTERN 1 COLOR RB+
@ 21.5-6,WCOLS([WGRAPH])-10 SAY "D_KUBRA" FONT "TIMES NEW ROMAN",8 STYLE "BT" COLOR B+
@ 24-0.2-6,WCOLS([WGRAPH])-13 TO 24+0.2-6,WCOLS([WGRAPH])-12 STYLE "99Q" PEN 2 PATTERN 1 COLOR B+
@ 23.5-6,WCOLS([WGRAPH])-10 SAY "TULU" FONT "TIMES NEW ROMAN",8 STYLE "BT" COLOR B+
@ 26-0.2-6,WCOLS([WGRAPH])-13 TO 26+0.2-6,WCOLS([WGRAPH])-12 STYLE "99Q" PEN 2 PATTERN 1 COLOR GB+
@ 25.5-6,WCOLS([WGRAPH])-10 SAY "SUBH" FONT "TIMES NEW ROMAN",8 STYLE "BT" COLOR B+
@ 26.5-6,WCOLS([WGRAPH])-10 SAY "SADIQ" FONT "TIMES NEW ROMAN",8 STYLE "BT" COLOR B+
WAIT "PRESS ANY KEY" WIND
SET DECIMAL TO 2
CLOS ALL
CLEAR
DEAC WIND WGRAPH
RELE WIND WGRAPH
RETURN
