using KPI.Global.StateMachine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Phoenix.Object.EmailWatcher
{
    public static class EmailSplitterOperations
    {
        public static string EmailSubjectFilter(this string sOriginalSubject, string sKey, out bool blnValueAvailable, string SplitterifExists = "")
        {
            State.KPILog.Info("EmailSplitterOperations: EmailSubjectFilter()");
            blnValueAvailable = false;

            if (!string.IsNullOrWhiteSpace(sOriginalSubject) && !string.IsNullOrWhiteSpace(sKey) && sOriginalSubject.Contains(sKey))
            {
                sOriginalSubject = sOriginalSubject.Remove(0, sOriginalSubject.IndexOf(sKey) + sKey.Length + 1);
                string sSplitter = !string.IsNullOrEmpty(SplitterifExists) ? SplitterifExists : "";
                int iIndex = -1;
                if (sSplitter == "")
                {
                    sOriginalSubject = sOriginalSubject.TrimStart();
                    char[] chars = { ':', ';', ' ' };
                    iIndex = sOriginalSubject.IndexOfAny(chars);
                    if (iIndex > 0)
                    {
                        SplitterifExists = sOriginalSubject[iIndex].ToString();
                    }
                }
                string[] Value = sOriginalSubject.Split(new string[] { SplitterifExists }, StringSplitOptions.RemoveEmptyEntries);
                blnValueAvailable = true;
                return Value.Length > 0 ? Value[0] : sOriginalSubject;

            }
            return string.Empty;
        }
    }

    public static class EmailContentCreation
    {
        public const String Signature = " Thank you, <br/> ProPhoenix Corporation <br/> <br/> 502 Pleasant Valley Ave, <br/> Moorestown, NJ 08057. <br/>(609) 953-6850 Office <br/>(609) 953-5311 Fax ";
        public const String Image = "<img src='../Images/ProphoenixLogo.png'/>&nbsp; <img src='/Images/EzFireLogo.png' width='150px' height='60px'/>";
        public const String link = "<a  href = 'http://www.prophoenix.com/'>www.prophoenix.com</a> &nbsp; <a  href = 'http://www.ezfirerecords.com/'>www.ezfirerecords.com</a> ";
        //static string location = System.Reflection.Assembly.GetEntryAssembly().Location;
        //static string directoryPath = Path.GetDirectoryName(location);
       

        public static string HlpDeskUnknownUser( bool blnReplylineneeded )
        {
            string sSeparator = "<br/>";
            StringBuilder strBuilder = new StringBuilder();
            StringBuilder PlainTextFormation = new StringBuilder();
            strBuilder.Append("<html> ");
            strBuilder.Append("<body> <table> <tr> <td> ");
            strBuilder.Append("<br/>{BodyText}<br/> ");
        
            if (blnReplylineneeded)
            {
                strBuilder.Append("To close this Ticket, type “Closed”.&nbsp;&nbsp;To reopen, type “Reopen”<br/> ");
                strBuilder.Append("**&nbsp;&nbsp;Please do not alter the subject and/ or the line above&nbsp;&nbsp;**");
            }
            strBuilder.Append("</td> </tr> <tr><td style='color:#808080;' >{AppendServerDetails()}</td></tr> </table></body> </html>");

            return strBuilder.ToString();
        }


        public static string HlpDeskTicketCreated(bool blnReplylineneeded ,string sActivityHistory)
        {

            StringBuilder strBuilder = new StringBuilder();
           
            strBuilder.Append("<html> ");
            strBuilder.Append("<head> <title></title> </head>");
            strBuilder.Append("<body> <table> <tr> <td> ");
            strBuilder.Append("<br/></td></tr>");
            strBuilder.Append("<tr><td style='overflow:hidden'>");
            strBuilder.Append(" =====================================================================================================================================");
            strBuilder.Append("</td></tr><tr><td><br/>");
 
            if (blnReplylineneeded)
            {
                strBuilder.AppendFormat("<br/> To close this Ticket, type “Closed”.&nbsp;&nbsp;To reopen, type “Reopen”<br/> ");
                strBuilder.Append("**&nbsp;&nbsp;Please do not alter the subject and/ or the line above&nbsp;&nbsp;**");
            }
            strBuilder.Append("<tr><td>"+sActivityHistory+"</td></tr> ");
            strBuilder.Append("</td> </tr>  </table></body> </html>");
           
            return strBuilder.ToString();
        }


        public static string ToEncryptDecrypt(this String sValue, bool blnEncrypt)
        {
            StringBuilder sb = new StringBuilder(sValue);
            if (!string.IsNullOrEmpty(sValue))
            {
                /*IPhone have non supported chars ,to avoid issue it is converted nad reconverted  */
                Dictionary<string, string> dict = new Dictionary<string, string> {
                    { "&","^and^" },
                    { "~", "^tld^"},
                    {"`", "^quot^" },
                    { "!", "^exlm^" },
                    {"@", "^adrt^" },
                     { "#","^hsh^" },
                    { "$", "^dlr^"},
                    {"%", "^pct^" },
                    { "*", "^str^" },
                    {"(", "^lftbr^" },
                     { ")","^rytbr^" },
                    {"-", "^mins^" },
                    { "+", "^pls^" },
                    {"{", "^lbrc^" },
                    { "}","^rbrc^" },
                    { "[", "^lbx^"},
                    {"]", "^rbx^" },
                    { "|", "^orc^" },
                    {"\\", "^bcksls^" },
                     { ":","^coln^" },
                    { ";", "^semcoln^"},
                    {"”", "^qut^" },
                    { "’", "^rcdn^" },
                    {"<", "^lesdn^" },
                     { ">","^grtdn^" },
                    { "?", "^qmark^"},
                    {"/", "^frsls^" },
                    { "\r", "^r^"},
                    {"\n", "^n^" }

                };

                foreach (var item in dict)
                {
                    if (blnEncrypt)
                    {
                        sb.Replace(item.Key, item.Value);
                    }
                    else
                    {
                        sb.Replace(item.Value, item.Key);
                    }
                }
            }
            return sb.ToString(); ;
        }

        public static string RenameSpecialChars(this string content)
        {

            return content;
        }

        public static string bodyMessageFinder(this String sbodyContent,string sEmailContent)
        {
            State.KPILog.Info("bodyMessageFinder()");

            #region If Body Content exist
            if (!string.IsNullOrWhiteSpace(sbodyContent))
            {
                State.KPILog.Info("If: Body content exist");
                try
                {
                    sbodyContent = sbodyContent.ToEncryptDecrypt(true);
                    
                    ASCIIEncoding ascii = new ASCIIEncoding();
                    Byte[] bytes = ascii.GetBytes(sbodyContent);// All Unknown charcters will get converted here
                    String decoded = ascii.GetString(bytes);
                    sbodyContent = decoded.ToEncryptDecrypt(false); 
                }
                catch(Exception objERR)
                {
                    State.KPILog.Error("Error: While Body Content conversion - " + Convert.ToString(objERR));
                }
                sbodyContent = sbodyContent.Replace("\r", "^r^").Replace("\n", "^n^");

                string sKeyAvailable = string.Empty;

                #region If EMail Content exist
                if (!string.IsNullOrEmpty(sEmailContent))
                {
                    State.KPILog.Info("If: EMail content exist");

                    List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>
                    {
                        { new KeyValuePair<int, string>(sbodyContent.IndexOf("^r^^n^** " + sEmailContent, StringComparison.OrdinalIgnoreCase), sEmailContent)},
                    };

                    int iMin = list.Select(s => s.Key).Where(index => index != -1).OrderBy(s => s).FirstOrDefault();
                    if (iMin > 0)
                    {
                        sbodyContent = iMin == sbodyContent.Length - 1 ? sbodyContent : sbodyContent.Substring(0, iMin);
                    }
                }
                #endregion

                int Maxindex = GetMinindexForMaxBodyFilter(sbodyContent, out sKeyAvailable);

                Maxindex = Maxindex > 0 ? Maxindex : sbodyContent.Length - 1;
                if (Maxindex > 0 || Maxindex== sbodyContent.Length - 1)
                {
                    sbodyContent = Maxindex == sbodyContent.Length - 1 ? sbodyContent: sbodyContent.Substring(0, Maxindex);

                    //Remove From:
                    sbodyContent = sbodyContent.RemoveMiddleString("^r^^n^From", "^r^^n^Sent:");
                    sbodyContent = sbodyContent.RemoveMiddleString("^r^^n^Sent:", "^r^^n^To");
                    sbodyContent = sbodyContent.RemoveMiddleString("^r^^n^Sent:", "^r^^n^CC");
                    sbodyContent = sbodyContent.RemoveMiddleString("^r^^n^Sent:", "^r^^n^BCC");

                    sbodyContent = sbodyContent.RemoveMiddleString("^r^^n^To:", "^r^^n^CC");
                    sbodyContent = sbodyContent.RemoveMiddleString("^r^^n^To:", "^r^^n^BCC");

                    sbodyContent = sbodyContent.RemoveMiddleString("^r^^n^CC:", "^r^^n^BCC");

                    sbodyContent = sbodyContent.RemoveMiddleString("^r^^n^To", "^r^^n^Subject");
                    sbodyContent = sbodyContent.RemoveMiddleString("^r^^n^CC", "^r^^n^Subject");
                    sbodyContent = sbodyContent.RemoveMiddleString("^r^^n^BCC", "^r^^n^Subject");

                    sbodyContent = sbodyContent.RemoveMiddleString("^r^^n^Subject:", "^r^^n^");
                    sbodyContent = sbodyContent.Replace("**", "");
                    if (!string.IsNullOrEmpty(sKeyAvailable))
                    {
                        sbodyContent = sbodyContent.RemoveMiddleString("^r^^n^==", sKeyAvailable).Replace(sKeyAvailable, "");

                    }
                    sbodyContent = RemoveAddtionalOptions(sbodyContent);
                }
                sbodyContent = sbodyContent.Replace("^r^", "\r").Replace("^n^", "\n");
                return sbodyContent.Replace("^r^", "\r").Replace("^n^", "\n").Replace("==", "").Trim();

            }
            #endregion

            return string.Empty;
        }

        public static int GetMinindexForMaxBodyFilter(string sbodyContent, out string sKeyAvailable)
        {
            State.KPILog.Info("GetMinindexForMaxBodyFilter()");

            List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>
            {
                 { new KeyValuePair<int, string>(sbodyContent.IndexOf("^r^^n^==", StringComparison.OrdinalIgnoreCase), "^r^^n^==") },
                { new KeyValuePair<int, string>(sbodyContent.IndexOf("^n^## Reply", StringComparison.OrdinalIgnoreCase), "^r^^n^## Reply")},
                { new KeyValuePair<int, string>(sbodyContent.IndexOf("^n^** To close this Ticket, type “Closed”.", StringComparison.OrdinalIgnoreCase),"^n^** To close this Ticket, type “Closed”.") },
               { new KeyValuePair<int, string>(sbodyContent.IndexOf("^n^To close this Ticket, type “Closed”.", StringComparison.OrdinalIgnoreCase), "^n^To close this Ticket, type “Closed”.") },
                { new KeyValuePair<int, string>(sbodyContent.IndexOf("^n^** Please do not alter", StringComparison.OrdinalIgnoreCase), "^n^** Please do not alter")},
                { new KeyValuePair<int, string>(sbodyContent.IndexOf("## Reply above this line to send an update to this Ticket ##", StringComparison.OrdinalIgnoreCase), "## Reply above this line to send an update to this Ticket ##")},
                { new KeyValuePair<int, string>(sbodyContent.IndexOf("To close this Ticket, type “Closed”.To reopen, type “Reopen”", StringComparison.OrdinalIgnoreCase), "To close this Ticket, type “Closed”. To reopen, type “Reopen”")},
                { new KeyValuePair<int, string>(sbodyContent.IndexOf("**Please do not alter the subject and/ or the line above**", StringComparison.OrdinalIgnoreCase), "**Please do not alter the subject and/ or the line above**")},
                      { new KeyValuePair<int, string>(sbodyContent.IndexOf("**&nbsp;&nbsp;Please do not alter the subject and/ or the line above&nbsp;&nbsp;**", StringComparison.OrdinalIgnoreCase), "**&nbsp;&nbsp;Please do not alter the subject and/ or the line above&nbsp;&nbsp;**")},
                { new KeyValuePair<int, string>(sbodyContent.IndexOf("To close this Ticket, type “Closed”.&nbsp;&nbsp;To reopen, type “Reopen”", StringComparison.OrdinalIgnoreCase), "To close this Ticket, type “Closed”.&nbsp;&nbsp;To reopen, type “Reopen”")},
                       { new KeyValuePair<int, string>(sbodyContent.IndexOf("^n^To close this Ticket, type ?Closed?. To reopen, type ?Reopen?", StringComparison.OrdinalIgnoreCase), "^n^To close this Ticket, type ?Closed?. To reopen, type ?Reopen?")},
                        { new KeyValuePair<int, string>(sbodyContent.IndexOf("To close this Ticket, type ", StringComparison.OrdinalIgnoreCase), "To close this Ticket, type ")},
            };

            int iMin = list.Select(s => s.Key).Where(index => index != -1).OrderBy(s => s).FirstOrDefault();
            sKeyAvailable = (from val in list where val.Key == iMin select val.Value).SingleOrDefault();

            return !string.IsNullOrEmpty(sKeyAvailable) ? iMin : -1;
        }

        public static string RemoveMiddleString(this string Source, string sFromKey, string sToKey)
        {
            if (!string.IsNullOrWhiteSpace(Source))
            {

                int iFromKeyIndex = Source.IndexOf(sFromKey, StringComparison.OrdinalIgnoreCase);
                if (iFromKeyIndex + sFromKey.Length <= Source.Length)
                {
                    int iToKeyIndex = Source.IndexOf(sToKey, iFromKeyIndex + sFromKey.Length, StringComparison.OrdinalIgnoreCase);

                    if (iFromKeyIndex != -1 && iToKeyIndex > iFromKeyIndex)
                    {
                        if (iFromKeyIndex > 1)
                        {
                            Source = Source.Substring(0, iFromKeyIndex) + Source.Substring(iToKeyIndex);
                        }
                        else
                        {
                            Source = Source.Substring(iToKeyIndex);

                        }
                    }
                }
            }
            return Source;
        }

        private static string RemoveAddtionalOptions(string sbodyContent)
        {
            State.KPILog.Info("RemoveAddtionalOptions()");
            try
            {
                string sModifiedString = sbodyContent;

                int MobileReplycontentIndex = sModifiedString.IndexOf("Sent from", StringComparison.OrdinalIgnoreCase);
                if (MobileReplycontentIndex == -1)
                {
                    return sbodyContent;
                }

                int WroteIndex = sModifiedString.IndexOf("wrote:", MobileReplycontentIndex, StringComparison.OrdinalIgnoreCase);

                if (WroteIndex == -1)
                {
                    return sbodyContent;
                }

                int DateFieldsIndex = sModifiedString.IndexOf("On", MobileReplycontentIndex, StringComparison.OrdinalIgnoreCase);

                if (DateFieldsIndex == -1)
                {
                    return sbodyContent;
                }

                int MailStartIndex = sModifiedString.IndexOf("<", DateFieldsIndex, StringComparison.OrdinalIgnoreCase);
                if (MailStartIndex == -1)
                {
                    return sbodyContent;
                }
                int MailEndIndex = sModifiedString.IndexOf(">", MailStartIndex, StringComparison.OrdinalIgnoreCase);

                if (MailEndIndex == -1)
                {
                    return sbodyContent;
                }

                if (MobileReplycontentIndex != -1 && WroteIndex != -1 && MobileReplycontentIndex < WroteIndex)
                {
                    if (DateFieldsIndex != -1 && MailStartIndex != -1 && MailEndIndex != -1)
                    {
                        if (DateFieldsIndex < WroteIndex && DateFieldsIndex > MobileReplycontentIndex)
                        {
                            if (MailStartIndex < MailEndIndex && MailEndIndex < WroteIndex)
                            {

                                string sFinalContent2 = sModifiedString.Substring(WroteIndex + "wrote:".Length).Replace("?", "").Replace("<", "").Replace(">", "");
                                string sFinalContent1 = sModifiedString.Substring(0, MobileReplycontentIndex);
                                sModifiedString = sFinalContent1 + sFinalContent2;
                                if (!string.IsNullOrWhiteSpace(sModifiedString))
                                {
                                    return sModifiedString.Trim();
                                }
                            }
                        }
                    }
                }
            }
            catch 
            {
                

            }
            return sbodyContent;
        }
    }
}
