using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Agarrar : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    Camera cam;

    [SerializeField] private Colision coli;
    [SerializeField] private GameManager manager;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Transform setPosition;

    void Start()
    {
        TryGetComponent(out manager);
        cam = Camera.main;
    }


    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !coli.Checkd)
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 25);
            if (Physics.Raycast(ray, out hit, 25, mask))
            {
                hit.transform.TryGetComponent(out Tags obj);

                if (obj && !obj.Checkd)
                {
                    obj.Checkd = true;
                    StartCoroutine(ChangePosition(obj.MyObject));

                    if (!coli.MyProducts.Contains(obj.MyName))
                    {
                        bool validCheck = obj.MyPrice.Equals(manager.MyPrice) && obj.MyColor.Equals(manager.MyColor) && obj.MyTag.Equals(manager.MyTag);
                        coli.AddItem(validCheck, obj.MyName, obj.MySprite);
                    }
                }
            }
        }
    }

    private IEnumerator ChangePosition(Transform obj)
    {
        obj.parent = setPosition;
        while (obj.position != setPosition.position)
        {
            obj.position = Vector3.MoveTowards(obj.position, setPosition.position, 14 * Time.deltaTime);
            yield return null;
        }
        yield break;
    }
}
