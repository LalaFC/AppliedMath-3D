using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TrajectoryGuide : MonoBehaviour
{
    [SerializeField] GameObject guidePoint;
    [SerializeField] List<GameObject> guides;
    [SerializeField] float maxNumber;

    [SerializeField] float centerPoint = 5f;
    public float speed = 1f;
    public Transform Player;
    [SerializeField] private float angle;
    private Vector3 offset;

    private Vector3 initialPosition;

    int points = 0;
    bool canGuide = true, canSpawnGuide = true;
    public static UnityEvent trajectoryGuide;

    // Start is called before the first frame update
    void Awake()
    {
        guides = new List<GameObject>();

        // Pool regular bullets
        for (int i = 0; i < maxNumber; i++)
        {
            GameObject obj = Instantiate(guidePoint);
            obj.SetActive(false);
            guides.Add(obj);
        }
    }


    // Update is called once per frame
    private void Update()
    {
        Vector3 pointedEndPosition = CalculatePosition(0);
        offset = new Vector3(0, Player.position.y - pointedEndPosition.y, Player.position.z - pointedEndPosition.z);

        angle += speed * Time.deltaTime;
        float normalizedAngle = angle % (2 * Mathf.PI);
        Vector3 newPosition = CalculatePosition(normalizedAngle);
        transform.position = newPosition + offset;

        if (Vector3.Distance(transform.position, initialPosition) < 0.007f)
        {
            foreach (var guide in guides)
            {
                guide.gameObject.SetActive(false);
            }
            canGuide = true;
        }
        if (canGuide)
            InstantiateGuide();
    }

    void InstantiateGuide()
    {
        if (points < guides.Count-1 && canSpawnGuide)
        {
            StartCoroutine(Guide());
        }
        else if (points >= guides.Count-1)
        {
            canGuide = false;
            foreach (var guide in guides)
            {
                guide.gameObject.SetActive(false);
            }
            points= 0;
        }
    }
    IEnumerator Guide()
    {
        canSpawnGuide= false;
        points++;
        guides[points].transform.position = transform.position;
        guides[points].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        canSpawnGuide= true;

    }

    Vector3 CalculatePosition(float angle)
    {
        float x = (1 - Mathf.Cos(angle)) * Mathf.Sin(angle) * centerPoint;
        float y = Mathf.Sin(30) * centerPoint;
        float z = -Mathf.Cos(angle) * centerPoint;
        return new Vector3(x, y, z);
    }

}
