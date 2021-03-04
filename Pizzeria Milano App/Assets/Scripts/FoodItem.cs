using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;



//namespace Assets.AWSDynamoDB.Scripts
//{
    [DynamoDBTable("Menu")]
    public class FoodItem
    {

        [DynamoDBHashKey]   // Hash key.
        public string NumberOnMenu { get; set; }


        [DynamoDBProperty]
        public string Name { get; set; }

         [DynamoDBProperty]
    public string Description { get; set; }

   

        [DynamoDBProperty]
        public string TypeFood { get; set; }

        [DynamoDBProperty]
        public int Price { get; set; }

   

}


//}




