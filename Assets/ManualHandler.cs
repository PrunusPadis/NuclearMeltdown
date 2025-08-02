using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ManualHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    public GameObject manual;
    public GameObject pagesParent;
    public HideManual hideManualCollider;
    public GameObject hideposObj;
    public GameObject showposObj;

    public PageTurner pageTurnerPrevious;
    public PageTurner pageTurnerNext;

    public List<GameObject> pages;

    private Vector3 targetPos = Vector3.negativeInfinity;
    private bool updatePos = false;
    private float minDistance = 0.001f;
    private float speed = 1f;
    public Vector3 hidepos;
    public Vector3 showpos;
    public bool manualShown = false;
    private int currentPage = 0;
    private int maxPages = 1;

    private Vector3 hoverPos;
    private Vector3 originalPos;

    private GameObject rotatePageTarget = null;
    private bool rotating = false;
    private int rotationDir = 0;
    private float rotationSpeed = 1.5f;

    public void Start()
    {
        foreach (Transform child in pagesParent.transform)
        {
            pages.Add(child.gameObject);
        }
        maxPages = pagesParent.transform.childCount;
        hoverPos = new Vector3(manual.transform.position.x, manual.transform.position.y, manual.transform.position.z - 0.02f);
        originalPos = manual.transform.position;
        hidepos = hideposObj.transform.position;
        showpos = showposObj.transform.position;
    }

    public void ShowManual()
    {
        Debug.Log("ManualHandler ShowManual");
        targetPos = showpos;
        updatePos = true;
        //manualShown = true;
        hideManualCollider.ToggleHideManual();
        GetComponent<BoxCollider>().enabled = false;
        pageTurnerPrevious.setEnabled(true);
        pageTurnerNext.setEnabled(true);
    }


    public void HideManual()
    {
        Debug.Log("ManualHandler HideManual");
        targetPos = hidepos;
        updatePos = true;
        //manualShown = false;
        GetComponent<BoxCollider>().enabled = true;
        pageTurnerPrevious.setEnabled(false);
        pageTurnerNext.setEnabled(false);
    }


    public void NextPage()
    {
        Debug.Log("start NextPage currentPage" + currentPage);
        var pageToTurn = pages[currentPage];
        rotatePageTarget = pageToTurn;
        rotating = true;
        rotationDir = 1;

        //pageToTurn.transform.rotation = new Quaternion(0, 0, 180, 0);
        //pageToTurn.transform.rotation = new Quaternion(0,0,0,0);

        currentPage = currentPage + 1 < maxPages ? currentPage + 1 : currentPage;

        Debug.Log("NextPage currentPage" + currentPage);
    }

    public void PreviousPage()
    {
        Debug.Log("start PreviousPage currentPage" + currentPage);
        var pageToTurn = pages[currentPage];
        //pageToTurn.transform.rotation = new Quaternion(0, 0, 0, 0);

        currentPage = currentPage - 1 > 0 ? currentPage - 1 : 0;

        rotatePageTarget = pageToTurn;
        rotating = true;
        rotationDir = -1;

        Debug.Log("PreviousPage currentPage" + currentPage);
    }

    private void Update()
    {
        if (updatePos && targetPos != Vector3.negativeInfinity)
        {
            if (Vector3.Distance(manual.transform.position, targetPos) > minDistance)
            {
                manual.transform.position = Vector3.MoveTowards(manual.transform.position, targetPos, speed * Time.deltaTime);
            } else
            {
                updatePos = false;
                targetPos = Vector3.negativeInfinity;
                manualShown = !manualShown;
            }
        }

        if (rotating && rotatePageTarget != null)
        {
            if (rotationDir == -1)
            {
                var newZ = Mathf.Lerp(
                    rotatePageTarget.transform.rotation.z,
                    0,
                    rotationSpeed * Time.deltaTime);

                var rot = rotatePageTarget.transform.eulerAngles;
                rot.z = newZ;
                //rotatePageTarget.transform.rotation = rot;
                rotatePageTarget.transform.eulerAngles = rot;


                //if (rotatePageTarget.transform.rotation.z <= 0.01f)
                //{
                //    rotating = false;
                //    rotatePageTarget = null;
                //    rotationDir = 0;
                //}
            }
            else if (rotationDir == 1)
            {

                var newZ = Mathf.Lerp(
                    rotatePageTarget.transform.rotation.z,
                    180,
                    rotationSpeed * Time.deltaTime);

                var rot = rotatePageTarget.transform.eulerAngles;
                rot.z = newZ;
                //rotatePageTarget.transform.rotation = rot;
                rotatePageTarget.transform.eulerAngles = rot;

                //if (rotatePageTarget.transform.rotation.z <= 179.99f)
                //{
                //    rotating = false;
                //    rotatePageTarget = null;
                //    rotationDir = 0;
                //}
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("ManualHandler OnPointerEnter");
        if (!manualShown)
        {
            manual.transform.position = hoverPos;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("ManualHandler OnPointerExit");
        if (!manualShown)
        {
            manual.transform.position = originalPos;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("ManualHandler OnPointerUp, manualShown: " + manualShown);
        //if (!manualShown)
        //{
        //    ShowManual();
        //}
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        if (!manualShown)
        {
            ShowManual();
        }
    }
}
