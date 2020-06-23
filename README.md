# GoogleDriveApi
This is an ASP.NET MVC 5 project which can be used to upload a file to Google drive

This is the documentation of the Assignment. Please read through carefully so as to understand 
the structure of the implementation of the Assignment.

Initially, I enabled Google Drive API V3 for my application by going to the URL:
https://console.developers.google.com/, registered my application with the name "Assignment", 
and I was issued an OAuth 2.0 Client IDs.

I used ASP.NET MCV to implement the Assignment. 
The implementation contains a single Controller (HomeController) which can be found in the folder
~/Controllers/ of project GoogleDriveApi. The Controller contains two action methods named Index and 
UploadFile.

The implementation also contains a View (Index.cshtml) which can be found in the folder 
~/Views/Home/ of the project GoogleDriveApi.

The Index action method of the Controller is used to render the View Index.cshtml to the user. 

The UploadFile action method handles the user's request to upload the file to Google drive. It calls the
static method UploadFile of a Class GoogleApiDriveRepository which can be found in the folder
~/Repository/ of the project GoogleDriveApi. The class GoogleApiDriveRepository encapsulates the
process required to upload a file to Google Drive by authenticating the user first and uploading the file
only if the user grants the application access.
