using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon.DynamoDBv2.DataModel;





namespace Assets.MenuAWSDynamoDB.Scripts 
{
    [DynamoDBTable("Menu")]
    public class Menu
    {
        [DynamoDBHashKey]   // Hash key.
        public string FoodID { get; set; }

        [DynamoDBProperty]
        public int NumberOnMenu { get; set; }


        [DynamoDBProperty]
        public string Name { get; set; }


        [DynamoDBProperty]
        public string TypeFood { get; set; }

        [DynamoDBProperty]
        public float Price { get; set; }

    }


}








