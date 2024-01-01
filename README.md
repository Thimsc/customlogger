# LOGGER LIBRARY:
Logger library that applications can use to log messages. 

## Logger Library

Technology:
* .Net 6.0
* C#
* Dependency Injenction
* Entity framework 6.0
* SQL server database (optional)

Design Pattern
* Observer design pattern

Sample Log configuration:

  "Logging": {

   "FileProvider": {
     "FileName": "log.txt",
     "LogDirectory": "."
   },
   "DBProvider": {
     "ConnectionString": "..."
   },
   "ConsoleProvider": {
     "Enable": true
   }
 }
