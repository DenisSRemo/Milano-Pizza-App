using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;



//namespace Assets.AWSDynamoDB.Scripts
//{
[DynamoDBTable("Orders")]
public class Order
{

    [DynamoDBHashKey]   // Hash key.
    public string OrderID { get; set; }


    [DynamoDBProperty]
    public string Items { get; set; }

    [DynamoDBProperty]
    public string Adreass { get; set; }
    [DynamoDBProperty]
    public string PhoneNumber { get; set; }

    [DynamoDBProperty]
    public string Name { get; set; }

    [DynamoDBProperty]
    public float Total { get; set; }

    


}