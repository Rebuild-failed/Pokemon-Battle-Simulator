using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private ItemModel model;
    public ItemModel Model
    {
        set
        {
            if (value != null)
            {
                model = value;
            }
        }
        get
        {
            return model;
        }
    }
    public Item(ItemModel _model)
    {
        model = _model;
    }
    public virtual void Do()
    {

    }
	
}
