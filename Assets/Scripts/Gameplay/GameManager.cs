using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public string MyTag { get; private set; }
    public string MyColor { get; private set; }
    public string MyPrice { get; private set; }
    public int ValidItems { get; private set; }
    [Header("Player Tag")]
    [SerializeField] private Sprite[] etiquetas;
    [SerializeField] private Image playerTag;
    [SerializeField] private TMP_Text playerPrice;
    [Space]
    [SerializeField] private List<Tags> tags;
    [SerializeField] private List<Mesh> tagMesh;
    [SerializeField] private List<string> prices;
    [SerializeField] private Color[] cols;

    [Header("UI")]
    [SerializeField] private GameObject right;
    [SerializeField] private GameObject wrong;
    [SerializeField] private GameObject endPanel;

    [Header("Item Check")]
    [SerializeField] private GameObject prefabProd;
    [SerializeField] private Transform prodParent;
    [SerializeField] private Sprite[] checkers;

    public string[] tagList;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        foreach (Tags item in tags)
        {
            int tag = Random.Range(0, tagList.Length);
            int col = Random.Range(0, cols.Length);
            int price = Random.Range(0, prices.Count);

            if (col.Equals(0)) item.MyColor = "Azul";
            else if (col.Equals(1)) item.MyColor = "Rojo";

            item.gameObject.GetComponent<MeshRenderer>().material.color = cols[col];
            item.MyTag = tagList[tag];
            item.gameObject.GetComponent<MeshFilter>().mesh = tagMesh[tag];
            item.MyPrice = prices[price];
            item.Price.text = "$" + item.MyPrice.ToString() + ".000";
        }

        int myDef = Random.Range(0, tags.Count);
        MyColor = tags[myDef].MyColor;
        MyTag = tags[myDef].MyTag;
        MyPrice = tags[myDef].MyPrice;
        playerPrice.text = "$" + MyPrice.ToString() + ".000";

        if (MyColor.Equals("Azul")) playerTag.color = cols[0];
        else if (MyColor.Equals("Rojo")) playerTag.color = cols[1];

        switch (MyTag)
        {
            case "Cuadrado":
                playerTag.sprite = etiquetas[0];
                break;

            case "Estrella":
                playerTag.sprite = etiquetas[1];
                break;

            case "Circulo":
                playerTag.sprite = etiquetas[2];
                break;

            case "Triangulo":
                playerTag.sprite = etiquetas[3];
                break;
        }
    }
    private void Start()
    {
        foreach (Tags item in tags)
        {
            bool valid = item.MyPrice.Equals(MyPrice) && item.MyTag.Equals(MyTag) && item.MyColor.Equals(MyColor);
            if (valid) ValidItems++;
        }
        StartCoroutine(Tweening.SetScale(endPanel.transform, Vector3.zero, false));
    }

    public void CheckItems(List<bool> checks, List<Tags> products)
    {
        for (int i = 0; i < checks.Count; i++)
        {
            GameObject pf = Instantiate(prefabProd, prodParent);
            var txt = pf.GetComponentInChildren<TMP_Text>();
            txt.text = products[i].MyName.ToString();
            pf.transform.GetChild(0).TryGetComponent(out Image chkSP);
            pf.transform.GetChild(1).TryGetComponent(out Image prodSP);
            prodSP.sprite = products[i].MySprite;

            if (checks[i]) chkSP.sprite = checkers[0];
            else chkSP.sprite = checkers[1];
        }
        if (checks.Count.Equals(ValidItems)) StartCoroutine(Tweening.SetScale(right.transform, Vector3.one, true));
        else StartCoroutine(Tweening.SetScale(wrong.transform, Vector3.one, true));
        StartCoroutine(Tweening.SetScale(endPanel.transform, Vector3.one, true));
    } 
    public void NextLevel(string level)
    {
        Loading.scene = level;
        SceneManager.LoadScene("Loading");
    }
}
