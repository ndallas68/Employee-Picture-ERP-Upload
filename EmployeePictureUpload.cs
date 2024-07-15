using Mongoose.IDO.Protocol;
using System;
using System.IO;
using System.Linq;

namespace EmployeePictureUpload
{
    internal class EmployeePictureUpload
    {
        public Session session;
        public EmployeePictureUpload(string url, string username, string password, string configuration)
        {
            session = new Session();
            session.Login(url, username, password, configuration);
        }
        public void GetImages()
        {
            string dir = @"\\sty-fs-1\Employee_Photos\";
            string[] imageTypes = { ".jpeg", ".jpg", ".png" };
            var files = Directory.EnumerateFiles(dir, "*.*", SearchOption.TopDirectoryOnly)
                .Where(s => imageTypes.Any(ext => s.EndsWith(ext, StringComparison.OrdinalIgnoreCase)));

            foreach (string filePath in files)
            {
                string[] fileNameSplit = Path.GetFileName(filePath).Split('_');
                string empNum = fileNameSplit[0];

                bool intCheck = int.TryParse(empNum, out int i);
                if (intCheck)
                {
                    byte[] bytes = File.ReadAllBytes(filePath);
                    string imageFile = Convert.ToBase64String(bytes);

                    SetEmployeePhoto(empNum, imageFile);
                }
            }

        }


        public void SetEmployeePhoto(string empNum, string photoInBase64)
        {
            LoadCollectionRequestData oRequest = new LoadCollectionRequestData
            {
                IDOName = "SLEmployees",
                PropertyList = new PropertyList("EmpNum"),
                Filter = $"EmpNum={empNum}",
                OrderBy = "",
                RecordCap = 0
            };

            LoadCollectionResponseData oRes = session.client.LoadCollection(oRequest);
            UpdateCollectionRequestData request = new UpdateCollectionRequestData { IDOName = "SLEmployees" };

            if (oRes.Items.Count > 0)
            {
                IDOUpdateItem employeePhoto = new IDOUpdateItem(UpdateAction.Update, oRes.Items[0].ItemID);
                employeePhoto.Properties.Add("Picture", photoInBase64);
                request.Items.Add(employeePhoto);
            }

            UpdateCollectionResponseData uploadRes = session.client.UpdateCollection(request);
        }

    }
}
