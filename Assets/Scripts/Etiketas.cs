using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Etiketas : MonoBehaviour
{
    public List<int> precio;
    public int valor;
    public List<Color> colores;
    public TMP_Text texto;
    public Color coler;
    public string nomcolor;
    public GameObject cubo;
    public List<Mesh> mesh;
    public Managernivel manag;
    public Image Product;
    public int eti;
    public string nomEtiqueta;
    public string[] ArrayEtiquetas;
    public int A;

    void Start()
    {
        A = Random.Range(0, colores.Count);
        for (int i = 0; i < 2; i++)
        {
            precio.Add(manag.valor);
            colores.Add(manag.col[manag.A]);
            mesh.Add(mesh[manag.col1]);
            mesh.Add(mesh[manag.col1]);
        }
        eti = Random.Range(0, mesh.Count);
        GetComponent<MeshFilter>().mesh = mesh[eti];
        valor = precio[Random.Range(0, precio.Count)];

        switch (A)
        {
            case 1:
                {
                    coler = colores[1];
                    cubo.GetComponent<MeshRenderer>().material.color = coler;
                    nomcolor = "Azul";
                }
                break;

            case 2:
                {
                    coler = colores[0];
                    cubo.GetComponent<MeshRenderer>().material.color = coler;
                    nomcolor = "Rojo";
                }
                break;

            default:
                {
                    nomcolor = manag.color;
                    cubo.GetComponent<MeshRenderer>().material.color = manag.col[manag.A];
                }
                break;
        }


        texto.text = "$" + valor.ToString() + ".000";

        if (eti >= ArrayEtiquetas.Length)
        {
            nomEtiqueta = manag.nomEtiqueta;
            Debug.Log("ejecución");
        }
        else nomEtiqueta = ArrayEtiquetas[eti];
    }
    public void Validar()
    {
        // if (manag.valor == valor && nomcolor == manag.color && nomEtiqueta == manag.nomEtiqueta) jeison.nvl++;
    }
}
