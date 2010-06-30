clos all
set safety off
set cursor on
CLEAR
rec=0
@ 12,2 say "Enter Record No. To Display" font "Times New roman",14 style "bt" color g*
@ 12,50 get rec PICTURE "99999" font "Times New roman",14 style "bt" color n
read
clear
use inherit
locate for recno=rec
if found()
dele for recno=rec all
else
wait window "Record Not Found"
endif

use detail
locate for recno=rec
if found()
dele for recno=rec all
wait window "Record Have Been Successfully Deleted"
else
wait window "Record Not Found"
endif

pack

clos all