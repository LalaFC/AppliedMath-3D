using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public Transform EndPoint;
    public Slider slider;
    private Vector3 StartPoint;
    [SerializeField] float speed;
    private bool move = false;
    private bool inPosition = true;

    private void Start()
    {
        StartPoint = transform.position;
        slider.maxValue = EndPoint.transform.position.x - transform.position.x;
    }
    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            inPosition = false;
            if (this.name == "LerpCube")
            {
                Lerp();
            }


            else if (this.name == "MoveTowardsCube")
            {
                MoveTowards();
            }


            if (transform.position == EndPoint.transform.position)
            {
                move = false;
            }
        }
    }
    public void MoveObject()
    {
       if (inPosition)
        {    
            move = true;
        }
        else
        {
            transform.position = StartPoint;
            slider.value = 0;
            move = true;
        }
    }
    private void Lerp()
    {
        EndPoint.transform.position = new Vector3(EndPoint.transform.position.x, transform.position.y, transform.position.z);
        slider.value = Mathf.Lerp(slider.value, slider.maxValue, speed*Time.deltaTime);
        this.transform.position = new Vector3(Mathf.Lerp(transform.position.x, EndPoint.position.x, speed * Time.deltaTime), transform.position.y, transform.position.z);
    }
    private void MoveTowards()
    {
        EndPoint.transform.position = new Vector3(EndPoint.transform.position.x, transform.position.y, transform.position.z);
        slider.value = Mathf.MoveTowards(slider.value, slider.maxValue, speed*Time.deltaTime);
        this.transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, EndPoint.transform.position.x, speed * Time.deltaTime),transform.position.y,transform.position.z);
    }
}
