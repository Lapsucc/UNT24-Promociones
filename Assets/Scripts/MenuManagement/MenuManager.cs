using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pop;
    [SerializeField] private TMP_Text popError;
    [Header("Data")]
    public InputField RecopiladoNombre;
    public InputField recopiladorCedula;

    public List<string> recom;
    public List<string> lag;
    public GameObject nom;
    public GameObject next;
    public Dropdown drop;

    void Start()
    {
    }

    public void NuevoJugador()
    {
        if (!string.IsNullOrWhiteSpace(RecopiladoNombre.text) && !string.IsNullOrWhiteSpace(recopiladorCedula.text))
        {
            string ha = RecopiladoNombre.text + " " + recopiladorCedula.text;
            nom.SetActive(true);

        }
        else
        {
            popError.text = "Debes llenar todos los campos";
            pop.SetActive(true);
        }

    }
    public void Filtro()
    {
        for (int i = 0; i < lag.Count; i++)
        {
            for (int j = 0; j < lag.Count; j++)
            {
                if (i != j)
                {
                    if (lag[i] == lag[j])
                    {
                        lag.RemoveAt(j);
                        Filtro();
                        return;
                    }
                }
            }
            drop.ClearOptions();
            drop.AddOptions(lag);
        }

    }
    public void SelecOldPlayer()
    {
        int L = drop.value;
    }

    public void SetLevel(string lvl)
    {
        Loading.scene = lvl;
        SceneManager.LoadScene(1);
    }
    public void Abandon()
    {
        Application.Quit();
    }
}
