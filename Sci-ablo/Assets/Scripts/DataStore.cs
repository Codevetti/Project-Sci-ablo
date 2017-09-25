using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataStore<R, T> where T : DataStore<R, T>
{

    private static Dictionary<R, T> _items = new Dictionary<R, T>();

    public static T Find(R id)
    {
        return _items[id];
    }

    public static void AddItems(List<T> items)
    {
        foreach(T item in items)
        {
            AddItems(item);
        }
    }

    public static void AddItem(T item)
    {
        string idName = Regex.Replace(item.name, @"\s+", "");
        R id = (R)System.Enum.Parse(typeof(R), idName, true);
        _items[id] = item;
        item.id = id;
    }

    public static void Clear()
    {
        _items.Clear();
    }

    public R id
    {
        get; set;
    }

    public string name
    {
        get; set;
    }

    public string description
    {
        get; set;
    }

    public DataStore()
    {

    }
}
