# brady-plc

## Things not included in the Solution

A short list of functionality which has not been included in the solution, which would make for a more robust application.

- There is no checking to assure the Xml input conforms to a schema
  - No schema defined
- There is no account taken of input and output file names.

  The application creates an output file of the same name as the input file, but in the output directory. No checking is made to ensure that the new file name is unique in the output directory.

- There is no auditing of files which have or have not been processed

  When a file is processed it is deleted from the directory, this will stop the same files from being processed in the event that the application is restarted without any configuration changes.