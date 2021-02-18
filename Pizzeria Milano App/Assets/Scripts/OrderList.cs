using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using UnityEngine.UI;

public class OrderList : MonoBehaviour
{
    public string cognitoIdentityPoolString;


    private CognitoAWSCredentials credentials;
    private IAmazonDynamoDB _client;
    private DynamoDBContext _context;

    private List<FoodItem> foodItems = new List<FoodItem>();
    private int currentItemIndex;
    private TextMesh resultText;
    private Menu menu;



    private void Awake()
    {
        menu.FetchMenuFromAWS();
          //  createOperation.onClick.AddListener(CreateCharacterInTable);
        //    refreshOperation.onClick.AddListener(FetchAllfoodFromAWS);

        //    NextCharacterButton.onClick.AddListener(CycleNextFoodItem);
        //    PrevCharacterButton.onClick.AddListener(CyclePrevFoodItem);
    }
    private void Start()
    {
        credentials = new CognitoAWSCredentials(cognitoIdentityPoolString, RegionEndpoint.EUWest1);
        credentials.GetIdentityIdAsync(delegate (AmazonCognitoIdentityResult<string> result)
        {
            if (result.Exception != null)
            {
                Debug.LogError("exception hit: " + result.Exception.Message);
            }

            // Create a DynamoDB client, passing in our credentials from Cognito.
            var ddbClient = new AmazonDynamoDBClient(credentials, RegionEndpoint.EUWest1);

            resultText.text += ("\n*** Retrieving table information ***\n");

            

            

            

        });
    }






    private DynamoDBContext Context
    {
        get
        {
            if (_context == null)
                _context = new DynamoDBContext(_client);

            return _context;
        }
    }


    

   












    public void CreateOrderInTable(Order order)
    {
        var newOrder = order;
       

       
        Context.SaveAsync(newOrder, (result) =>
        {
            if (result.Exception == null)
                resultText.text += @"order saved";
        });
    }
























}
