   *************************************************************
   *- Program/Function:  INFO.PRG
   *-          Created:  11/18/93
   *-            Owner:  Mark Bonafe'
   *-          Purpose:  This function creates a message box using the
   *-                    FOXTOOLS.FLL library.  The function returns a
   *-                    character expression from the following list:
   *-                      1. OK
   *-                      2. Cancel
   *-                      3. Abort
   *-                      4. Retry
   *-                      5. Ignore
   *-                      6. Yes
   *-                      7. No
   *-
   *-     How it Works:  The first parameter, gcMsg, is used for Question or
   *-                    Information you want to provide the user.
   *-
   *-                    The second parameter, gcBoxTtl, is used for the Title
   *-                    of the resulting window.
   *-
   *-                    The third parameter, gcButtons, is used to tell
   *-                    INFO.PRG which buttons to display.  You can choose
   *-                    from the list below:
   *-                      0. O   = OK
   *-                      1. OC  = OK, Cancel
   *-                      2. ARI = Abort, Retry, Ignor
   *-                      3. YNC = Yes, No, Cancel
   *-                      4. YN  = Yes, No
   *-                      5. RC  = Retry, Cancel
   *-
   *-                    The fourth parameter, gcType, is used to determine
   *-                    what type of message box to display.  You can
   *-                    choose from this list:
   *-                      1. Regular
   *-                      2. Stop
   *-                      3. Question
   *-                      4. Exclaim
   *-                      5. Info
   *-
   *-    Example Usage:  To ask the user if they want to continue and you
   *-                    want "Yes, No, Cancel" buttons:
   *-                      continue = INFO("Do you wish to continue", ;
   *-                                      "Attention User", ;
   *-                                      "YNC", ;
   *-                                      "Question")
   *-                     INFO.PRG will return one of three choices,
   *-                       "Yes", "No", or "Cancel".
   ************************************************************************

   PARAMETERS gcmsg,gcboxttl,gcbuttons,gctype
   PRIVATE gntype,gcexact,gnbuttons,gnret_code
   STORE 'OK' TO GCRET_CODE
   STORE SET('EXACT') TO gcexact
   SET EXACT ON

   *- check for valid gcType.  Default to "REGULAR"
   IF TYPE('gcType') != "C"
      STORE "REGULAR" TO gctype
   ELSE
      gctype=UPPER(gctype)
   ENDIF
   gctype = IIF(INLIST(gctype,"REGULAR","STOP","QUESTION","EXCLAIM","INFO"),;
      gctype,"REGULAR")

   *- Check for valid gcButtons. Default to "O"
   IF TYPE('gcButtons') != "C"
      STORE "O" TO gcbuttons
   ELSE
      gcbuttons = IIF(INLIST(gcbuttons,"O","OC","ARI","YNC","YN","RC"), ;
         gcbuttons,"O")
   ENDIF

   *- Check for valid gcMsg.  Default to "Hey, you forgot to send INFO a message"
   IF TYPE('gcMsg') != "C"
      STORE "Hey, you forgot to send INFO a message" TO gcmsg
   ENDIF

   *- Check for valid gcBoxTtl.  Default to "Attention"
   IF TYPE('gcBoxTtl') != "C"
      STORE "Attention" TO gcboxttl
   ENDIF

   *- set gnType to correct value
   DO CASE
      CASE gctype="STOP"
         STORE 1 TO gntype
      CASE gctype="QUESTION"
         STORE 2 TO gntype
      CASE gctype="EXCLAIM"
         STORE 3 TO gntype
      CASE gctype="INFO"
         STORE 4 TO gntype
      OTHERWISE
         STORE 0 TO gntype
   ENDCASE
   gntype = 16 * gntype

   *- set gnButtons to correct value
   DO CASE
      CASE gcbuttons = "O"
         STORE 0 TO gnbuttons
      CASE gcbuttons = "OC"
         STORE 1 TO gnbuttons
      CASE gcbuttons = "ARI"
         STORE 2 TO gnbuttons
      CASE gcbuttons = "YNC"
         STORE 3 TO gnbuttons
      CASE gcbuttons = "YN"
         STORE 4 TO gnbuttons
      CASE gcbuttons = "RC"
         STORE 5 TO gnbuttons
   ENDCASE
   gnbuttons = gntype + gnbuttons

   *- setup and send the message
   IF _WINDOWS
     SET LIBRARY TO foxtools.fll ADDITIVE
     STORE msgbox(gcmsg,gcboxttl,gnbuttons) TO gnret_code
     RELEASE LIBRARY foxtools.fll
   ELSE
   GNRET_CODE=DOSINFO(GCBOXTTL,GCMSG,GNBUTTONS)
   ENDIF
   DO CASE
      CASE gnret_code = 1
         STORE "OK" TO gcret_code
      CASE gnret_code = 2
         STORE "CANCEL" TO gcret_code
      CASE gnret_code = 3
         STORE "ABORT" TO gcret_code
      CASE gnret_code = 4
         STORE "RETRY" TO gcret_code
      CASE gnret_code = 5
         STORE "IGNORE" TO gcret_code
      CASE gnret_code = 6
         STORE "YES" TO gcret_code
      CASE gnret_code = 7
         STORE "NO" TO gcret_code
   ENDCASE

   IF SET('exact') != gcexact
      IF gcexact = "ON"
         SET EXACT ON
      ELSE
         SET EXACT OFF
      ENDIF
   ENDIF

   RETURN gcret_code
