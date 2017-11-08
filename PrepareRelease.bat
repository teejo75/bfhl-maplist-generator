@Echo off
del *.pdb
del *.vshost.*
del *.xml
del *.log
del *.dat
rd /s /q app.publish
del *.application
del *.manifest
