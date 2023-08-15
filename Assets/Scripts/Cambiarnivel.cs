using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cambiarnivel : MonoBehaviour
{
    public int nomEscena;

    public void iniciar()
    {
        SceneManager.LoadScene(nomEscena);
    }
}
