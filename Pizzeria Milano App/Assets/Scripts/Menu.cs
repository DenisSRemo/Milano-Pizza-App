﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using UnityEngine.UI;

public class Menu:MonoBehaviour
{
    public string cognitoIdentityPoolString;


    private CognitoAWSCredentials credentials;
    private IAmazonDynamoDB _client;
    private DynamoDBContext _context;

    public List<FoodItem> foodItems = new List<FoodItem>();
    private int currentItemIndex;
    private TextMesh resultText;

    


    private void Awake()
    {
    //    createOperation.onClick.AddListener(CreateInTable);
    //    refreshOperation.onClick.AddListener(FetchAllFoodItemsFromAWS);

    //    NextCharacterButton.onClick.AddListener(CycleNextFoodItem);
    //    PrevCharacterButton.onClick.AddListener(CyclePrevFoodItems);
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

            // Create a DescribeTableRequest to get information about our table, and ensure we can access it.
            var request = new DescribeTableRequest
            {
                TableName = @"Menu"
            };

            ddbClient.DescribeTableAsync(request, (ddbresult) =>
                {
                    if (result.Exception != null)
                    {
                        resultText.text += result.Exception.Message;
                        Debug.Log(result.Exception);
                        return;
                    }

                    var response = ddbresult.Response;

                    // Debug information
                    TableDescription description = response.Table;
                    resultText.text += ("Name: " + description.TableName + "\n");
                    resultText.text += ("# of items: " + description.ItemCount + "\n");
                    resultText.text += ("Provision Throughput (reads/sec): " + description.ProvisionedThroughput.ReadCapacityUnits + "\n");
                    resultText.text += ("Provision Throughput (reads/sec): " + description.ProvisionedThroughput.WriteCapacityUnits + "\n");

                }, null);

            // Set our _client field to the dynamoDB client.
            _client = ddbClient;

            // Fetch any stored menu from the DB
           FetchMenuFromAWS();

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

    public void LoadFood(FoodItem foodItem,Button button)
    {
        button.GetComponentInChildren<Text>().text = foodItem.NumberOnMenu + ".   " + foodItem.Name + "  /n " + foodItem.Description + "    /n" + foodItem.Price;
    }
    //private void CycleNextFoodItem()
    //{
    //    if (foodItems.Count <= 0) return;

    //    if (currentItemIndex < foodItems.Count - 1)
    //    {
    //        currentItemIndex++;
    //        LoadFood(foodItems[currentItemIndex]);
    //    }
    //    else
    //    {
    //        LoadFood(foodItems[1]);
    //        currentItemIndex = 0;
    //    }
    //}

    //private void CyclePrevFoodItem()
    //{
    //    if (foodItems.Count <= 0) return;

    //    if (currentItemIndex> 0)
    //    {
    //        currentItemIndex--;
    //        LoadFood(foodItems[currentItemIndex]);
    //    }
    //    else
    //    {
    //       LoadFood(foodItems[foodItems.Count]);
    //        currentItemIndex = foodItems.Count - 1;
    //    }
    //}
    

    private void CreateFoodItemInTable()
    {

    }


    public void FetchMenuFromAWS()
    {
        resultText.text = "\n***LoadTable***";
        Table.LoadTableAsync(_client, "Menu", (loadTableResult) =>
        {
            if (loadTableResult.Exception != null)
            {
                resultText.text += "\n failed to load menu table";
            }
            else
            {
                try
                {
                    var context = Context;

                    // Note scan is pretty slow for large datasets compared to a query, as we are not searching on the index.
                    var search = context.ScanAsync<FoodItem>(new ScanCondition("NumberOnMenu", ScanOperator.GreaterThan, 0));
                    search.GetRemainingAsync(result =>
                    {
                        if (result.Exception == null)
                        {
                            foodItems = result.Result;

                            
                                
                        }
                        else
                        {
                            Debug.LogError("Failed to get async table scan results: " + result.Exception.Message);
                        }
                    }, null);
                }
                catch (AmazonDynamoDBException exception)
                {
                    Debug.Log(string.Concat("Exception fetching items from table: {0}", exception.Message));
                    Debug.Log(string.Concat("Error code: {0}, error type: {1}", exception.ErrorCode, exception.ErrorType));
                }

            }
        });
    }
    


   


   































}
