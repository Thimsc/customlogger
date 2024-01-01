LOGGER LIBRARY:
Logger library that applications can use to log messages. 
Requirements -
  Client/application make use of your logger library to log messages to a sink.
  Message -
      - has content which is of type string has a level associated with it
    - is directed to a destination (sink)
    - has namespace associated with it to identify the part of application that sent the message
  Sink-
    - This is the destination for a message (eg text file, database, console, etc) Sink is tied to one or more message level
