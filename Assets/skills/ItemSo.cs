using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum skillAorP
{
    패시브,
    엑티브
}

[System.Serializable]
public class Item
{
    public Sprite Img;
    public string name;
    public skillAorP SkillAorP;
}


[CreateAssetMenu]
public class ItemSo : ScriptableObject
{
    public List<Item> items;
}
