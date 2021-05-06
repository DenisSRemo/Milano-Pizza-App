using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using TMPro;

public class Manager : MonoBehaviour
{



    private OrderList orderlist;
   [SerializeField] private Menu menu;
    private Button button;
    private List<Button> buttons;
    private int number;
 [SerializeField]  private Order order;
  [SerializeField]  private TextMeshProUGUI address;
    [SerializeField]private TextMeshProUGUI phone_number;
    [SerializeField]private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI total;
    void Start()
    {
        //every time a client opens the app, an order is created
       
        menu.FetchMenuFromAWS();
        number = menu.foodItems.Count;
        //creates a menu on the app
        if (menu.foodItems.Count > 0)
        {
            for (int i = 0; i < menu.foodItems.Count; i++)
                menu.LoadFood(menu.foodItems[i], buttons[i]);
        }
        //creates a order
        order.OrderID = System.DateTime.UtcNow.ToString();// the orderID is unique to the order. \
        //In is based on the time and date the app 
        order.Items = null;
        order.Adreass = null;
        address.text = null;
        phone_number.text = null;
        name.text = null;
        
    }




    void Update()
    {
       
        if (address.text != null)
            order.Adreass = address.text;
        if (phone_number.text != null)
            order.PhoneNumber = phone_number.text;
        if (name.text != null)
            order.Name = name.text;
        total.text = order.Total.ToString();
    }
    
    


    // price of each item choosen by the client in the order total
    public void Select_item(FoodItem item)
    {
        order.Total=order.Total+item.Price;
    }


    public void Send_order()
    {
        if(order.Total!=0&& address.text != null&& phone_number.text != null&& name.text != null)
        {
            orderlist.CreateOrderInTable(order);

        }
    }
}
