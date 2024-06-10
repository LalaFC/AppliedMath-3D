using UnityEngine;
using UnityEngine.UI;

public class SampleScript : MonoBehaviour
{
    public Slider Aim;
    [SerializeField] float centerPoint = 5f; 
    public float speed = 2f; 
    [SerializeField] Transform Player;
    [SerializeField]private float angle;
    private Vector3 offset;

    private int cycleCount=0;

    private void Start()
    {
        Player = PlayerManager.instance.player.transform;
        centerPoint = PlayerManager.instance.force.value;
    }
    private void Update()
    {
        Vector3 pointedEndPosition = CalculatePosition(0);
        offset = new Vector3(0, (Player.position.y+3) - pointedEndPosition.y, Player.position.z - pointedEndPosition.z);

        angle += speed * Time.deltaTime;
        float normalizedAngle = angle % (2 * Mathf.PI);
        Vector3 newPosition = CalculatePosition(normalizedAngle);
        transform.position = newPosition + offset;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EndPoint")
        {
            cycleCount++;
            Debug.Log("Object has returned to initial position " + (cycleCount) + " times.");
            if (cycleCount == 1)
            {
                transform.position = new Vector3(0.255102038f, 0.765306115f, 0.586734712f);
                PlayerManager.instance.actions.ChangeCam();
                PlayerManager.instance.trajectory.GuideSwitch();
                Destroy(this.gameObject);
            }
        }
    }

    private void OnDisable()
    {
        
        cycleCount = 0;
    }

    Vector3 CalculatePosition(float angle)
    {
        
        float x = (1 - Mathf.Cos(angle)) * Mathf.Sin(angle) * centerPoint;
        float y = Mathf.Sin(30) * centerPoint;
        float z = -Mathf.Cos(angle) * centerPoint;
        return new Vector3(x, y, z);
    }
}