using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;



//namespace Assets.CharacterCreatorAWSDynamoDB.Scripts
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
    public float Total { get; set; }

    


}