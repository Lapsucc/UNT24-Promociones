using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logica_entre : MonoBehaviour
{
    private void Awake()
    {
        Logica_entre[] noDestruirEntreEscenas = FindObjectsOfType<Logica_entre>();
        if (noDestruirEntreEscenas.Length>1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
