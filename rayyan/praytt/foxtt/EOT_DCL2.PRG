SET STAT BAR ON
SET TALK OFF
SET SAFETY OFF

select ray
ZAP

inputyear=MYEAR
f_lati = val(subs(latitude,1,2)) + (val(subs(latitude,4,2))/100)
f_lati = f_lati/15
if upper(lo_dir)='EAST'
f_lati = f_lati
else
f_lati = -1 * f_lati
endi
LONGITUDEADJUSTMENT = 0
MONTHNUM2=0
FOR aa = 1 TO 2
	IF aa =1
		timehours = 6-f_lati
	ELSE
	GO TOP
		timehours = 18-f_lati
	ENDI

	timeminutes = 00
	inputminutesaftermidnight = 60 * timehours + timeminutes
	degreestoradians = 3.1416 / 180
	radianstodegrees = 180 / 3.1416
	daylightadjustment = 0

	solarminutesaftermidnight = inputminutesaftermidnight + longitudeadjustment + daylightadjustment

    cyear=alltrim(str(inputyear))
    dateloop=ctod('01/01/'+cyear)

	FOR inputdate2= 1 TO 365
*	@ 12,5 SAY "CREATING TABLES AND MAKING REPLACEMENTS" FONT "ARIAL",17;
	STYLE "B" COLOR RGB(RECNO()*5,RECNO()*10,RECNO()*15,RECNO()*20,RECNO()*25,RECNO()*30)
	@ 12,5 SAY "CREATING TABLES AND MAKING REPLACEMENTS" FONT "ARIAL",17;
	STYLE "B"

		monthnum=MONTH(dateloop)
		IF monthnum2#MONTHNUM
		INPUTDATE=1
		MONTHNUM2=MONTH(dateloop)
		ELSE
		INPUTDATE=INPUTDATE+1
		ENDI
		
		IF monthnum > 2
			correctedyear = inputyear
			correctedmonth = monthnum - 3
		ELSE
			correctedyear = inputyear - 1
			correctedmonth = monthnum  + 9
		ENDI

		t = ((solarminutesaftermidnight / 60.0 / 24.0) + inputdate + FLOOR (30.6 * correctedmonth + 0.5) + FLOOR (365.25 * (correctedyear - 1976)) - 8707.5) / 36525.0

		G = 357.528 + 35999.05 * t
		G = normalizeto360 (G)

		C = (1.915 * SIN (G * degreestoradians)) + (0.020 * SIN (2.0 * G * degreestoradians))

		l = 280.460 + (36000.770 * t) + C
		l = normalizeto360 (l)

		alpha = l - 2.466 * SIN (2.0 * l * degreestoradians) + 0.053 *  SIN (4.0 * l * degreestoradians)

*--------
		eotadjustment = (l - C - alpha) / 15.0 * 60.0
		outputeot = formatfloatstring (eotadjustment)
		IF VAL(RIGHT(outputeot,3))>=100
			OUTPUT=outputeot
			IF LEFT(OUTPUT,1)="-"			
			outputeot=-(ABS(VAL(outputeot))+1-0.1)
			outputeot=ALLTRIM(STR(outputeot,6,2))
			ELSE
			outputeot=ABS(VAL(outputeot))+1-0.1
			outputeot=ALLTRIM(STR(outputeot,6,2))
			ENDIF
		ENDIF

*-------

		obliquity = 23.4393 - 0.013 * t
		declination = atan (tan (obliquity * degreesToRadians) * sin (alpha * degreesToRadians)) * radiansToDegrees
		outputDecl = FormatFloatString (declination)
		IF VAL(RIGHT(OUTPUTDECL,3))>=100
			OUTPUT=OUTPUTDECL
			IF LEFT(OUTPUT,1)="-"			
			OUTPUTDECL=-(ABS(VAL(OUTPUTDECL))+1-0.1)
			OUTPUTDECL=ALLTRIM(STR(OUTPUTDECL,6,2))
			ELSE
			OUTPUTDECL=ABS(VAL(OUTPUTDECL))+1-0.1
			OUTPUTDECL=ALLTRIM(STR(OUTPUTDECL,6,2))
			ENDIF
		ENDIF
*--------

		IF aa=1
			APPE BLAN
			REPL DATE WITH dateloop, days WITH inputdate2,;
			SUN_RISE WITH outputeot, DECLINE6 WITH OUTPUTDECL
		ELSE
			REPL SUN_SET WITH outputeot, DECLINE18 WITH OUTPUTDECL
			SKIP
		ENDI


		dateloop=dateloop+1
		IF DAY(dateloop)=29 AND MONTH(dateloop)=2
			dateloop=dateloop+1
		ENDI

	ENDF
ENDF
DO REPLACE IN EOT_DCL2
SET STAT BAR OFF
RETURN


*--------------------------
FUNCTION formatfloatstring
PARA theinput


negativenumber = .F.
IF theinput < 0
	negativenumber = .T.
	theinput = -1 * theinput
