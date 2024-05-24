#region Using directives
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.Store;
using System;
using UAManagedCore;
using FTOptix.UI;
using FTOptix.ODBCStore;
using FTOptix.DataLogger;
using FTOptix.System;
using FTOptix.RAEtherNetIP;
using FTOptix.CommunicationDriver;
using FTOptix.SQLiteStore;
using System.Reflection.PortableExecutable;
using FTOptix.EdgeAppPlatform;
using System.Threading.Tasks.Dataflow;
#endregion

public class InsertNewProduct : BaseNetLogic {


    [ExportMethod]
    public void InsertProduct() {


        var proj = Project.Current;
        var store = proj.Get<Store>("DataStores/BikeStores");
        var table = store.Tables.Get<Table>("products");

        //string[] header;
        //object[,] resultSet;

        // Get the columns to add.
        //below is dynamic read of columns
        //store.Query("SELECT Column_Name, Data_Type FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'products'", out header, out resultSet);

        //var columnCount = resultSet.GetLength(0);

        //string[] columnNames = new string[columnCount];

        //string[] columnTypes = new string[columnCount];

        //for (var i = 0; i < columnCount; i++)
        //{
        //    columnNames[i] = resultSet[i, 0].ToString();
        //}


       // get column names staticly
        string[] columns = { "product_name", "brand_id", "category_id", "model_year", "list_price" };

        // Create and populate a matrix with values to insert into the odbc table
        var rawValues = new object[1, 5];

        //Value 0
        rawValues[0, 0] = Project.Current.GetVariable("Model/New_Product/product_name").Value.Value;
        //Value 1
        rawValues[0, 1] = Project.Current.GetVariable("Model/New_Product/brand_id").Value.Value;
        //Value 2
        rawValues[0, 2] = Project.Current.GetVariable("Model/New_Product/category_id").Value.Value;
        //Value 3
        rawValues[0, 3] = Project.Current.GetVariable("Model/New_Product/model_year").Value.Value;
        //Value 4
        rawValues[0, 4] = Project.Current.GetVariable("Model/New_Product/list_price").Value.Value;
 
        // Insert values into table
        table.Insert(columns, rawValues);
       

    }
}
