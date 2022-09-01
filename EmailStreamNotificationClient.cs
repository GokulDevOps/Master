using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Phoenix.Object.EmailWatcher
{
        public interface IMailStreamNotification
        {
            void ConnectMailServer(StreamNotificationParameters objStreamNotificationParameters);
        }


        [Serializable]
        public class StreamNotificationList
        {
            public List<StreamNotificationParameters> objNotificationParameterList;

            public StreamNotificationList()
            {
                objNotificationParameterList = new List<StreamNotificationParameters>();
            }

            public int Count
            {
                get
                {
                    return objNotificationParameterList.Count;
                }
            }

            public void Add(StreamNotificationParameters objStreamNotificationParameters)
            {
                try
                {
                    if (objNotificationParameterList != null)
                        objNotificationParameterList.Add(objStreamNotificationParameters);
                }
                catch (Exception objError)
                {

                }
            }

            public StreamNotificationParameters this[int index]
            {
                get
                {
                    if (Count == -1 || index < 0 || index >= objNotificationParameterList.Count)
                        return null;
                    else
                        return (StreamNotificationParameters)objNotificationParameterList[index];
                }
            }
        }

        public class StreamNotificationParameters
        {
            private System.String _MailServer;
            private System.String _EmailAddress;
            private System.String _Password;
            private System.Int32 _Port;
            public bool IsSSL = false;
            private System.String _FolderPath;
            private System.String _JurisID;
            private System.String _DBName;
            private System.String _WebServiceUrl;
            private System.String _FDID;
            private System.String _IsCadInstalled;
            private System.String _VendorName;
            private System.String _ApplicationMode;
            public bool IsTest = false;
            private System.String _AgencyName;
            private System.DateTime _ServerStartTime;
            private System.String _MailServerType;
            private System.String _UserName;
            private System.String _Domain;



        public StreamNotificationParameters()
        {
            _MailServer = null;
            _EmailAddress = null;
            _Password = null;
            _Port = System.Int32.MinValue;
            _FolderPath = null;
            _JurisID = null;
            _DBName = null;
            _WebServiceUrl = null;

            _ApplicationMode = null;
            _AgencyName = null;
            _ServerStartTime = DateTime.MinValue;
            _MailServerType = null;
            SourceName = String.Empty;
            HlpDeskClass = null;
            Category = null;
            Type = null;
            Priority = null;
            Status = null;
            OwnerPFID = Int64.MinValue;
            NotifyEmails = null;
            SMTPMSServer = null;
            SMTPMSPort = null;
            MailProcessType = null;
            SMTPUnModifiedServer = "";
            AFLGFolder = null;
            _UserName = null;
            _Domain = null;
            HLPSettingID = Int64.MinValue;
        }
        public string SourceName { get; set; }
        public string MailProcessType { get; set; }
        public string SMTPUnModifiedServer { get; set; }
        public string HlpDeskClass { get;  set; }
        public string Category { get;  set; }
        public string Type { get;  set; }
        public string Priority { get;  set; }
        public string Status { get;  set; }
        public long OwnerPFID { get;  set; }
        public string NotifyEmails { get;  set; }
        public string SMTPMSServer { get; set; }
        public string SMTPMSPort { get; set; }
        public string AFLGFolder { get; set; }

        public long HLPSettingID { get; set; }


        public System.String MailServer
            {
                get
                {
                    return this._MailServer;
                }
                set
                {
                    this._MailServer = value;
                }
            }

            public bool MailServerIsNull
            {
                get
                {
                    return ((this._MailServer == null) || !(this._MailServer != null && this._MailServer.Length > 0));
                }
            }
            public System.String EmailAddress
            {
                get
                {
                    return this._EmailAddress;
                }
                set
                {
                    this._EmailAddress = value;
                }
            }

            public bool EmailAddressIsNull
            {
                get
                {
                    return ((this._EmailAddress == null) || !(this._EmailAddress != null && this._EmailAddress.Length > 0));
                }
            }
            public System.String Password
            {
                get
                {
                    return this._Password;
                }
                set
                {
                    this._Password = value;
                }
            }

            public bool PasswordIsNull
            {
                get
                {
                    return ((this._Password == null) || !(this._Password != null && this._Password.Length > 0));
                }
            }

            public System.Int32 Port
            {
                get
                {
                    return this._Port;
                }
                set
                {
                    this._Port = value;
                }
            }

            public bool PortIsNull
            {
                get
                {
                    return (this._Port == System.Int32.MinValue);
                }
            }

            public System.String FolderPath
            {
                get
                {
                    return this._FolderPath;
                }
                set
                {
                    this._FolderPath = value;
                }
            }

            public bool FolderPathIsNull
            {
                get
                {
                    return ((this._FolderPath == null) || !(this._FolderPath != null && this._FolderPath.Length > 0));
                }
            }

            public System.String JurisID
            {
                get
                {
                    return this._JurisID;
                }
                set
                {
                    this._JurisID = value;
                }
            }

            public bool JurisIDIsNull
            {
                get
                {
                    return ((this._JurisID == null) || !(this._JurisID != null && this._JurisID.Length > 0));
                }
            }

            public System.String DBName
            {
                get
                {
                    return this._DBName;
                }
                set
                {
                    this._DBName = value;
                }
            }

            public bool DBNameIsNull
            {
                get
                {
                    return ((this._DBName == null) || !(this._DBName != null && this._DBName.Length > 0));
                }
            }

            public System.String WebServiceUrl
            {
                get
                {
                    return this._WebServiceUrl;
                }
                set
                {
                    this._WebServiceUrl = value;
                }
            }

            public bool WebServiceUrlIsNull
            {
                get
                {
                    return ((this._WebServiceUrl == null) || !(this._WebServiceUrl != null && this._WebServiceUrl.Length > 0));
                }
            }

            public System.String UserName
            {
                get
                {
                    return this._UserName;
                }
                set
                {
                    this._UserName = value;
                }
            }

            public bool UserNameIsNull
            {
                get
                {
                    return ((this._UserName == null) || !(this._UserName != null && this._UserName.Length > 0));
                }
            }

        public System.String FDID
            {
                get
                {
                    return this._FDID;
                }
                set
                {
                    this._FDID = value;
                }
            }

            public bool FDIDIsNull
            {
                get
                {
                    return ((this._FDID == null) || !(this._FDID != null && this._FDID.Length > 0));
                }
            }

            public System.String IsCadInstalled
            {
                get
                {
                    return this._IsCadInstalled;
                }
                set
                {
                    this._IsCadInstalled = value;
                }
            }

            public bool IsCadInstalledIsNull
            {
                get
                {
                    return ((this._IsCadInstalled == null) || !(this._IsCadInstalled != null && this._IsCadInstalled.Length > 0));
                }
            }

            public System.String VendorName
            {
                get
                {
                    return this._VendorName;
                }
                set
                {
                    this._VendorName = value;
                }
            }

            public bool VendorNameIsNull
            {
                get
                {
                    return ((this._VendorName == null) || !(this._VendorName != null && this._VendorName.Length > 0));
                }
            }
            public System.String ApplicationMode
            {
                get
                {
                    return this._ApplicationMode;
                }
                set
                {
                    this._ApplicationMode = value;
                }
            }

            public bool ApplicationModeIsNull
            {
                get
                {
                    return ((this._ApplicationMode == null) || !(this._ApplicationMode != null && this._ApplicationMode.Length > 0));
                }
            }

            public System.String AgencyName
            {
                get
                {
                    return this._AgencyName;
                }
                set
                {
                    this._AgencyName = value;
                }
            }

            public bool AgencyNameIsNull
            {
                get
                {
                    return ((this._AgencyName == null) || !(this._AgencyName != null && this._AgencyName.Length > 0));
                }
            }

            public System.DateTime ServerStartTime
            {
                get
                {
                    return this._ServerStartTime;
                }
                set
                {
                    this._ServerStartTime = value;
                }
            }

            public bool ServerStartTimeIsNull
            {
                get
                {
                    return (this._ServerStartTime == System.DateTime.MinValue);
                }
            }

            public System.String MailServerType
            {
                get
                {
                    return this._MailServerType;
                }
                set
                {
                    this._MailServerType = value;
                }
            }

            public bool MailServerTypeIsNull
            {
                get
                {
                    return ((this._MailServerType == null) || !(this._MailServerType != null && this._MailServerType.Length > 0));
                }
            }

            public System.String Domain
            {
                get
                {
                    return this._Domain;
                }
                set
                {
                    this._Domain = value;
                }
            }

            public bool DomainIsNull
            {
                get
                {
                    return ((this._Domain == null) || !(this._Domain != null && this._Domain.Length > 0));
                }
            }

        //Added by saranya for CRM#64214


        //End of CRM#64214
    }
 
}
