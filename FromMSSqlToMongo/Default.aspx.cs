using FromMSSqlToMongo.Utility;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FromMSSqlToMongo
{
    public partial class _Default : Page
    {

        //CONNECTION STRING BOTH MSSQL AND MONGODB
        string sqlconnectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        string mongodbconnectionstring = "mongodb://localhost:27017";
        //string mongodb = "Beer"; //MONGODB DATABASE NAME

        //TABLE SOURCE AND DESTINATION
        //string tableSource =  "Category"; //existing table name of sql server
        //string tableDestination = "Category"; //collection name for Mongo DB

        MongoClient client;
        IMongoDatabase database;
        protected void Page_Load(object sender, EventArgs e)
        {

            client = new MongoClient(mongodbconnectionstring);
        }

        protected void GenerateCollection(object sender, EventArgs  e)
        {
            string db = mongoDb.Text;

            database = client.GetDatabase(db);

            string source = tableSource.Text;
            string destination = tableDestination.Text;

            Transfer transfer = new Transfer(sqlconnectionstring, source, destination);
            transfer.TransferRecordsToMongoDB(database);
        }



    }
}