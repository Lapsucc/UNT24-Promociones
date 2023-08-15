using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Agarrar : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    Camera cam;
    public bool seleccion = true;
    public GameObject anim;
    Rigidbody animRigid;
    MeshFilter animFilter;
    MeshRenderer animRender;
    public Transform inicioanim;
    public Managernivel manager;
    public Colision coli;
    public Image[] imagenlistaaprovado;
    public Sprite[] checks;
    public Image[] Renders;
    public int suma;
    public GameObject boton;
    public GameObject boton_no;
    public Repetirnivel pasar;
    public Objetos obj;
    public List<Sprite> proim;

    void Start()
    {
        manager = GetComponent<Managernivel>();
        cam = Camera.main;
        animRigid = anim.GetComponent<Rigidbody>();
        animFilter = anim.GetComponent<MeshFilter>();
        animRender = anim.GetComponent<MeshRenderer>();

    }


    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && seleccion == true)
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                hit.transform.TryGetComponent(out Objetos objs);

                if (objs != null)
                {
                    anim.SetActive(true);
                    anim.transform.position = inicioanim.position;
                    anim.transform.localScale = hit.transform.localScale;
                    animRigid.useGravity = true;
                    animFilter.mesh = hit.transform.GetComponent<MeshFilter>().sharedMesh;
                    animRender.materials = hit.transform.GetComponent<MeshRenderer>().materials;

                    if (!coli.list.Contains(objs.nombre))
                    {
                        coli.list.Add(objs.nombre);
                        bool validCheck = objs.etiketas.valor.Equals(manager.valor) && objs.etiketas.nomcolor.Equals(manager.color) && objs.etiketas.nomEtiqueta.Equals(manager.nomEtiqueta);

                        if (validCheck) coli.list2.Add("Aprobado");
                        else coli.list2.Add("Rechazado");

                        suma++;
                        coli.prodSprite.Add(objs.Imag);
                    }
                }
            }
        }

        if (manager.puntosbuenos > 0 && manager.puntosmalos == 0)
        {
            //falso.pasNivel = true;
            boton.SetActive(true);
        }
        else
        {
            //falso.pasNivel = false;
            boton.SetActive(false);

        }
    }
}
