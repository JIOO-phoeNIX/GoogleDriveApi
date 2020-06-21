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
    public class GoogleApiDriveRepository
    {
        private static readonly string[] scopes = { DriveService.Scope.Drive };
        private static readonly string applicationName = "Assignment";

        /// <summary>
        /// This method is used to get the user's OAuth 2.0 credentials
        /// </summary>
        /// <returns>The user's OAuth 2.0 credentials</returns>
        public static UserCredential GetUserCredential()
        {
            UserCredential credential;

            using (var stream = new FileStream(@"C:\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string folderPath = @"C:\";
                string credentialPath = Path.Combine(folderPath, "users_credentials.json");
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
        /// 
        /// </summary>
        /// <param name="file"></param>
        public static DriveService CreateService()
        {
            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = GetUserCredential(),
                ApplicationName = applicationName,
            });

            return service;
        }

        /// <summary>
        /// ///
        /// </summary>
        /// <param name="file"></param>
        public static void UploadFile(HttpPostedFileBase file)
        {
            try
            {
                DriveService service = CreateService();

                string fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveApiFiles"),
                    fileName);
                file.SaveAs(path);

                var fileData = new Google.Apis.Drive.v3.Data.File
                {
                    Name = fileName,
                    MimeType = MimeMapping.GetMimeMapping(path)
                };

                FilesResource.CreateMediaUpload mediaUpload;

                using (var stream = new FileStream(path, FileMode.Open))
                {
                    mediaUpload = service.Files.Create(fileData, stream, fileData.MimeType);
                    mediaUpload.Fields = "id";

                    mediaUpload.Upload();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
       
        }
    }
}