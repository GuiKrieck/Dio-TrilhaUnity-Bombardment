using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyantScript : MonoBehaviour
{
    public float underwaterDrag = 3f;
    public float underwaterAngularDrag = 1f;
    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;
    private Rigidbody thisRigdidBody;
    public float buoyanceForce = 10f;
    private bool hasTouchedWater = false;

    // Start is called before the first frame update
    void Awake()
    {
        thisRigdidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //check if underwater
        float diffY = transform.position.y;
        bool isUnderwater = diffY < 0;
        if (isUnderwater)
        {
            hasTouchedWater = true;
        }
        //ignore if never touched water
        if (!hasTouchedWater) { return; }
        
        //Buoyance logic
        if (isUnderwater)
        {
            Vector3 vector = Vector3.up * buoyanceForce * -diffY;
            thisRigdidBody.AddForce(vector, ForceMode.Acceleration);
        }
        thisRigdidBody.drag = isUnderwater ? underwaterDrag : airDrag;
        thisRigdidBody.angularDrag = isUnderwater ? underwaterAngularDrag : airAngularDrag;
    }
}
