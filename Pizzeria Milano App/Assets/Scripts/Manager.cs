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
       
        menu.FetchMenuFromAWS();
        number = menu.foodItems.Count;
        if (menu.foodItems.Count > 0)
        {
            for (int i = 0; i < menu.foodItems.Count; i++)
                menu.LoadFood(menu.foodItems[i], buttons[i]);
        }
        order.OrderID = System.DateTime.UtcNow.ToString();
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

    //public void CreateButton(Transform panel, Vector3 position, Vector2 size)
    //{
    //    GameObject button = new GameObject();
    //    button.transform.parent = panel;
    //    button.AddComponent<RectTransform>();
    //    button.AddComponent<Button>();
    //    button.transform.position = position;
    //    button.GetComponent<RectTransform>().localScale = size;
            
        
    //}



    public void Select_item()
    {
       // order.total=order.total+fooditem.price;
    }


    public void Send_order()
    {
        if(order.Total!=0&& address.text != null&& phone_number.text != null&& name.text != null)
        {
            orderlist.CreateOrderInTable(order);

        }
    }
}
