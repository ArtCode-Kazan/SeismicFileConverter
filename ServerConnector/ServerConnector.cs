using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;

namespace ServerConnectorLib
{
    /// <summary>
    /// Class DesriptionInfo for information about program.
    /// </summary>
    public class DesriptionInfo
    {
        public string version;
        public string hashsum;

        /// <summary>
        /// Initializes a new instance of the <see cref="DesriptionInfo"/> class.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <param name="hashsum">The hashsum.</param>
        public DesriptionInfo(
            string version,
            string hashsum
            )
        {
            this.version = version;
            this.hashsum = hashsum;
        }
    }

    public interface IServerConnector
    {
        DesriptionInfo GetDescription();
        bool IsVersionLatest(string version);
        bool IsHashsumEqual(string originHashsum);
        void DownloadFile(string savePath, Uri url);
    }

    /// <summary>
    /// Class ServerConnector.
    /// Implements the <see cref="ServerConnectorLib.IServerConnector" />
    /// </summary>
    /// <seealso cref="ServerConnectorLib.IServerConnector" />
    public class ServerConnector : IServerConnector
    {
        /// <summary>
        /// Amount of number beetwen dots.
        /// </summary>
        public const int VersionSegmentsAmount = 4;

        public Uri serverUrl;
        public string archiveName;
        public string descriptionName;
        public string versionFieldName;
        public string hashsumMD5FieldName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerConnector"/> class.
        /// </summary>
        /// <param name="serverUrlString">The server URL string.</param>
        /// <param name="archiveName">Name of the archive.</param>
        /// <param name="descriptionName">Name of the description file with extension.</param>
        /// <param name="versionFieldName">Field name of the version.</param>
        /// <param name="hashsumMD5FieldName">Field name of the hashsum md5.</param>
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

        /// <summary>
        /// Gets the description URI.
        /// </summary>
        /// <value>The description URI.</value>
        public Uri DescriptionUri
        {
            get
            {
                Uri uriToDescription = new Uri(baseUri: this.serverUrl, relativeUri: this.descriptionName);
                return uriToDescription;
            }
        }

        /// <summary>
        /// Gets the archive URI.
        /// </summary>
        /// <value>The archive URI.</value>
        public Uri ArchiveUri
        {
            get
            {
                Uri archiveUri = new Uri(baseUri: this.serverUrl, relativeUri: this.archiveName);
                return archiveUri;
            }
        }

        /// <summary>
        /// Gets the actual information about version and hashsum.
        /// </summary>
        /// <returns>DesriptionInfo with actual information from server</returns>
        public virtual DesriptionInfo GetDescription()
        {
            string line;
            string serverHashsum = "";
            string serverVersion = "";

            using (WebClient client = new WebClient())
            {
                using (Stream stream = client.OpenRead(this.DescriptionUri))
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
            return new DesriptionInfo(serverVersion, serverHashsum);
        }

        /// <summary>
        /// Determines whether [is version latest] [the specified version].
        /// </summary>
        /// <param name="version">The current version.</param>
        /// <returns><c>true</c> if [is version latest] [the specified version]; otherwise, <c>false</c>.</returns>
        /// <exception cref="ServerConnectorLib.ServerConnector.InvalidVersionFormat">Invalid version format</exception>
        public virtual bool IsVersionLatest(string version)
        {
            string[] currentVersion = version.Split('.');
            string[] serverVersion = this.GetDescription().version.Split('.');

            if (serverVersion.Length == currentVersion.Length)
            {
                for (int i = 0; i < VersionSegmentsAmount; i++)
                {
                    if (Convert.ToInt16(currentVersion[i]) < Convert.ToInt16(serverVersion[i]))
                    {
                        return false;
                    }
                    else if (Convert.ToInt16(currentVersion[i]) > Convert.ToInt16(serverVersion[i]))
                    {
                        return true;
                    }
                }
                return true;
            }
            else
            {
                throw new InvalidVersionFormat("Invalid version format");
            }
        }

        /// <summary>
        /// Determines whether [is hashsum equal] [the specified origin hashsum].
        /// </summary>
        /// <param name="originHashsum">The current hashsum.</param>
        /// <returns><c>true</c> if [is hashsum equal] [the specified origin hashsum]; otherwise, <c>false</c>.</returns>
        public virtual bool IsHashsumEqual(string originHashsum)
        {
            if (originHashsum == this.GetDescription().hashsum)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Downloads the file.
        /// </summary>
        /// <param name="savePath">The save path.</param>
        /// <param name="url">The URL.</param>
        public void DownloadFile(string savePath, Uri url)
        {
            using (WebClient wclient = new WebClient())
            {
                wclient.DownloadFile(address: url, fileName: savePath);
            }
        }

        [Serializable]
        public class InvalidVersionFormat : Exception
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
