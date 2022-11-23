using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.Utility.Util
{
    public class ObjectUtil
    {

        public static List<T> GetDataObject<T>(DataSet ds)
        {
            DataTable dtDataTable = null;
            List<T> lstObject = null;
            String sTypeName = null;
            try
            {
                if (ds.Tables.Count > 0)
                {
                    dtDataTable = ds.Tables[0];
                    if (dtDataTable != null)
                    {
                        //objDataSet = dtDataTable.DataSet;
                        sTypeName = typeof(T).Name;
                        dtDataTable.DataSet.DataSetName = "ArrayOf" + sTypeName;
                        dtDataTable.TableName = sTypeName;
                        dtDataTable.Namespace = null;
                        dtDataTable.DataSet.Namespace = null;
                        lstObject = ObjectXMLSerializer<List<T>>.Load(dtDataTable);
                    }
                }
            }
            catch (Exception a_objEx)
            {
                throw a_objEx;
            }
            finally
            {
                // Clean up Code
            }

            return lstObject;

        }

        public static List<T> GetDataObject<T>(DataTable dtDataTable)
        {
            List<T> lstObject = null;
            String sTypeName = null;
            try
            {
                if (dtDataTable != null)
                {
                    //objDataSet = dtDataTable.DataSet;
                    sTypeName = typeof(T).Name;
                    dtDataTable.DataSet.DataSetName = "ArrayOf" + sTypeName;
                    dtDataTable.TableName = sTypeName;
                    dtDataTable.Namespace = null;
                    dtDataTable.DataSet.Namespace = null;
                    lstObject = ObjectXMLSerializer<List<T>>.Load(dtDataTable);
                }
            }
            catch (Exception a_objEx)
            {
                throw a_objEx;
            }
            finally
            {
                // Clean up Code
            }
            return lstObject;
        }

        public static List<T> GetDataObject<T>(IDataReader dr)
        {
            List<T> lstObject = null;
            String sTypeName = null;
            try
            {
                sTypeName = typeof(T).Name;

                DataSet ds = new DataSet();
                ds.Load(dr, LoadOption.PreserveChanges, new string[] { sTypeName });
                if (ds.Tables.Count > 0)
                {
                    DataTable dtDataTable = ds.Tables[0];
                    if (dtDataTable != null)
                    {
                        sTypeName = typeof(T).Name;
                        dtDataTable.DataSet.DataSetName = "ArrayOf" + sTypeName;
                        dtDataTable.TableName = sTypeName;
                        dtDataTable.Namespace = null;
                        dtDataTable.DataSet.Namespace = null;
                        lstObject = ObjectXMLSerializer<List<T>>.Load(dtDataTable);
                    }
                }
            }
            catch (Exception a_objEx)
            {
                throw a_objEx;
            }
            finally
            {
                // Clean up Code
            }
            return lstObject;
        }

        //Convert  Custom Object  collection to datatable
        // Insert Data in Datatable
        //public static DataTable ConvertTo<T>(IList<T> lstObj)
        //{
        //    //create DataTable Structure
        //    DataTable dtTmp = CreateTable<T>();
        //    Type entType = typeof(T);

        //    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
        //    //get the list item and add into the list

        //    foreach (T item in lstObj)
        //    {
        //        DataRow row = dtTmp.NewRow();
        //        foreach (PropertyDescriptor prop in properties)
        //        {
        //            row[prop.Name] = prop.GetValue(item);
        //        }
        //        dtTmp.Rows.Add(row);
        //    }

        //    return dtTmp;
        //}


        // Create Table and its columns
        public static DataTable CreateTable<T>()
        {
            Type entType = typeof(T);

            //set the datatable name as class name
            DataTable dtTemp = new DataTable(entType.Name);

            //get the property list
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);

            foreach (PropertyDescriptor prop in properties)
            {
                //add property as column
                dtTemp.Columns.Add(prop.Name, prop.PropertyType);
            }
            return dtTemp;
        }
    }
}
