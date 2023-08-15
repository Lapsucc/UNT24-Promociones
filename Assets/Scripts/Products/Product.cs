using UnityEngine;

[CreateAssetMenu(fileName = "Product", menuName = "Products", order = 0)]
[System.Serializable]
public class Product : ScriptableObject
{
    public Sprite icon;
    [Space]
    public string nme;
    public string price;
    public Mesh mesh;
}
