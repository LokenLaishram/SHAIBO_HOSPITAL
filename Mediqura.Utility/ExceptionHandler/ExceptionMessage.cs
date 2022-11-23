using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.Utility.ExceptionHandler
{
    public class ExceptionMessage
    {
        private static readonly string ERROR_FILE_PATH = System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings.Get("ExceptionMsgFile");

        /// <summary>
        /// Function fetch exception message based on the ResourceID passed into it
        /// </summary>
        /// <param name="ResourceID">String of ResourceID</param>
        /// <returns></returns>
        public static string GetMessage(string ResourceID)
        {
            string retMessage = "";

            string xmlPath = ERROR_FILE_PATH;
            System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
            xmldoc.Load(xmlPath);
            System.Xml.XmlNode node = xmldoc.SelectSingleNode("//Resources//Resource[@ID='" + ResourceID + "']");
            if (node != null)
            {
                if ((node.InnerText.Length > 0))
                {
                    retMessage = node.InnerText.ToString();
                }
            }
            else
            {
                retMessage = "The message is not found for " + ResourceID + ". Please contact system administrator.";
            }
            return retMessage;
        }

        /// <summary>
        /// Function extract the errorcode form exception object and return the respective exception message
        /// </summary>
        /// <param name="ex">ex of System.Exception object</param>
        /// <returns></returns>
        public static string GetMessage(System.Exception ex)
        {
            string errorCode = "";
            Exception tempException = ex;
            while (tempException != null)
            {
                errorCode = tempException.Message;
                tempException = tempException.InnerException;
                if (errorCode != "")
                    break;
            }

            return GetMessage(errorCode);
        }

    }
}
