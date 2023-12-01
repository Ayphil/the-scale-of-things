using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    public Vector3 offset;
    [SerializeField] float damping;

    private Vector3 _velocity = Vector3.zero;

    //The background image to use
    public Transform Area;

    //Sprite details
    private Sprite Sprite;
    private float PixelUnits;
    private Vector2 Size;
    private Vector2 Offset;

    //Camera bounds
    private float Left;
    private float Right;
    private float Top;
    private float Bottom;
    private float MaxZoom;
    private float MinZoom;

    private bool _isLocked = false;
    public void Start()
    {
        Refresh();
    }

    //Calculate the pixel per unit value is for this sprite
    private void CalculatePixelUnits()
    {
        PixelUnits = Sprite.rect.width / Sprite.bounds.size.x;
    }

    //Calculate the size and offset of the background sprite
    private void CalculateSize()
    {
        Size = new Vector2(Area.transform.localScale.x * Sprite.rect.width / PixelUnits,
                            Area.transform.localScale.y * Sprite.rect.height / PixelUnits);
        Offset = Area.transform.position;
    }
    //Calculate the max distance the camera can travel
    private void RefreshBounds()
    {
        var vertExtent = Camera.main.orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;
        Left = horzExtent - Size.x / 2.0f + Offset.x;
        Right = Size.x / 2.0f - horzExtent + Offset.x;
        Bottom = vertExtent - Size.y / 2.0f + Offset.y;
        Top = Size.y / 2.0f - vertExtent + Offset.y;
    }
    //Get zoom constraints, and zoom as large as possible for current view
    public void Refresh()
    {
        Sprite = Area.transform.GetComponent<SpriteRenderer>().sprite;
        CalculatePixelUnits();
        CalculateSize();
        RefreshBounds();
    }
    public void CameraLock(Vector3 coordinate){
        transform.position = coordinate;
        _isLocked = true;
    }
    private void FixedUpdate()
    {
        if(!_isLocked){
        Vector3 movePosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref _velocity, damping);
        }
    }

    private void LateUpdate()
    {
        //float xOffset = Area.transform.position.x;
        //Clamp camera inside of our bounds
        if(!_isLocked){
        Vector3 position = transform.position;
        transform.position = new Vector3(Mathf.Clamp(position.x, Left, Right), Mathf.Clamp(position.y, Bottom, Top), position.z);
        }
    }
}
