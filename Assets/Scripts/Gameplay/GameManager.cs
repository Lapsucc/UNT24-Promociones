using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public string MyTag { get; private set; }
    public string MyColor { get; private set; }
    public string MyPrice { get; private set; }
    public int ValidItems { get; private set; }

    [Header("Start UI")]
    [SerializeField] private Transform startPanel;
    [SerializeField] private Image mainTag;
    [SerializeField] private TMP_Text instruction;
    [SerializeField] private TMP_Text mainPrice;

    [Header("Player Tag")]
    [SerializeField] private Sprite[] etiquetas;
    [Space]
    [SerializeField] private List<Tags> tags;
    [SerializeField] private List<Mesh> tagMesh;
    [SerializeField] private List<string> prices;
    [SerializeField] private Color[] cols;
    [HideInInspector] private Color daColor;
    [HideInInspector] private Sprite daSprite;
    [HideInInspector] private Mesh daMesh;

    [Header("UI")]
    [SerializeField] private GameObject right;
    [SerializeField] private GameObject wrong;
    [SerializeField] private GameObject endPanel;

    [Header("Item Check")]
    [SerializeField] private GameObject prefabProd;
    [SerializeField] private Transform prodParent;
    [SerializeField] private Sprite[] checkers;

    public string[] tagList;

    [Header("Products")]
    [SerializeField] private List<Transform> positions = new();
    [SerializeField] private int[] prodsPerLevel = new int[10];
    private int productsAmnt = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        productsAmnt = PlayerData.level switch
        {
            1 => prodsPerLevel[0],
            2 => prodsPerLevel[1],
            3 => prodsPerLevel[2],
            4 => prodsPerLevel[3],
            5 => prodsPerLevel[4],
            6 => prodsPerLevel[5],
            7 => prodsPerLevel[6],
            8 => prodsPerLevel[7],
            9 => prodsPerLevel[8],
            10 => prodsPerLevel[9],
            _ => prodsPerLevel[0],
        };

        foreach (Tags item in tags)
        {
            int tag = Random.Range(0, tagList.Length);
            int col = Random.Range(0, cols.Length);
            int price = Random.Range(0, prices.Count);

            if (col.Equals(0)) item.MyColor = "Azul";
            else if (col.Equals(1)) item.MyColor = "Rojo";

            item.gameObject.GetComponent<MeshRenderer>().material.color = cols[col];
            item.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", cols[col]);
            item.MyTag = tagList[tag];
            item.gameObject.GetComponent<MeshFilter>().mesh = tagMesh[tag];
            item.MyPrice = prices[price];
            item.Price.text = "$" + item.MyPrice.ToString() + ".000";
        }

        int myDef = Random.Range(0, tags.Count);
        MyColor = tags[myDef].MyColor;
        MyTag = tags[myDef].MyTag;
        MyPrice = tags[myDef].MyPrice;

        if (MyColor.Equals("Azul")) daColor = cols[0];
        else if (MyColor.Equals("Rojo")) daColor = cols[1];

        daSprite = MyTag switch
        {
            "Cuadrado" => etiquetas[0],
            "Estrella" => etiquetas[1],
            "Circulo" => etiquetas[2],
            "Triangulo" => etiquetas[3],
            _ => etiquetas[0],
        };

        daMesh = MyTag switch
        {
            "Cuadrado" => tagMesh[0],
            "Estrella" => tagMesh[1],
            "Circulo" => tagMesh[2],
            "Triangulo" => tagMesh[3],
            _ => tagMesh[0],
        };

        instruction.text = "Toma los productos que tengan:" +
            "\nUn precio de: $" + MyPrice.ToString() + ".000" +
            "\nUna etiqueta de color: " + MyColor.ToUpper() +
            "\nY su figura sea un/a: " + MyTag.ToUpper();

        mainTag.sprite = daSprite;
        mainTag.color = daColor;
        mainPrice.text = "$" + MyPrice.ToString() + ".000";

        foreach (Tags item in tags)
        {
            bool valid = item.MyPrice.Equals(MyPrice) && item.MyTag.Equals(MyTag) && item.MyColor.Equals(MyColor);
            if (valid) ValidItems++;
        }
    }
    private void Start()
    {
        PlayerController.Instance.SetMeshTag(daMesh, daColor, "$" + MyPrice.ToString() + ".000");
        StartCoroutine(Tweening.SetScale(startPanel, Vector3.one, true));
    }

    public void CheckItems(List<bool> checks, List<Tags> products)
    {
        for (int i = 0; i < checks.Count; i++)
        {
            GameObject pf = Instantiate(prefabProd, prodParent);
            pf.transform.GetChild(0).TryGetComponent(out Image chkSP);
            pf.transform.GetChild(1).TryGetComponent(out Image prodSP);
            pf.transform.GetChild(2).TryGetComponent(out TMP_Text txt);
            txt.text = products[i].MyName.ToString();
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
        if (PlayerData.level < 10)
        {
            PlayerData.level++;
            Loading.scene = level;
            SceneManager.LoadScene(1);
        }
    }
    public void BackToMenu()
    {
        Loading.scene = "MainMenu";
        SceneManager.LoadScene(1);
    }

    public void HideObject(Transform tf) { StartCoroutine(Tweening.SetScale(tf, Vector3.zero, false)); }
    public void ShowObject(Transform tf) { StartCoroutine(Tweening.SetScale(tf, Vector3.one, true)); }
}
