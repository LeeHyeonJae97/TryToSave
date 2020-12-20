using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{   
    private Transform mainCam;
    public Transform bkg;
    public Transform stick;

    public Player player;

    private Image bkgImage, stickImage;
    private Color32 translucent, transparent;
    private Vector2 dir;
    private int bkgRadius;
    private bool isTouched;

    public bool IsFixed { get; set; }

    private void Awake()
    {
        mainCam = Camera.main.transform;
        bkgRadius = (int)((RectTransform)bkg).rect.width / 2;

        bkgImage = bkg.GetComponent<Image>();
        stickImage = stick.GetComponent<Image>();

        translucent = new Color32(255, 255, 255, 100);
        transparent = new Color32(255, 255, 255, 0);

        IsFixed = false;
    }

    private void OnEnable()
    {
        bkgImage.color = IsFixed ? translucent : transparent;
        stickImage.color = IsFixed ? translucent : transparent;
    }

    private void FixedUpdate()
    {
        if (isTouched)
        {
            stick.position = StickPosBound();

            dir = (stick.position - bkg.position).normalized;
            dir = dir.x * new Vector2(mainCam.right.x, mainCam.right.z).normalized + dir.y * new Vector2(mainCam.forward.x, mainCam.forward.z).normalized;

            if (dir != Vector2.zero) player.Move(new Vector3(dir.x, 0, dir.y));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!IsFixed)
        {
            bkg.position = stick.position = eventData.position;

            bkgImage.color = translucent;
            stickImage.color = translucent;
        }

        isTouched = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!IsFixed)
        {
            bkgImage.color = transparent;
            stickImage.color = transparent;
        }
        else
            stick.position = bkg.position;        

        isTouched = false;
    }

    private Vector2 StickPosBound()
    {
        Vector2 pos = Input.mousePosition;
        Vector2 offset = pos - (Vector2)bkg.position;

        if (offset.sqrMagnitude < bkgRadius * bkgRadius) return pos;

        else return offset.normalized * bkgRadius + (Vector2)bkg.position;
    }

    public void SetFixedPosition(Vector2 pos)
    {
        bkg.position = pos;
        stick.position = pos;
    }
}
