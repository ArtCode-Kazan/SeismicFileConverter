using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;

namespace UpdaterLib
{
    public class ServerInfo
    {
        public const int VersionSegmentsAmount = 4;

        public Uri serverUrl;
        public string archiveName;
        public string descriptionName;
        public string versionFieldName;
        public string hashsumMD5FieldName;

        public ServerInfo(string serverUrlString = "https://sigma-geophys.com/Distr/",
            string archiveName = "SeisJsonConveter.zip",
            string descriptionName = "version.txt",
            string versionFieldName = "version:",
            string hashsumMD5FieldName = "MD5:")
        {
            this.serverUrl = new Uri(serverUrlString);
            this.archiveName = archiveName;
            this.descriptionName = descriptionName;
            this.versionFieldName = versionFieldName;
            this.hashsumMD5FieldName = hashsumMD5FieldName;
        }

        public Uri DescriptionUri
        {
            get
            {
                Uri uriToDescription = new Uri(baseUri: this.serverUrl, relativeUri: this.descriptionName);
                return uriToDescription;
            }
        }

        public Uri ArchiveUri
        {
            get
            {
                Uri archiveUri = new Uri(baseUri: this.serverUrl, relativeUri: this.archiveName);
                return archiveUri;
            }
        }

        public string GetAppVersion()
        {
            string line;
            string serverVersion = "";
            using (WebClient client = new WebClient())
            {
                using (Stream stream = client.OpenRead(DescriptionUri))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains(this.versionFieldName))
                            {
                                serverVersion = line.Split(':')[1].Split(' ')[1];
                            }
                        }
                    }
                }
            }
            return serverVersion;
        }

        public string GetHashsum()
        {
            string line;
            string serverHashsum = "";
            using (WebClient client = new WebClient())
            {
                using (Stream stream = client.OpenRead(DescriptionUri))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains(this.hashsumMD5FieldName))
                            {
                                serverHashsum = line.Split(':')[1].Split(' ')[1];
                            }
                        }
                    }
                }
            }
            return serverHashsum;
        }

        public bool IsVersionLatest(string currentVersion)
        {
            if (GetAppVersion().Length == currentVersion.Length)
            {
                string[] originVersion = currentVersion.Split('.');
                string[] serverVersion = GetAppVersion().Split('.');

                for (int i = 0; i < VersionSegmentsAmount; i++)
                {
                    if (Convert.ToInt16(originVersion[i]) < Convert.ToInt16(serverVersion[i]))
                    {
                        return false;
                    }
                }
            }
            else
            {
                throw new InvalidVersionFormat("Invalid version format");
            }
            return true;
        }

        public bool IsHashsumEqual(string originHashsum)
        {
            if (originHashsum == GetHashsum())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    [Serializable]
    internal class InvalidVersionFormat : Exception
    {
        public InvalidVersionFormat()
        {
        }

        public InvalidVersionFormat(string message) : base(message)
        {
        }

        public InvalidVersionFormat(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidVersionFormat(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
