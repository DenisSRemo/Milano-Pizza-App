using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

public class Menu 
{
    public string cognitoIdentityPoolString;

    private CognitoAWSCredentials credentials;
    private IAmazonDynamoDB _client;
    private DynamoDBContext _context;

    private List<FoodItem> foodItems = new List<FoodItem>();
    private int currentItemIndex;






    private DynamoDBContext Context
    {
        get
        {
            if (_context == null)
                _context = new DynamoDBContext(_client);

            return _context;
        }
    }

    private void LoadFood(FoodItem foodItem)
    {

    }
    private void CycleNextFoodItem()
    {
        if (foodItems.Count <= 0) return;

        if (currentItemIndex < foodItems.Count - 1)
        {
            currentItemIndex++;
            LoadFood(foodItems[currentItemIndex]);
        }
        else
        {
            LoadFood(foodItems[1]);
            currentItemIndex = 0;
        }
    }

    private void CyclePrevFoodItem()
    {
        if (foodItems.Count <= 0) return;

        if (currentItemIndex> 0)
        {
            currentItemIndex--;
            LoadFood(foodItems[currentItemIndex]);
        }
        else
        {
           LoadFood(foodItems[foodItems.Count]);
            currentItemIndex = foodItems.Count - 1;
        }
    }


    private void CreateFoodItemInTable()
    {

    }




}
