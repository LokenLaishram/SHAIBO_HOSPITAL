using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Mediqura.Utility.Util
{
    public static class ObjectXMLSerializer<T> where T : class
    {
        #region Load methods

        /// <summary>
        /// Loads an object from an  DataTable 
        /// </summary>
        /// <example>
        /// <code>        
        /// </code>
        /// </example>
        /// <param name="path">DataTable  object to load the object from.</param>
        /// <returns>Object loaded from a DataTable in Document format.</returns>
        public static T Load(DataTable objTable)
        {
            StringWriter writer = new StringWriter();
            objTable.WriteXml(writer);

            string xml = writer.ToString();
            writer.Close();

            T serializableObject;

            // xml = @"C:\Documents and Settings\vikas.sharma\Desktop\TEST\test2.xml";
            // serializableObject = LoadFromXML(xml, null);

            serializableObject = LoadFromXML(xml);
            return serializableObject;
        }

        public static SqlParameter[] PopulateParams(T obj, string sp_name, string connectionString)
        {
            //Call a Procedure to know the parameters.
            string connectionStringForDB = connectionString;
            SqlConnection connectionSql = new SqlConnection(connectionStringForDB);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_sproc_columns";
            cmd.Parameters.Add("@procedure_name", SqlDbType.NVarChar).Value = sp_name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connectionSql;
            cmd.Connection.Open();

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            SqlParameter[] paramsSql = new SqlParameter[ds.Tables[0].Rows.Count];

            if (ds.Tables[0].Rows.Count > 0)
            {

                int intIncrement = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    try
                    {
                        paramsSql[intIncrement] = new SqlParameter();
                        paramsSql[intIncrement].ParameterName = dr["COLUMN_NAME"].ToString();
                    }
                    catch (Exception aObjExp)
                    {
                        string emes = aObjExp.Message;
                    }
                    intIncrement++;
                }
            }

            //Now iterate through the Properties..
            //get all the Properties of T.
            Type theType = typeof(T);
            // get all the members
            PropertyInfo[] propertyInfo = theType.GetProperties();

            for (int i = 0; i < paramsSql.Length; i++)
            {
                foreach (PropertyInfo pInfo in propertyInfo)
                {

                    if (pInfo.CanRead)
                    {
                        string ParameterNameOfStoredProcedure = paramsSql[i].ParameterName;
                        if (ParameterNameOfStoredProcedure.Replace("@", "").Trim().Equals(pInfo.Name, StringComparison.CurrentCultureIgnoreCase))
                        {
                            try
                            {
                                if (pInfo.GetValue(obj, null) != null)
                                {
                                    paramsSql[i].Value = pInfo.GetValue(obj, null).ToString();
                                }
                            }
                            catch (Exception aObjExp) { string exception = aObjExp.Message; }
                        }
                    }
                }

            }

            return paramsSql;


        }

        public static T Load(string xml)
        {

            T serializableObject = LoadFromXML(xml);

            return serializableObject;
        }

        #endregion
        #region Save methods

        public static string GetXML(T serializableObject)
        {
            return SaveToXML(serializableObject);
        }

        /// <summary>
        /// Saves an object to an XML file in Document format.
        /// </summary>
        /// <example>
        /// <code>        
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// ObjectXMLSerializer&lt;SerializableObject&gt;.Save(serializableObject, @"C:\XMLObjects.xml");
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="path">Path of the file to save the object to.</param>
        public static void Save(T serializableObject, string path)
        {
            SaveToXML(serializableObject, null, path, null);
        }


        /// <summary>
        /// Saves an object to an XML file in Document format, located in a specified isolated storage area.
        /// </summary>
        /// <example>
        /// <code>        
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// ObjectXMLSerializer&lt;SerializableObject&gt;.Save(serializableObject, "XMLObjects.xml", IsolatedStorageFile.GetUserStoreForAssembly());
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="fileName">Name of the file in the isolated storage area to save the object to.</param>
        /// <param name="isolatedStorageDirectory">Isolated storage area directory containing the XML file to save the object to.</param>
        public static void Save(T serializableObject, string fileName, IsolatedStorageFile isolatedStorageDirectory)
        {
            SaveToXML(serializableObject, null, fileName, isolatedStorageDirectory);
        }



        #endregion
        #region Private

        private static FileStream CreateFileStream(IsolatedStorageFile isolatedStorageFolder, string path)
        {
            FileStream fileStream = null;

            if (isolatedStorageFolder == null)
                fileStream = new FileStream(path, FileMode.OpenOrCreate);
            else
                fileStream = new IsolatedStorageFileStream(path, FileMode.OpenOrCreate, isolatedStorageFolder);

            return fileStream;
        }


        private static TextReader CreateTextReader(IsolatedStorageFile isolatedStorageFolder, string path)
        {
            TextReader textReader = null;

            if (isolatedStorageFolder == null)
                textReader = new StreamReader(path);
            else
                textReader = new StreamReader(new IsolatedStorageFileStream(path, FileMode.Open, isolatedStorageFolder));

            return textReader;
        }

        private static TextReader CreateTextReader(string path)
        {
            TextReader textReader = null;
            textReader = new StreamReader(path);
            return textReader;
        }

        //private static TextWriter CreateTextWriter(IsolatedStorageFile isolatedStorageFolder, string path)
        //{
        //    TextWriter textWriter = null;

        //    if (isolatedStorageFolder == null)
        //        textWriter = new StreamWriter(path);
        //    else
        //        textWriter = new StreamWriter(new IsolatedStorageFileStream(path, FileMode.OpenOrCreate, isolatedStorageFolder));

        //    return textWriter;
        //}

        private static TextWriter CreateTextWriter(string path)
        {

            TextWriter textWriter = null;

            textWriter = new StreamWriter(path);

            return textWriter;
        }

        private static T LoadFromXML(string path, IsolatedStorageFile isolatedStorageFolder)
        {
            T serializableObject = null;

            using (TextReader textReader = CreateTextReader(isolatedStorageFolder, path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                serializableObject = xmlSerializer.Deserialize(textReader) as T;

            }

            return serializableObject;
        }

        private static T LoadFromXML(string xml)
        {
            T serializableObject = null;

            using (StringReader reader = new StringReader(xml))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                serializableObject = xmlSerializer.Deserialize(reader) as T;
            }

            return serializableObject;

        }

        private static string SaveToXML(T serializableObject)
        {
            StringWriter writer = new StringWriter();

            String output = String.Empty;
            //try
            //{
            XmlWriterSettings writerSettings = new XmlWriterSettings();

            writerSettings.CheckCharacters = false;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            writerSettings.OmitXmlDeclaration = true;

            using (XmlWriter xmlWriter = XmlWriter.Create(writer,
           writerSettings))
            {
                xmlSerializer.Serialize(xmlWriter, serializableObject);

            }

            output = writer.ToString();
            //}
            //catch (Exception aojEx)
            //{


            //    output = GetXMLFromFile(serializableObject);



            //}

            return output;
        }


        private static TextWriter CreateTextWriter(IsolatedStorageFile isolatedStorageFolder, string path)
        {
            TextWriter textWriter = null;

            if (isolatedStorageFolder == null)
                textWriter = new StreamWriter(path);
            else
                textWriter = new StreamWriter(new IsolatedStorageFileStream(path, FileMode.OpenOrCreate, isolatedStorageFolder));

            return textWriter;
        }


        public static String GetXMLFromFile(T serializableObject)
        {
            String output = String.Empty;

            using (StringWriter textWriter = new StringWriter())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(textWriter, serializableObject);

                output = textWriter.ToString();
                textWriter.Close();

            }





            return output;
        }

        /// <summary>

        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.

        /// </summary>

        /// <param name="characters">Unicode Byte Array to be converted to String</param>

        /// <returns>String converted from Unicode Byte Array</returns>

        private static String UTF8ByteArrayToString(Byte[] characters)
        {

            UTF8Encoding encoding = new UTF8Encoding();

            String sconstructedString = encoding.GetString(characters);

            return (sconstructedString);

        }


        private static void SaveToXML(T serializableObject, System.Type[] extraTypes, string path, IsolatedStorageFile isolatedStorageFolder)
        {
            using (TextWriter textWriter = CreateTextWriter(isolatedStorageFolder, path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(textWriter, serializableObject);
            }
        }

        #endregion
    }
}
