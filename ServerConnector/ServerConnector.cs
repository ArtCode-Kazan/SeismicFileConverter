using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;

namespace ServerConnectorLib
{
    public class DesriptionInfo
    {
        public string version;
        public string hashsum;

        public DesriptionInfo(
            string version,
            string hashsum
            )
        {
            this.version = version;
            this.hashsum = hashsum;
        }
    }

    public class ServerConnector
    {
        public const int VersionSegmentsAmount = 4;

        public Uri serverUrl;
        public string archiveName;
        public string descriptionName;
        public string versionFieldName;
        public string hashsumMD5FieldName;

        public ServerConnector(string serverUrlString,
            string archiveName,
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

        public DesriptionInfo GetDescription()
        {
            string line;
            string serverHashsum = "";
            string serverVersion = "";
            try
            {
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

                                if (line.Contains(this.versionFieldName))
                                {
                                    serverVersion = line.Split(':')[1].Split(' ')[1];
                                }
                            }
                        }
                    }
                }
            }
            catch (WebException e)
            {
                throw new WebException(e.Message);
            }

            return new DesriptionInfo(serverVersion, serverHashsum);
        }

        public bool IsVersionLatest(string currentVersion)
        {
            if (GetDescription().version.Split('.').Length == currentVersion.Split('.').Length)
            {
                string[] originVersion = currentVersion.Split('.');
                string[] serverVersion = GetDescription().version.Split('.');

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
            if (originHashsum == GetDescription().hashsum)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DownloadFile(string savePath, Uri url)
        {
            try
            {
                using (WebClient wclient = new WebClient())
                {
                    wclient.DownloadFile(address: url, fileName: savePath);
                }
            }
            catch (WebException e)
            {
                throw new WebException(e.Message);
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
}
