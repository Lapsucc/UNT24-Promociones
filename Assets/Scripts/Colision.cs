using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Colision : MonoBehaviour
{
    private GameManager manager;

    [Header("Level")]
    public GameObject panel;
    public GameObject seguir;
    public GameObject etuqueta;
    [Space]
    [Header("Products")]
    public List<string> MyProducts;
    [SerializeField] private GameObject prefabProd;
    [SerializeField] private Transform prodParent;
    [SerializeField] private Sprite[] checkers;
    [HideInInspector] public bool Checkd = false;
    public List<bool> selectedItems;
    public List<Sprite> selectedSprites;
    private void Start()
    {
        GameObject.Find("GameManager").TryGetComponent(out manager);
    }
    public void Reconteo()
    {
        Checkd = true;
        TryGetComponent(out Rigidbody rb);
        rb.velocity = Vector3.zero;

        for (int i = 0; i < selectedItems.Count; i++)
        {
            GameObject pf = Instantiate(prefabProd, prodParent);
            pf.TryGetComponent(out TMP_Text txt);
            txt.text = MyProducts[i].ToString();
            pf.transform.GetChild(0).TryGetComponent(out Image chkSP);
            pf.transform.GetChild(1).TryGetComponent(out Image prodSP);
            prodSP.sprite = selectedSprites[i];

            if (selectedItems[i]) chkSP.sprite = checkers[0];
            else chkSP.sprite = checkers[1];
        }
        //manager.CheckValues(selectedItems.Count);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("final") && !Checkd) Reconteo();
    }
    public void AddItem(bool check, string nme, Sprite sp)
    {
        selectedItems.Add(check);
        MyProducts.Add(nme);
        selectedSprites.Add(sp);
    }
}
