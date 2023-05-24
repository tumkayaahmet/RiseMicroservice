RiseMicroservice<br/>
A Phone Directory application consisting of microservices:<br/>
*In the Contact project, operations for adding and editing person and contact information are performed, and the generated report data is prepared and sent to the CreateFile service.<br/>
*In the CreateFile project, the data received from the Contact project is prepared and published to the RabbitMQ queue.<br/>
In the Report project, operations for listing report details are performed. The report data is fetched by listening to the RabbitMQ queue and saved as an Excel report to the respective file.<br/>
<br/>
Prerequisites<br/>
To install the Phone Directory application, you will need the following components:<br/>
ASP.NET Core 6.0<br/>
https://dotnet.microsoft.com/en-us/download/dotnet/6.0<br/>
MongoDbCompass<br/>
https://www.mongodb.com/try/download/shell<br/>
RabbitMQ<br/>
https://www.rabbitmq.com/download.html<br/>

<br/><br/>
 
For Run Project<br/>
1- Right-click on the Project Solution and open Properties.<br/>
  1.1. Check the "Multiple Projects" option.<br/>
  1.2. Select Contact and Report projects as start projects.<br/>
  1.3. Okay<br/>

2- To run the project<br/>

3- To open two Swagger pages, follow these steps<br/>
  3.1- Contact <br/>
  3.1.2 ContactInformation<br/>
        GET     : When a personId is provided, it retrieves the information of the person as a list.<br/>
        POST    : It saves the contact information of the person with the provided personId.<br/>
        PUT     : It updates the contact information of the person with the provided ContactInformationId.<br/>
        DELETE  : It deletes the contact information with the provided ContactInformationId.<br/>
  3.1.3- Person
        GET     : It retrieves the list of people along with their contact information as a combined list.<br/>
        POST    : It creates a new person.<br/>
        PUT     : It updates the person information with the provided personId.<br/>
        GET{personId}: It retrieves the person information associated with the provided personId.        <br/>
        DELETE  : It deletes the person information with the provided personId.<br/>
        
  3.2- Report<br/>
        GET     : It prepares the list of report details.<br/>
        GET    : It retrieves the report detail associated with the provided ReportDetailId.<br/>
        
  3.3- ReportPreparing<br/>
        GET     : It prepares an Excel report that retrieves the number of people based on location.<br/>



Run the tests.<br/>
If the project is running, please stop it.<br/>
Open one of the test classes. Right-click on the [Test] annotation and select "Run Test"<br/>
You can track it using the test explorer.<br/>

What do the tests do?<br/>
Each test verifies the business operations.<br/>

