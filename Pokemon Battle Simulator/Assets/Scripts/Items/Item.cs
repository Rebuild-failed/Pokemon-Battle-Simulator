using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private ItemModel model;
    public Item(ItemModel _model)
    {
        model = _model;
    }
    public ItemModel GetModel()
    {
        return model;
    }
    public virtual void Do()
    {

    }
	
}
