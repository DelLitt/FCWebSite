namespace FCWeb.Core
{
    using FCCore.Common;
    using Microsoft.AspNet.Http;
    using Microsoft.Net.Http.Headers;
    using System;
    using System.IO;
    using System.Linq;
    using System.Security;

    public class FormUpload
    {
        private string uploadDestination { get; set; }
        private string[] allowedExtensions { get; set; }

        public FormUpload(string uploadDestination, params string[] allowedExtensions)
        {
            this.uploadDestination = uploadDestination;
            this.allowedExtensions = allowedExtensions;
        }

        private bool VerifyFileExtension(string path)
        {
            if(allowedExtensions == null || !allowedExtensions.Any()) { return true; }

            return allowedExtensions.Contains(Path.GetExtension(path));
        }

        private bool VerifyFileSize(IFormFile file)
        {
            double fileSize = GetFileSize(file);

            //filesize less than 10MB => true, else => false
            return (fileSize < 11000) ? true : false;
        }

        private double GetFileSize(IFormFile file)
        {
            double fileSize = 0;
            using (var reader = file.OpenReadStream())
            {
                //get filesize in kb
                fileSize = (reader.Length / 1024);
            }

            return fileSize;
        }


        public string SaveFile(IFormFile file)
        {
            if (string.IsNullOrWhiteSpace(uploadDestination))
            {
                throw new ArgumentNullException(uploadDestination, string.Format("{0} can not be null!", nameof(uploadDestination)));
            }

            string fileName = string.Empty;

            if (file.ContentDisposition != null)
            {
                //parse uploaded file
                var parsedContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                fileName = parsedContentDisposition.FileName.Trim('"');
                string uploadPath = Path.Combine(uploadDestination, fileName);

                //check extension
                if(!this.VerifyFileExtension(uploadPath))
                {
                    throw new SecurityException(string.Format("'{0}' is not allowed extenssion!", Path.GetExtension(uploadPath)));
                }

                //check file size
                if(!this.VerifyFileSize(file))
                {
                    throw new SecurityException(
                        string.Format("File {0} size is too large ({1})!", 
                            fileName, 
                            GetFileSize(file)));
                }

                string phPath = WebHelper.ToPhysicalPath(uploadPath);

                //save the file to upload destination
                file.SaveAs(phPath);
            }

            return fileName;
        }
    }
}
