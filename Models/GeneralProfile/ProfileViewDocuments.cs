using System;
using Microsoft.AspNetCore.Http;

namespace Global_Intern.Models.GeneralProfile
{
    public class ProfileViewDocuments
    {

        public ProfileViewDocuments()
        {

        }
        public ProfileViewDocuments(ProfileViewDocuments documents)
        {
            UserCLFullPath = documents.UserCLFullPath;

            UserCVFullPath = documents.UserCVFullPath;

            CVCreatedAt = documents.CVCreatedAt;

            CLCreatedAt = documents.CLCreatedAt;

            CLType = documents.CLType;

            CVType = documents.CVType;

            CLTitle = documents.CLTitle;

            CVTitle = documents.CVTitle;

        }

        public string UserCLFullPath { get; set; }
        public string UserCVFullPath { get; set; }
        public DateTime CVCreatedAt { get; set; }
        public DateTime CLCreatedAt { get; set; }
        public string CLType { get; set; }

        public string CVType { get; set; }
        public string CVTitle { get; set; }

        public string CLTitle { get; set; }

        public virtual User User { get; set; }
    }
}
