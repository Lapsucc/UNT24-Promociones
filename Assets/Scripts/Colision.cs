using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Colision : MonoBehaviour
{
    public GameObject panel;
    public List<string> list, list2;
    public TMP_Text[] prodName;
    public Sprite[] renders;
    public Agarrar agarro;
    public Managernivel mana;
    public GameObject seguir;
    public GameObject etuqueta;
    [Space]
    [SerializeField] private GameObject prefabProd;
    [SerializeField] private Transform prodParent;
    [SerializeField] private Sprite[] checkers;
    public List<Sprite> prodSprite;
    private bool checkd = false;
    public void Reconteo()
    {
        checkd = true;
        TryGetComponent(out Rigidbody rb);
        rb.velocity = Vector3.zero;

        foreach (string prod in list2)
        {
            GameObject pf = Instantiate(prefabProd, prodParent);
            pf.TryGetComponent(out TMP_Text txt);
            txt.text = list[list2.IndexOf(prod)].ToString();
            pf.transform.GetChild(0).TryGetComponent(out Image chkSP);
            pf.transform.GetChild(1).TryGetComponent(out Image prodSP);

            //prodSP.sprite = agarro.proim[indx];

            if (prod.Equals("Aprobado") || prod.Equals("APROBADO")) chkSP.sprite = checkers[0];
            else if (prod.Equals("Rechazado") || prod.Equals("RECHAZADO")) chkSP.sprite = checkers[1];
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        TryGetComponent(out Collider col);
        col.enabled = false;

        if (other.gameObject.CompareTag("final"))
        {
            if (!checkd) Reconteo();
            panel.SetActive(true);
            seguir.SetActive(false);
            etuqueta.SetActive(false);
        }
    }
}
