python "%xulrunner-sdk%\sdk\bin\header.py" -I"%xulrunner-sdk%\idl" -o %1.h %1%2
python "%xulrunner-sdk%\sdk\bin\typelib.py" -I"%xulrunner-sdk%\idl" -o %1.xpt %1%2