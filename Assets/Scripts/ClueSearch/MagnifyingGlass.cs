using System;
using UnityEngine;

public class MagnifyingGlass : DragNDrop
{
    [SerializeField] public Transform TargetObject;
    [SerializeField] public Vector3 searchPosition;
    private float timer = 0;

    public event EventHandler TargetReached;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DragCheck();
        if ( (transform.position + searchPosition - TargetObject.position).magnitude < 2 )
            timer += Time.deltaTime;
        else timer = 0;

        if ( timer >=  3)
            TargetReached.Invoke(null, null);
    }
}
