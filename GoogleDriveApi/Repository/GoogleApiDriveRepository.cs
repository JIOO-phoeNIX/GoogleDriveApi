using System;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;

namespace GoogleDriveApi.Repository
{
    /// <summary>
    /// This class encapsulates the process required to upload a file to Google Drive. It contains
    /// the methods needed to authenticate the user and upload the file.
    /// </summary>
    public class GoogleApiDriveRepository
    {
        private static readonly string[] scopes = { DriveService.Scope.Drive }; //define the scopes
        private static readonly string applicationName = "Assignment";  

        /// <summary>
        /// This method is used to get the user's OAuth 2.0 credentials
        /// </summary>
        /// <returns>The user's OAuth 2.0 credentials</returns>
        public static UserCredential GetUserCredential()
        {
            UserCredential credential;

            //Read the Client Credentials from the client_secret.json file and use it to get the 
            //OAuth 2.0 credentials and store it in the folder C:\users_credentials
            using (FileStream stream = new FileStream(@"C:\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                //Set the folder path where the OAuth 2.0 credentials is stored
                string folderPath = @"C:\";                
                string credentialPath = Path.Combine(folderPath, "users_credentials");

                //Get the OAuth 2.0 credentials after authenticating the user and user gives access
                //and store it in the folder C:\users_credentials
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credentialPath, true)).Result;
            }

            return credential;
        }

        /// <summary>
        /// This method creates a Drive API service using the user's OAuth 2.0 credentials
        /// </summary>
        /// <returns>Google Drive API service</returns>
        public static DriveService CreateService()
        {
            // Create a Drive API service.
            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = GetUserCredential(),
                ApplicationName = applicationName,
            });

            return service;
        }
       
        /// <summary>
        /// This method trys to upload the file to Google Drive using the Drive API Service       
        /// </summary>
        /// <param name="file">The file to be uploaded</param>
        public static FilesResource.CreateMediaUpload UploadFile(HttpPostedFileBase file)
        {
            try
            {
                DriveService service = CreateService();

                //Get the file to be uploaded and save it to the folder ~/GoogleDriveApiFiles/
                string fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(@"C:\GoogleDriveApiFiles", fileName);
                file.SaveAs(path);

                //Create a Google Drive file type using the file Name and MimeType of the file
                //to be uploaded
                var fileData = new Google.Apis.Drive.v3.Data.File
                {
                    Name = fileName,
                    MimeType = MimeMapping.GetMimeMapping(path)
                };

                //Create a media upload that is used to upload the file to Google Drive
                FilesResource.CreateMediaUpload mediaUpload;

                //Open the file to be uploaded to Google Drive
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    //Create the file
                    mediaUpload = service.Files.Create(fileData, stream, fileData.MimeType);
                    mediaUpload.Fields = "id";

                    //Upload the file to Google Drive
                    mediaUpload.Upload();
                }

                return mediaUpload;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}