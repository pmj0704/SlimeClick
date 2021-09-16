using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curs : MonoBehaviour
{

    Vector3 mousePos, transPos;
    public Vector3 targetPos;
    private CircleCollider2D collider2D = null;
    [SerializeField]
    private GameObject quitPanel;
    [SerializeField]
    private Camera main;

    private void Awake()
    {
        collider2D = GetComponent<CircleCollider2D>();
        GameManager.Instance.setMousePos(targetPos);
    }

    private void Update()
    {
         if(Input.GetMouseButtonDown(0))
        {
            CalTargetPos();
            if(targetPos.y > 0)
            {
                transform.position = targetPos;
                GameManager.Instance.setMousePos(transform.position);
                Debug.Log(transform.position);
                RippleEffect.Instance.Emit();
                StartCoroutine(Fade());
            }
        }
         if(Input.GetKeyDown(KeyCode.Escape))
        {
            quitPanel.SetActive(true);
            if(quitPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    void CalTargetPos()
    {
        mousePos = Input.mousePosition;
        transPos = main.ScreenToWorldPoint(mousePos);
        targetPos = new Vector3(transPos.x, transPos.y, 0);
        GameManager.Instance.setMousePos(targetPos);
    }
    private IEnumerator Fade()
    {
        if (Mathf.Round(collider2D.radius) == 0)
        {
            for (float i = 0; i < 0.7f; i += 0.05f)
            {
                collider2D.radius = i;
                yield return new WaitForSeconds(0.01f);
            }
            collider2D.radius = 0;
        }
    }
}
