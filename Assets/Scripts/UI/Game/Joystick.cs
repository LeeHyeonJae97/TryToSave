using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void Move(Vector3 dir);

    public Transform mainCam;
    public Move move;

    public Transform bkg, stick;
    public Image bkgImage, stickImage;
    public Color32 translucent, transparent;

    private Vector2 dir;
    private int bkgRadius;
    private bool isTouched;
    public bool IsFixed { get; private set; }

    public Vector2 leftFixedPosition;
    public Vector2 rightFixedPosition;

    private void Awake()
    {
        bkgRadius = (int)((RectTransform)bkg).rect.width / 2;        
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

            if (dir != Vector2.zero) move(new Vector3(dir.x, 0, dir.y));
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
        else stick.position = bkg.position;        

        isTouched = false;
    }

    private Vector2 StickPosBound()
    {
        Vector2 pos = Input.mousePosition;
        Vector2 offset = pos - (Vector2)bkg.position;

        if (offset.sqrMagnitude < bkgRadius * bkgRadius) return pos;

        else return offset.normalized * bkgRadius + (Vector2)bkg.position;
    }

    // 조이스틱의 위치 설정, SettingsManager에서 호출된다.
    public void SetJoystickPos(int index)
    {
        switch (index)
        {
            case 0:
                IsFixed = false;
                bkgImage.color = stickImage.color = transparent;                 
                break;
            case 1:
                IsFixed = true;
                bkg.position = stick.position = leftFixedPosition;
                bkgImage.color = stickImage.color = translucent;                
                break;
            case 2:
                IsFixed = true;
                bkg.position = stick.position = rightFixedPosition;
                bkgImage.color = stickImage.color = translucent;
                break;
        }
    }
}