ENDI

integerportion = FLOOR (theinput)
decimalportion = ROUND (theinput * 100,2) % 100

*	// added 7/17/99 to correct problem with 0.999 being rounded to 0.000 in decimal conversion:
IF decimalportion = 0 AND theinput - integerportion > 0.5
	integerportion = integerportion + 1
ENDI
IF integerportion < 10
	integerportionstring = "" + ALLT(STR(integerportion))  &&   // add a space at beginning if necessary
ELSE
	integerportionstring = "" + ALLT(STR(integerportion))
ENDI

IF decimalportion < 10
	decimalportionstring = "0" + ALLT(STR(decimalportion)) &&   // add a leading zero if necessary
ELSE
	decimalportionstring = "" + ALLT(STR(decimalportion))
ENDI

IF negativenumber = .T.
	RETURN ("-" + integerportionstring + "." + decimalportionstring)
ELSE
	RETURN (" " + integerportionstring + "." + decimalportionstring)
ENDI

*--------------------------
*--------------------------
FUNCTION normalizeto360
PARA thething

RETURN (thething - FLOOR (thething / 360.0) * 360)
*--------------------------


PROCEDURE COMP
CLOSE DATABASES
USE RAYYAN IN 1				&& The parent table/.DBF
USE RAYYAN1  IN 2				&& The child table/.DBF
SELECT RAYYAN1
SET ORDER TO TAG DAYS			&& Specify the table/.DBF order
SELECT RAYYAN					&& Parent work area
SET RELATION TO DAYS INTO RAYYAN1	&& Create the relationship
SELECT RAYYAN1					&& Open two Browse windows
BROWSE NOWAIT						&& Moving the record pointer
SELECT RAYYAN					&& in the parent table/.DBF
BROWSE NOWAIT						&& moves the pointer in the child

PROCEDURE REPLACE


replace sign1 with "1" for left(sun_rise,1)="-" all
replace sign2 with "1" for left(sun_set,1)="-" all

replace sign1 with "-1" for left(sun_rise,1)#"-" all
replace sign2 with "-1" for left(sun_set,1)#"-" all

replace dec_dir6 with "SOUTH" for left(DECLINE6,1)="-" all
replace dec_dir6 with "NORTH" for left(DECLINE6,1)#"-" all

replace dec_dir18 with "SOUTH" for left(DECLINE18,1)="-" all
replace dec_dir18 with "NORTH" for left(DECLINE18,1)#"-" all

replace DECLINE18 with ALLTRIM(SUBSTR(DECLINE18,2)) FOR DEC_DIR18="SOUTH"
replace DECLINE6 with ALLTRIM(SUBSTR(DECLINE6,2)) FOR DEC_DIR6="SOUTH"

replace DECLINE18 with ALLTRIM(DECLINE18) FOR DEC_DIR18="NORTH"
replace DECLINE6 with ALLTRIM(DECLINE6) FOR DEC_DIR6="NORTH"

replace SUN_RISE with ALLTRIM(SUBSTR(SUN_RISE,2)) FOR left(sun_rise,1)="-" ALL
replace SUN_SET with ALLTRIM(SUBSTR(SUN_SET,2)) FOR left(sun_SET,1)="-" ALL

replace SUN_RISE with ALLTRIM(SUN_RISE) FOR left(sun_rise,1)#"-" ALL
replace SUN_SET with ALLTRIM(SUN_SET) FOR left(sun_SET,1)#"-" ALL


REPLACE DECLINE6 WITH DMS(DECLINE6) ALL
REPLACE DECLINE18 WITH DMS(DECLINE18) ALL
REPLACE SUN_SET WITH DMS(SUN_SET) ALL
REPLACE SUN_RISE WITH DMS(SUN_RISE) ALL


PROCEDURE CHECK

replace sign1 with "-1" for left(sun_rise,1)#"-" all
replace sign2 with "-1" for left(sun_set,1)#"-" all

replace sign1 with "1" for left(sun_rise,1)="-" all
replace sign2 with "1" for left(sun_set,1)="-" all

replace dec_dir6 with "SOUTH" for left(DECLINE6,1)="-" all
replace dec_dir6 with "NORTH" for left(DECLINE6,1)#"-" all

replace dec_dir18 with "SOUTH" for left(DECLINE18,1)="-" all
replace dec_dir18 with "NORTH" for left(DECLINE18,1)#"-" all

replace decline6 with alltrim(str(abs(val(decline6)),5,2)),decline18 with alltrim(str(abs(val(decline18)),5,2)), sun_rise with alltrim(str(abs(val(sun_rise)),5,2)), sun_set with alltrim(str(abs(val(sun_set)),5,2)) all

function dms
PARA ans
set decimal to 2
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

IF SEC>30
MIN=MIN+1
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
dms=val(hr+"."+MIN+sec)
DMS=ALLTRIM(STR(dms,5,2))
RETURN dms