using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] private RaycastHit hit;
    [HideInInspector] private Ray ray;
    [HideInInspector] private Camera cam;
    [HideInInspector] private Rigidbody rb;
    [Header("Movement")]
    [Range(0, 20)][SerializeField] private float speedForce;
    [HideInInspector] private bool Forward = false, Backwards = false;
    [HideInInspector] private bool Checkd = false;

    [Header("Pick Objects")]
    [SerializeField] private LayerMask mask;
    [SerializeField] private Transform setPosition;
    [HideInInspector] private string MyPrice, MyColor, MyTag;

    [Header("Products")]
    private List<bool> sItems = new();
    private List<Tags> sTags = new();

    private void Start()
    {
        TryGetComponent(out rb);
        cam = Camera.main;
        MyPrice = GameManager.Instance.MyPrice;
        MyColor = GameManager.Instance.MyColor;
        MyTag = GameManager.Instance.MyTag;
    }
    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !Checkd)
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 25, mask))
            {
                hit.collider.TryGetComponent(out Tags tag);

                if (tag && !tag.Checkd && !sTags.Contains(tag))
                {
                    tag.Checkd = true;
                    StartCoroutine(ChangePosition(tag.MyObject));
                    sTags.Add(tag);
                    sItems.Add(tag.MyPrice.Equals(MyPrice) && tag.MyColor.Equals(MyColor) && tag.MyTag.Equals(MyTag));
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (!Checkd)
        {
            if (Forward || Input.GetAxis("Horizontal") > 0) rb.AddForce(new Vector3(0, 0, speedForce) * Time.deltaTime, ForceMode.Impulse);
            else if (Backwards || Input.GetAxis("Horizontal") < 0) rb.AddForce(new Vector3(0, 0, -speedForce) * Time.deltaTime, ForceMode.Impulse);
            else rb.velocity = Vector3.zero;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("final") && !Checkd)
        {
            Checkd = true;
            TryGetComponent(out Rigidbody rb);
            Forward = Backwards = false;
            rb.velocity = Vector3.zero;
            rb.Sleep();
            GameManager.Instance.CheckItems(sItems, sTags);
        }
    }

    public void AddItem(bool check, Tags tag)
    {
        sItems.Add(check);
        sTags.Add(tag);
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
    public void MForwards(bool val)
    {
        Forward = val;
        if (val) Backwards = !val;
    }
    public void MBackwards(bool val)
    {
        Backwards = val;
        if (val) Forward = !val;
    }
}
