using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Das ist die Klasse fur die Karte
public class Card
{
    public string name;
    public string type;
    public string imageName; //konnen wir einfach Images mit Namen von Karten bennen und somit diesen Attribut weglassen?
    public string component1;
    public string component2;
    public string component3;
    public string component4;
    public string description;
    public string diat;

    //wird vor 3 Tagen bevor Abgabe hinzugefuegt
    public int price;
    public int calories;

    //price
    //call

    public Card(string name, string type, string imageName, string component1, string component2, string component3, string component4, string description, string diat)
    {
        this.name = name;
        this.type = type;
        this.imageName = imageName;
        this.component1 = component1;
        this.component2 = component2;
        this.component3 = component3;
        this.component4 = component4;
        this.description = description;
        this.diat = diat;
    }
    public Card(string name, string type, string imageName, string component1, string component2, string component3, string component4, string description, int price, int calories)
    {
        this.name = name;
        this.type = type;
        this.imageName = imageName;
        this.component1 = component1;
        this.component2 = component2;
        this.component3 = component3;
        this.component4 = component4;
        this.description = description;
        this.price = price;
        this.calories = calories;
    }

    public List<string> GetCookingComponents()
    {
        return new List<string> { component1, component2, component3, component4 };
    }
}
