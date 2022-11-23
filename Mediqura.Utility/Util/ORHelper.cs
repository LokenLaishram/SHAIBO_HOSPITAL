using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.Utility.Util
{
    public static class ORHelper<T>
    {

        #region -Convert DataTable/DataReader to Generic List

        /// <summary>
        /// Converts a DataTable into a generic List<>
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static List<T> FromDataTableToList(DataTable dataTable)
        {
            //This create a new list with the same type of the generic class
            List<T> genericList = new List<T>();

            try
            {
                //Obtains the type of the generic class
                Type t = typeof(T);

                //Obtains the properties definition of the generic class.
                //With this, we are going to know the property names of the class
                PropertyInfo[] pi = t.GetProperties();

                //For each row in the datatable
                foreach (DataRow row in dataTable.Rows)
                {
                    //Create a new instance of the generic class
                    object defaultInstance = Activator.CreateInstance(t);
                    //For each property in the properties of the class
                    foreach (PropertyInfo prop in pi)
                    {
                        if (row.Table.Columns.Contains(prop.Name))
                        {
                            //Get the value of the row according to the field name
                            //Remember that the classïs members and the tableïs field names
                            //must be identical
                            object columnvalue = row[prop.Name];
                            //Know check if the value is null. 
                            //If not, it will be added to the instance
                            if (columnvalue != DBNull.Value)
                            {
                                //Set the value dinamically. Now you need to pass as an argument
                                //an instance class of the generic class. This instance has been
                                //created with Activator.CreateInstance(t)
                                prop.SetValue(defaultInstance, columnvalue, null);
                            }
                        }

                    }
                    //Now, create a class of the same type of the generic class. 
                    //Then a conversion itïs done to set the value
                    T myclass = (T)defaultInstance;
                    //Add the generic instance to the generic list
                    genericList.Add(myclass);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(prop.Name + ": " + ex.ToString());                    
                throw ex;
            }

            //At this moment, the generic list contains all de datatable values
            return genericList;
        }

        /// <summary>
        /// Converts a IDataReader into a generic List<>
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static List<T> FromDataReaderToList(IDataReader dataReader)
        {
            //This create a new list with the same type of the generic class
            List<T> genericList = new List<T>();

            try
            {
                //Obtains the type of the generic class
                Type t = typeof(T);

                //Obtains the properties definition of the generic class.
                //With this, we are going to know the property names of the class
                PropertyInfo[] pi = t.GetProperties();

                //For each row in the datatable
                //if (dataReader.HasRows)
                //{
                while (dataReader.Read())
                {
                    //Create a new instance of the generic class
                    object defaultInstance = Activator.CreateInstance(t);
                    //For each property in the properties of the class
                    foreach (PropertyInfo prop in pi)
                    {
                        dataReader.GetSchemaTable().DefaultView.RowFilter = "ColumnName= '" + prop.Name + "'";
                        bool isExistsColumn = dataReader.GetSchemaTable().DefaultView.Count > 0;
                        if (isExistsColumn == true)
                        {
                            //Get the value of the row according to the field name
                            //Remember that the classïs members and the tableïs field names
                            //must be identical                            
                            object columnvalue = dataReader[prop.Name];
                            //Know check if the value is null. 
                            //If not, it will be added to the instance
                            if (columnvalue != DBNull.Value)
                            {
                                //Set the value dinamically. Now you need to pass as an argument
                                //an instance class of the generic class. This instance has been
                                //created with Activator.CreateInstance(t)
                                prop.SetValue(defaultInstance, columnvalue, null);
                            }
                        }
                    }
                    //Now, create a class of the same type of the generic class. 
                    //Then a conversion itïs done to set the value
                    T myclass = (T)defaultInstance;
                    //Add the generic instance to the generic list
                    genericList.Add(myclass);
                }
                dataReader.Close();
                //}
            }
            catch (Exception ex)
            {
                //Console.WriteLine(prop.Name + ": " + ex.ToString());                    
                throw ex;
            }

            //At this moment, the generic list contains all de datatable values
            return genericList;
        }

        #endregion
        #region -Convert DataTable/DataReader to Generic Class

        /// <summary>
        /// Converts a IDataReader into a generic Class
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static T FromDataReader(IDataReader dataReader)
        {
            object defaultInstance = null;
            try
            {
                //Obtains the type of the generic class
                Type t = typeof(T);

                //Obtains the properties definition of the generic class.
                //With this, we are going to know the property names of the class
                PropertyInfo[] pi = t.GetProperties();

                //For each row in the datatable
                //if (dataReader.HasRows)
                //{
                while (dataReader.Read())
                {
                    //Create a new instance of the generic class
                    defaultInstance = Activator.CreateInstance(t);
                    //For each property in the properties of the class
                    foreach (PropertyInfo prop in pi)
                    {
                        dataReader.GetSchemaTable().DefaultView.RowFilter = "ColumnName= '" + prop.Name + "'";
                        bool isExistsColumn = dataReader.GetSchemaTable().DefaultView.Count > 0;
                        if (isExistsColumn == true)
                        {
                            //Get the value of the row according to the field name
                            //Remember that the classïs members and the tableïs field names
                            //must be identical
                            object columnvalue = dataReader[prop.Name];
                            //Know check if the value is null. 
                            //If not, it will be added to the instance
                            if (columnvalue != DBNull.Value)
                            {
                                //Set the value dinamically. Now you need to pass as an argument
                                //an instance class of the generic class. This instance has been
                                //created with Activator.CreateInstance(t)
                                prop.SetValue(defaultInstance, columnvalue, null);
                            }
                        }
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                //Console.WriteLine(prop.Name + ": " + ex.ToString());                    
                throw ex;
            }

            return (T)defaultInstance;
        }

        /// <summary>
        /// Converts a DataTable into a generic Class
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static T FromDataTable(DataTable dataTable)
        {
            object defaultInstance = null;

            try
            {
                //Obtains the type of the generic class
                Type t = typeof(T);

                //Obtains the properties definition of the generic class.
                //With this, we are going to know the property names of the class
                PropertyInfo[] pi = t.GetProperties();

                //For each row in the datatable

                foreach (DataRow row in dataTable.Rows)
                {
                    //Create a new instance of the generic class
                    defaultInstance = Activator.CreateInstance(t);
                    //For each property in the properties of the class
                    foreach (PropertyInfo prop in pi)
                    {
                        if (row.Table.Columns.Contains(prop.Name))
                        {
                            //Get the value of the row according to the field name
                            //Remember that the classïs members and the tableïs field names
                            //must be identical
                            object columnvalue = row[prop.Name];
                            //Know check if the value is null. 
                            //If not, it will be added to the instance
                            if (columnvalue != DBNull.Value)
                            {
                                //Set the value dinamically. Now you need to pass as an argument
                                //an instance class of the generic class. This instance has been
                                //created with Activator.CreateInstance(t)
                                prop.SetValue(defaultInstance, columnvalue, null);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(prop.Name + ": " + ex.ToString());                    
                throw ex;
            }

            return (T)defaultInstance;
        }

        #endregion
        #region -Convert Generic List To DataTable
        /// <summary> 
        /// Converts a generic List<> into a DataTable. 
        /// </summary> 
        /// <param name="list"></param> 
        /// <returns>DataTable</returns> 
        public static DataTable GenericListToDataTable(object list)
        {
            DataTable dt = null;
            Type listType = list.GetType();
            if (listType.IsGenericType)
            {
                //determine the underlying type the List<> contains 
                Type elementType = listType.GetGenericArguments()[0];

                //create empty table -- give it a name in case 
                //it needs to be serialized 
                dt = new DataTable(elementType.Name + "List");

                //define the table -- add a column for each public 
                //property or field 
                MemberInfo[] miArray = elementType.GetMembers(
                    BindingFlags.Public | BindingFlags.Instance);
                foreach (MemberInfo mi in miArray)
                {
                    if (mi.MemberType == MemberTypes.Property)
                    {
                        PropertyInfo pi = mi as PropertyInfo;
                        dt.Columns.Add(pi.Name, pi.PropertyType);
                    }
                    else if (mi.MemberType == MemberTypes.Field)
                    {
                        FieldInfo fi = mi as FieldInfo;
                        dt.Columns.Add(fi.Name, fi.FieldType);
                    }
                }

                //populate the table 
                IList il = list as IList;
                foreach (object record in il)
                {
                    int i = 0;
                    object[] fieldValues = new object[dt.Columns.Count];
                    foreach (DataColumn c in dt.Columns)
                    {
                        MemberInfo mi = elementType.GetMember(c.ColumnName)[0];
                        if (mi.MemberType == MemberTypes.Property)
                        {
                            PropertyInfo pi = mi as PropertyInfo;
                            fieldValues[i] = pi.GetValue(record, null);
                        }
                        else if (mi.MemberType == MemberTypes.Field)
                        {
                            FieldInfo fi = mi as FieldInfo;
                            fieldValues[i] = fi.GetValue(record);
                        }
                        i++;
                    }
                    dt.Rows.Add(fieldValues);
                }
            }
            return dt;
        }

        #endregion

    }
}
