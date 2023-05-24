RiseMicroservice
A Phone Directory application consisting of microservices:
*In the Contact project, operations for adding and editing person and contact information are performed, and the generated report data is prepared and sent to the CreateFile service.
*In the CreateFile project, the data received from the Contact project is prepared and published to the RabbitMQ queue.
In the Report project, operations for listing report details are performed. The report data is fetched by listening to the RabbitMQ queue and saved as an Excel report to the respective file.

Prerequisites
To install the Phone Directory application, you will need the following components:
ASP.NET Core 6.0
https://dotnet.microsoft.com/en-us/download/dotnet/6.0
MongoDbCompass
https://www.mongodb.com/try/download/shell
RabbitMQ
https://www.rabbitmq.com/download.html



Give examples
For Run Project
1- Right-click on the Project Solution and open Properties.
  1.1. Check the "Multiple Projects" option.
  1.2. Select Contact and Report projects as start projects.
  1.3. Okay

2- To run the project

3- To open two Swagger pages, follow these steps
  3.1- Contact 
  3.1.2 ContactInformation
        GET     : When a personId is provided, it retrieves the information of the person as a list.
        POST    : It saves the contact information of the person with the provided personId.
        PUT     : It updates the contact information of the person with the provided ContactInformationId.
        DELETE  : It deletes the contact information with the provided ContactInformationId.
  3.1.3- Person
        GET     : It retrieves the list of people along with their contact information as a combined list.
        POST    : It creates a new person.
        PUT     : It updates the person information with the provided personId.
        GET{personId}: It retrieves the person information associated with the provided personId.        
        DELETE  : It deletes the person information with the provided personId.
        
  3.2- Report
        GET     : It prepares the list of report details.
        GET    : It retrieves the report detail associated with the provided ReportDetailId.
        
  3.3- ReportPreparing
        GET     : It prepares an Excel report that retrieves the number of people based on location.



Run the tests.
If the project is running, please stop it.
Open one of the test classes. Right-click on the [Test] annotation and select "Run Test"
You can track it using the test explorer.

What do the tests do?
Each test verifies the business operations.

