using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Mediqura.CommonData.Common;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.NetworkInformation;
using Zen.Barcode;
using System.IO;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Drawing;
using QRCoder;
using System.Xml;
using CrystalDecisions.CrystalReports.Engine;
using Mediqura.CommonData.PatientData;
using Mediqura.BOL.PatientBO;
using Saplin.Controls;
using Mediqura.CommonData.LoginData;
using Mediqura.BOL.UserBO;
using Mediqura.CommonData.MedBillData;
using Mediqura.BOL.MedBillBO;
using System.Security.Cryptography;
using System.Text;
using Amazon.CodePipeline.Model;

namespace Mediqura.Web.MedCommon
{
    public class Commonfunction
    {
        public static bool isValidDate(string input)
        {
            string[] ValidDate = { "dd/MM/yyyy", "dd-MM-yyyy" };
            DateTime outTime; bool ValidateTime;
            ValidateTime = DateTime.TryParseExact(input, ValidDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out outTime);
            return ValidateTime;
        }
        public static bool isValidTime(string input)
        {
            DateTime outTime; bool ValidateTime;
            ValidateTime = DateTime.TryParseExact(input, "hh:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out outTime);
            return ValidateTime;
        }
        public static bool ChecklowerTime(string input)
        {
            bool InValidateTime;
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime Date = isValidTime(input) ? DateTime.Parse(input.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault) : System.DateTime.Today;
            DateTime Today = Convert.ToDateTime(System.DateTime.Now.ToString("hh:mm:ss tt"));
            string ADADTE = Date.ToString("hh:mm:ss tt");
            string Todays = Today.ToString("hh:mm:ss tt");
            InValidateTime = isValidTime(input) == false ? true : Convert.ToDateTime(ADADTE) <= Convert.ToDateTime(Todays) ? true : false;
            return InValidateTime;
        }
        public static bool isValidTodayDate(string input)
        {
            string[] ValidDate = { "dd/MM/yyyy", "dd-MM-yyyy" };
            DateTime outTime; bool ValidateTime;
            ValidateTime = DateTime.TryParseExact(input, ValidDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out outTime);
            return ValidateTime;
        }
        public static bool CheckOverDate(string input)
        {
            bool InValidateTime;
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime Date = isValidDate(input) ? DateTime.Parse(input.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault) : System.DateTime.Today;
            DateTime Today = Convert.ToDateTime(System.DateTime.Today);
            string ADADTE = Date.ToString("yyyy-MM-dd");
            string Todays = Today.ToString("yyyy-MM-dd");
            InValidateTime = isValidDate(input) == false ? true : Convert.ToDateTime(ADADTE) > Convert.ToDateTime(Todays) ? true : false;
            return InValidateTime;
        }
        public static bool ChecklowerDate(string input)
        {
            bool InValidateTime;
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime Date = isValidDate(input) ? DateTime.Parse(input.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault) : System.DateTime.Today;
            DateTime Today = Convert.ToDateTime(System.DateTime.Today);
            string ADADTE = Date.ToString("yyyy-MM-dd");
            string Todays = Today.ToString("yyyy-MM-dd");
            InValidateTime = isValidDate(input) == false ? true : Convert.ToDateTime(ADADTE) <= Convert.ToDateTime(Todays) ? true : false;
            return InValidateTime;
        }
        public static bool ChecklowerAppointmentDate(string input)
        {
            bool InValidateTime;
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime Date = isValidDate(input) ? DateTime.Parse(input.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault) : System.DateTime.Today;
            DateTime Today = Convert.ToDateTime(System.DateTime.Today);
            string ADADTE = Date.ToString("yyyy-MM-dd");
            string Todays = Today.ToString("yyyy-MM-dd");
            InValidateTime = isValidDate(input) == false ? true : Convert.ToDateTime(ADADTE) < Convert.ToDateTime(Todays) ? true : false;
            return InValidateTime;
        }
        public static bool Checkvalidmobile(string input)
        {
            bool validmobile;
            string pattern = null;
            pattern = "^([7-9]{1})([0-9]{1})([0-9]{8})$";
            validmobile = Regex.IsMatch(input.Trim(), pattern) ? true : false;
            return validmobile;
        }
        public static bool Checkvalidpin(string input)
        {
            bool validpin;
            string pattern = null;
            pattern = "[0-9]{5}";
            validpin = Regex.IsMatch(input.Trim(), pattern) ? true : false;
            return validpin;
        }
        public static bool Checkemail(string input)
        {
            bool validemail;
            string pattern = null;
            pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            validemail = Regex.IsMatch(input.Trim(), pattern) ? true : false;
            return validemail;
        }
        public static bool Clear_PHR_Uncompletetransactions(int FinancialYearID, Int64 EmployeeID, int SubtockID)
        {
            LogData ObjLogdata = new LogData();
            UserBO LogdataBO = new UserBO();
            ObjLogdata.FinancialYearID = FinancialYearID;
            ObjLogdata.EmployeeID = EmployeeID;
            ObjLogdata.MedSubStockID = SubtockID;
            bool result = LogdataBO.ClearuncompleteTransactions(ObjLogdata);
            return result;
        }
        public static Int32 SemicolonSeparation_String_32(string SourceString)
        {
            try
            {
                if (SourceString.Contains(":") && SourceString.Substring(SourceString.LastIndexOf(':') + 1).All(char.IsDigit) == true)
                {
                    Int32 x = Convert.ToInt32(SourceString.Substring(SourceString.LastIndexOf(':') + 1));
                    return x;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }
        public static Int64 SemicolonSeparation_String_64(string SourceString)
        {
            try
            {
                if (SourceString.Contains(":") && SourceString.Substring(SourceString.LastIndexOf(':') + 1).All(char.IsDigit) == true)
                {
                    Int64 x = Convert.ToInt64(SourceString.Substring(SourceString.LastIndexOf(':') + 1));
                    return x;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }
        public static IPAddress GetIPAddress(string hostName)
        {
            Ping ping = new Ping();
            var replay = ping.Send(hostName);

            if (replay.Status == IPStatus.Success)
            {
                return replay.Address;
            }
            return null;
        }
        public static bool validateZip(String text)
        {
            return !Regex.Match(text, @"^\d{3}\s?\d{3}$").Success;

        }
        public static int CheckDoctorAvail(int DeptID, Int64 DoctID)
        {
            OPdoctoravailable objdoctor = new OPdoctoravailable();
            OPDbillingBO objstdBO = new OPDbillingBO();
            objdoctor.DoctorID = DoctID;
            objdoctor.DepartmentID = DeptID;
            return objstdBO.ChecDoctorAvailability(objdoctor);
        }
        public static string getBarcode(string code)
        {
            BarcodeSymbology s = BarcodeSymbology.Code128;
            BarcodeDraw drawObject = BarcodeDrawFactory.GetSymbology(s);
            var metrics = drawObject.GetDefaultMetrics(60);
            metrics.Scale = 2;
            var barcodeImage = drawObject.Draw(code, metrics);

            using (MemoryStream ms = new MemoryStream())
            {
                barcodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                return "data:image/png;base64," + Convert.ToBase64String(imageBytes);
            }
        }
        public static int CheckMSBDoctor(Int64 DoctID)
        {
            OPdoctoravailable objdoctor = new OPdoctoravailable();
            OPDbillingBO objstdBO = new OPDbillingBO();
            return objstdBO.checkMsbDoctor(DoctID);
        }
        public static byte[] getBarcodeImage(string code)
        {
            BarcodeSymbology s = BarcodeSymbology.Code128;
            BarcodeDraw drawObject = BarcodeDrawFactory.GetSymbology(s);
            var metrics = drawObject.GetDefaultMetrics(60);
            metrics.Scale = 2;
            var barcodeImage = drawObject.Draw(code, metrics);

            using (MemoryStream ms = new MemoryStream())
            {
                barcodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                return imageBytes;
            }
        }
        public static string getQR(string code)
        {
            BarcodeSymbology s = BarcodeSymbology.CodeQr;
            BarcodeDraw drawObject = BarcodeDrawFactory.GetSymbology(s);
            var metrics = drawObject.GetDefaultMetrics(60);
            metrics.Scale = 2;
            var barcodeImage = drawObject.Draw(code, metrics);

            using (MemoryStream ms = new MemoryStream())
            {
                barcodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                return "data:image/png;base64," + Convert.ToBase64String(imageBytes);
            }
        }
        public static byte[] getQRImage(string code)
        {

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            imgBarCode.Height = 50;
            imgBarCode.Width = 50;
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();

                    return byteImage;
                }
            }
        }
        public static bool isValidURL(string url)
        {
            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Timeout = 2000;
            WebResponse webResponse;
            try
            {
                webResponse = webRequest.GetResponse();
            }
            catch //If exception thrown then couldn't get response from address
            {
                return false;
            }
            webResponse.Close();
            return true;
        }
        public static PatientQRdata ReadPatientQR(string xml)
        {
            PatientQRdata objData = new PatientQRdata();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/PATIENTDATA");
                foreach (XmlNode node in nodeList)
                {
                    objData.UHID = Convert.ToInt64(node.SelectSingleNode("UHID").InnerText);
                    objData.Name = node.SelectSingleNode("NAME").InnerText;
                    objData.DOB = node.SelectSingleNode("DOB").InnerText;
                    objData.Gender = node.SelectSingleNode("GENDER").InnerText;
                    objData.Address = node.SelectSingleNode("ADDRESS").InnerText;
                    objData.ContactNo = node.SelectSingleNode("CONTACTNO").InnerText;
                    if (node.SelectSingleNode("IPNO") != null)
                    {
                        objData.IPNo = node.SelectSingleNode("IPNO").InnerText;
                    }

                }
            }
            catch
            {
                objData = null;
            }

            return objData;
        }
        public static String CalculateY(String DOB) // in xxx.xxxxx
        {
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime DateofBirth = DateTime.Parse(DOB, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime Now = DateTime.Now;
            int age = new DateTime(DateTime.Now.Subtract(DateofBirth).Ticks).Year - 1;
            DateTime PastYearDate = DateofBirth.AddYears(age);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            string Y = age.ToString();
            return Y;
        }
        public static String CalculateM(String DOB) // in xxx.xxxxx
        {
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime DateofBirth = DateTime.Parse(DOB, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime Now = DateTime.Now;
            int age = new DateTime(DateTime.Now.Subtract(DateofBirth).Ticks).Year - 1;
            DateTime PastYearDate = DateofBirth.AddYears(age);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            string M = Months.ToString();

            return M;

        }
        public static String CalculateD(String DOB) // in xxx.xxxxx
        {
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime DateofBirth = DateTime.Parse(DOB, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime Now = DateTime.Now;
            int age = new DateTime(DateTime.Now.Subtract(DateofBirth).Ticks).Year - 1;
            DateTime PastYearDate = DateofBirth.AddYears(age);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            String D = Now.Subtract(PastYearDate.AddMonths(Months)).Days.ToString();
            return D;

        }
        public static int UpdatePatientAge(String DOB, Int64 UHID) // in xxx.xxxxx
        {
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);

            PatientAgeData obj = new PatientAgeData();
            RegistrationBO objpatBO = new RegistrationBO();
            obj.UHID = UHID;
            List<PatientAgeData> result = objpatBO.GetpatientDOB(obj);
            if (result.Count > 0)
            {
                DOB = result[0].DOB.ToString("dd/MM/yyyy");
            }

            DateTime DateofBirth = DateTime.Parse(DOB, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime Now = DateTime.Now;
            int age = new DateTime(DateTime.Now.Subtract(DateofBirth).Ticks).Year - 1;
            DateTime PastYearDate = DateofBirth.AddYears(age);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }

            Int32 Age = age;
            int Month = Months;
            Int32 Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            return objpatBO.UpdatePatientAge(Age, Month, Days, UHID);

        }
        public static String TallyResponse(string response)
        {
            int create = 0;
            int altered = 0;
            String msg = "";
            string errors = "";
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(response);
                XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/RESPONSE");
                foreach (XmlNode node in nodeList)
                {
                    create = Convert.ToInt32(node.SelectSingleNode("CREATED").InnerText);
                    altered = Convert.ToInt32(node.SelectSingleNode("ALTERED").InnerText);
                    errors = node.SelectSingleNode("ERRORS").InnerText;
                    if (create > 0)
                    {
                        msg = msg + create + " Saved \n";
                    }
                    if (altered > 0)
                    {
                        msg = msg + altered + " Updated \n";
                    }
                    if (errors != "0")
                    {
                        errors = node.SelectSingleNode("LINEERROR").InnerText;
                        msg = msg + " ERROR: " + errors;
                    }

                }
            }
            catch
            {
                msg = "Somthing went wrong!";
            }

            return msg;
        }
        public static int SavePatientDocuments(PatientCaseHistoryData objdata, ReportDocument report)
        {
            MemoryStream memStream = (MemoryStream)report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            objdata.FileName = objdata.ServiceName + "_" + objdata.CaseNo;
            objdata.Extension = "pdf";
            //objdata.FileData = memStream.ToArray();
            PatientCaseHistoryBO objBO = new PatientCaseHistoryBO();
            return objBO.SavePatientDocuments(objdata);
        }
        public static string GetClientIPAddress()
        {
            string clientIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            //clientIPAddress = "192.168.0.102";
            return clientIPAddress;
        }
        #region LookUps DropDowns
        public static void PopulateDdl(DropDownList drpDownList, List<LookupItem> lstLookups)
        {
            if (lstLookups == null) lstLookups = new List<LookupItem>();
            if (drpDownList != null && lstLookups != null)
            {
                drpDownList.DataSource = lstLookups;
                drpDownList.DataValueField = "ItemId";
                drpDownList.DataTextField = "ItemName";
                drpDownList.DataBind();
                drpDownList.Items.Insert(0, "-- Select --");
                drpDownList.Items[0].Value = "0";
                drpDownList.ClearSelection();
                //drpDownList.Items[0].Attributes["disabled"] = "disabled";
            }
        }
        public static void Populatechkbox(ListBox listbox, List<LookupItem> lstLookups)
        {
            if (lstLookups == null) lstLookups = new List<LookupItem>();
            if (listbox != null && lstLookups != null)
            {
                listbox.DataSource = lstLookups;
                listbox.DataValueField = "ItemId";
                listbox.DataTextField = "ItemName";
                listbox.DataBind();
                listbox.ClearSelection();
                //drpDownList.Items[0].Attributes["disabled"] = "disabled";
            }
        }
        //---------DROP DPWN FOR TIME --------------------------//
        public static void PopulateDdlHour(DropDownList drpDownHourList, List<LookupItem> lstLookups)
        {
            if (lstLookups == null) lstLookups = new List<LookupItem>();
            if (drpDownHourList != null && lstLookups != null)
            {
                drpDownHourList.DataSource = lstLookups;
                drpDownHourList.DataValueField = "ItemId";
                drpDownHourList.DataTextField = "ItemName";
                drpDownHourList.DataBind();
                drpDownHourList.Items.Insert(0, "Hr");
                drpDownHourList.Items[0].Value = "0";
                drpDownHourList.ClearSelection();
                //drpDownList.Items[0].Attributes["disabled"] = "disabled";
            }
        }
        public static void PopulateDdlMinute(DropDownList drpDownMinuteList, List<LookupItem> lstLookups)
        {
            if (lstLookups == null) lstLookups = new List<LookupItem>();
            if (drpDownMinuteList != null && lstLookups != null)
            {
                drpDownMinuteList.DataSource = lstLookups;
                drpDownMinuteList.DataValueField = "ItemId";
                drpDownMinuteList.DataTextField = "ItemName";
                drpDownMinuteList.DataBind();
                drpDownMinuteList.Items.Insert(0, "MM");
                drpDownMinuteList.Items[0].Value = "0";
                drpDownMinuteList.ClearSelection();
                //drpDownList.Items[0].Attributes["disabled"] = "disabled";
            }
        }
        //---------Checkbox dropdown--------------------------------//
        public static void PopulateCheckbox(DropDownCheckBoxes checkboxList, List<LookupItem> lstLookups)
        {
            if (lstLookups == null) lstLookups = new List<LookupItem>();
            if (checkboxList != null && lstLookups != null)
            {
                checkboxList.DataSource = lstLookups;
                checkboxList.DataValueField = "ItemId";
                checkboxList.DataTextField = "ItemName";
                checkboxList.DataBind();
                checkboxList.ClearSelection();

            }
        }
        public static void DisableDropdownList(DropDownList drpDownList)
        {
            foreach (ListItem item in drpDownList.Items)
            {
                item.Attributes.Add("disabled", "disabled");

            }
        }
        public static void EnableDropdownList(DropDownList drpDownList)
        {
            drpDownList.Items[0].Attributes["disabled"] = "disabled";

        }
        public static void Insertzeroitemindex(DropDownList drpDownList)
        {
            drpDownList.Items.Clear();
            drpDownList.Items.Insert(0, "-- Select --");
            drpDownList.Items[0].Value = "0";
        }
        #endregion
        #region Hour-Min-AMPM-Day DropDowns
        public static void PopulateDdlHour(DropDownList drpDownList)
        {
            drpDownList.Items.Add(new ListItem("HH", "00"));
            for (int i = 1; i <= 12; i++)
            {
                string t = i.ToString();
                if (t.Length == 1) t = "0" + t;
                drpDownList.Items.Add(new ListItem(t, t));
            }
        }
        public static void PopulateDdlMinute(DropDownList drpDownList)
        {
            drpDownList.Items.Add(new ListItem("MM", "00"));
            for (int i = 1; i <= 60; i++)
            {
                string t = i.ToString();
                if (t.Length == 1) t = "0" + t;
                drpDownList.Items.Add(new ListItem(t, t));
            }
        }
        public static void PopulateDdlAMPM(DropDownList drpDownList)
        {
            //drpDownList.Items.Add(new ListItem("AM/PM", "AM/PM"));
            drpDownList.Items.Add(new ListItem("AM", "AM"));
            drpDownList.Items.Add(new ListItem("PM", "PM"));
        }
        public static void PopulateDdlDays(DropDownList drpDownList)
        {
            drpDownList.Items.Add(new ListItem("--Select Day--", "0"));
            drpDownList.Items.Add(new ListItem("Sunday", "1"));
            drpDownList.Items.Add(new ListItem("Monday", "2"));
            drpDownList.Items.Add(new ListItem("Tuesday", "3"));
            drpDownList.Items.Add(new ListItem("Wednesday", "4"));
            drpDownList.Items.Add(new ListItem("Thursday", "5"));
            drpDownList.Items.Add(new ListItem("Friday", "6"));
            drpDownList.Items.Add(new ListItem("Saturday", "7"));
        }
        public static String Getrounding(String str) // in xxx.xxxxx
        {
            try
            {
                decimal number;
                Decimal.TryParse(str, out number);
                return Decimal.Round(number, 2).ToString(); // in xxx.xx format
            }
            catch
            {
                return "0.00"; // in 0.00 format if invalid number is passed
            }
        }
        public static String Getroundingo(String str) // in xxx.xxxxx
        {
            try
            {
                decimal number;
                Decimal.TryParse(str, out number);
                return Decimal.Round(number, 0).ToString(); // in xxx.xx format
            }
            catch
            {
                return "0"; // in 0.00 format if invalid number is passed
            }
        }
        #endregion
        string EncryptionKey;
        private static string GenerateKey(int iKeySize)
        {
            RijndaelManaged aesEncryption = new RijndaelManaged();
            aesEncryption.KeySize = iKeySize;
            aesEncryption.BlockSize = 128;
            aesEncryption.Mode = CipherMode.CBC;
            aesEncryption.Padding = PaddingMode.PKCS7;
            aesEncryption.GenerateIV();
            string ivStr = Convert.ToBase64String(aesEncryption.IV);
            aesEncryption.GenerateKey();
            string keyStr = Convert.ToBase64String(aesEncryption.Key);
            string completeKey = ivStr + "," + keyStr;
            return Convert.ToBase64String(ASCIIEncoding.UTF8.GetBytes(completeKey));
        }
        public string Encrypt(string clearText)
        {
            EncryptionKey = "MAKV2SPBNI99212$&#(!";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public string Decrypt(string cipherText)
        {
            EncryptionKey = "MAKV2SPBNI99212$&#(!";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

    }

    public static class MyExtensionClass
    {
        public static string ToFormat12h(this DateTime dt)
        {
            return dt.ToString("yyyy/MM/dd, hh:mm:ss tt");
        }

        public static string ToFormat24h(this DateTime dt)
        {
            return dt.ToString("yyyy/MM/dd, HH:mm:ss");
        }
    }

 

}