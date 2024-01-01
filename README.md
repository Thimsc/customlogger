# LOGGER LIBRARY:
Logger library that applications can use to log messages. 

## Logger Library

 #### Technology:
* .Net 6.0
* C#
* Dependency Injenction
* Entity framework 6.0
* SQL server database (optional)

 #### Design Pattern
* Observer design pattern

 #### Sample Log configuration:
```json
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
```

 #### Test code example:
```
 public class TestDemo
 {
     IDevOnCustomLogger _logger;
     public TestDemo(IDevOnCustomLogger l)
     {
         _logger = l.AttachCurrentType<TestDemo>();
     }

     public async Task Show()
     {
         //Test for log with error
         await _logger.LogAsync("Logger test method", MessageLevel.ERROR);
     }
 }
```
