﻿using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferDBs
{
    public class Transfer
    {
        private string tableSource; //existing table name of sql server
        private string tableDestination; //collection name for Mongo DB
        private MongoClient client;
        private IMongoDatabase database;

        //CONNECTION STRING BOTH MSSQL AND MONGODB
        private string sqlconnectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        private string mongodbconnectionstring = "mongodb://localhost:27017";


        public Transfer(string db, string source, string destination)
        {          
            tableSource = source;
            tableDestination = destination;
            client = new MongoClient(mongodbconnectionstring);
            database = client.GetDatabase(db);
        }
        public bool TransferRecordsToMongoDB()
        {

            var coll = database.GetCollection<BsonDocument>(tableSource);
            var result = false;

            using (SqlConnection conn = new SqlConnection(sqlconnectionstring))
            {
                string query = "SELECT * FROM " + tableSource;

                using (SqlCommand objCmd = new SqlCommand(query, conn))
                {
                    // TO PREVENT DUPLICACY DELETE COLLECTION IF ALREADY EXIST.
                    database.DropCollection(tableDestination);

                    conn.Open();
                    SqlDataReader objReader = objCmd.ExecuteReader();
                    List<BsonDocument> bsonlist = new List<BsonDocument>(1000);
                    while (objReader.Read())
                    {
                        BsonDocument objBson = new BsonDocument();

                        for (int j = 0; j < objReader.FieldCount; j++)
                        {
                            //CHECK TYPE OF ALL COLUMN NAMES SO THAT WE CAN STORE ALL RECORDS IN COLLECTION "DATATYPE WISE"
                            if (objReader[j].GetType() == typeof(String))
                            {
                                objBson.Add(new BsonElement(objReader.GetName(j), objReader[j].ToString()));
                            }
                            else if ((objReader[j].GetType() == typeof(Int32)))
                            {
                                objBson.Add(new BsonElement(objReader.GetName(j), BsonValue.Create(objReader.GetInt32(j))));
                            }
                            else if (objReader[j].GetType() == typeof(Int16))
                            {
                                objBson.Add(new BsonElement(objReader.GetName(j), BsonValue.Create(objReader.GetInt16(j))));
                            }
                            else if (objReader[j].GetType() == typeof(Int64))
                            {
                                objBson.Add(new BsonElement(objReader.GetName(j), BsonValue.Create(objReader.GetInt64(j))));
                            }
                            else if (objReader[j].GetType() == typeof(float))
                            {
                                objBson.Add(new BsonElement(objReader.GetName(j), BsonValue.Create(objReader.GetFloat(j))));
                            }
                            else if (objReader[j].GetType() == typeof(Double))
                            {
                                objBson.Add(new BsonElement(objReader.GetName(j), BsonValue.Create(objReader.GetDouble(j))));
                            }
                            else if (objReader[j].GetType() == typeof(DateTime))
                            {
                                objBson.Add(new BsonElement(objReader.GetName(j), BsonValue.Create(objReader.GetDateTime(j))));
                            }
                            else if (objReader[j].GetType() == typeof(Guid))
                            {
                                objBson.Add(new BsonElement(objReader.GetName(j), BsonValue.Create(objReader.GetGuid(j))));
                            }
                            else if (objReader[j].GetType() == typeof(Boolean))
                            {
                                objBson.Add(new BsonElement(objReader.GetName(j), BsonValue.Create(objReader.GetBoolean(j))));
                            }
                            else if (objReader[j].GetType() == typeof(DBNull))
                            {
                                objBson.Add(new BsonElement(objReader.GetName(j), BsonNull.Value));
                            }
                            else if (objReader[j].GetType() == typeof(Byte))
                            {
                                objBson.Add(new BsonElement(objReader.GetName(j), BsonValue.Create(objReader.GetByte(j))));
                            }
                            else if (objReader[j].GetType() == typeof(Byte[]))
                            {
                                objBson.Add(new BsonElement(objReader.GetName(j), BsonValue.Create(objReader[j] as Byte[])));
                            }
                            else
                            {
                                objBson.Add(new BsonElement(objReader.GetName(j), objReader[j].ToString()));
                            }
                        }

                        bsonlist.Add(objBson);
                    }

                    //INSERT ALL RECORDS IN MONGODB COLLECTION
                    coll = database.GetCollection<BsonDocument>(tableDestination);
                    coll.InsertMany(bsonlist);
                    bsonlist.RemoveRange(0, bsonlist.Count);
                    result = true;
                }
            }
            return result;
        }
    }

}
