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

public class Menu:MonoBehaviour
{
    public string cognitoIdentityPoolString;


    private CognitoAWSCredentials credentials;
    private IAmazonDynamoDB _client;
    private DynamoDBContext _context;

    public List<FoodItem> foodItems = new List<FoodItem>();
    private int currentItemIndex;
    private TextMesh resultText;

    


   
    private void Start()
    {
        credentials = new CognitoAWSCredentials(cognitoIdentityPoolString, RegionEndpoint.EUWest1);
        credentials.GetIdentityIdAsync(delegate (AmazonCognitoIdentityResult<string> result)
        {
            if (result.Exception != null)
            {
                Debug.LogError("exception hit: " + result.Exception.Message);
            }

           // Create a DynamoDB client, passing in the credentials from Cognito.
           var ddbClient = new AmazonDynamoDBClient(credentials, RegionEndpoint.EUWest1);

            resultText.text += ("\n*** Retrieving table information ***\n");

            // Create a DescribeTableRequest to get information about the table
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

          
            _client = ddbClient;

            
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
    // creates a button for every food item on the menu
    public void LoadFood(FoodItem foodItem,Button button)
    {
        button.GetComponentInChildren<Text>().text = foodItem.NumberOnMenu + ".   " + foodItem.Name + "  /n " + foodItem.Description + "    /n" + foodItem.Price;
    }
   

   //the function for accesing the menu from AWS
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
