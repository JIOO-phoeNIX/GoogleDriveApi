using System.Web;
using GoogleDriveApi.Repository;
using Xunit;
using Moq;


namespace GoogleDriveApiTest
{
    public class GoogleDriveApiTest
    {
        /// <summary>
        /// Test the OAuth 2 Credentials process
        /// </summary>
        [Fact]
        public void GetUserCredentialTest()
        {
            // Arrange            

            // Act
            var actual = GoogleApiDriveRepository.GetUserCredential();

            // Assert
            Assert.NotNull(actual);
        }

        /// <summary>
        /// Test the service method used to upload the file
        /// </summary>
        [Fact]
        public void CreateServiceTest()
        {
            // Arrange

            // Act
            var actual = GoogleApiDriveRepository.CreateService();

            // Assert 
            Assert.NotNull(actual);
        }

        /// <summary>
        /// Test the upload method used to upload the file to Google Drive
        /// </summary>
        [Fact]
        public void UploadFileTest()
        {
            // Arrange           

            // Arrange
            Mock<HttpPostedFileBase> uploadedFile = new Mock<HttpPostedFileBase>();
            uploadedFile.Setup(x => x.FileName).Returns("imagefile.jpg");            

            // Act
            var actual = GoogleApiDriveRepository.UploadFile(uploadedFile.Object);

            // Arrange
            Assert.NotNull(actual);
        }       
    }
}
