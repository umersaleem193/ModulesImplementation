# ModulesImplementation
#Overview:
Setup environment involving installing VStudio, .Net 8.0 SDK, SQL Management Studio etc. 
Department modules for which buttons are placed and they can be navigated from there. 
Email module can be accessed by 'Reminder/Create' endpoint.

#Details:
Department Module:
There is a Department that can be created and it will have a name and logo. Once created, it will be saved in DB.
There is a sub department which will be linked to the department as its child and it will also have a name and a logo.
There is an additional layer I have added which is Department Hierarchy which can be used a top layer. 
As a list all departments, sub departments can be seen together which clearly defines their relationship. 
Email Module:
Once reminder is created by accessing path /Reminder/Create an event will be created and hangfire will try to send email at the set date and time, once done it will stop. I have attached screenshot of the email as a proof.

Note: Project could have one more approach where REST APIs could be separately created, however I have worked in purely MVC fashion due to shortage of time to finish this.
