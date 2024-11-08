using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class AnchorManager : MonoBehaviour
{
    public static AnchorManager _anchorManager;

    [Header("General settings")]
    public float roomSize = 20;
    public float anchorReach = 3f;
    public float anchorNumber = 5f;

    internal Transform userCamera;
    private List<AnchorContext> anchors = new List<AnchorContext>();

    [Header("Scene elements")]
    public GameObject anchorPrefab;

    [SerializeField]
    Transform floor;

    float minDistance;

    private void Awake()
    {
        _anchorManager = this;
    }
    private void Start()
    {
        userCamera = Camera.main.transform;

        floor.localScale = new Vector3(roomSize/2, 1, roomSize/2);

        GenerateAnchors();
    }

    void Update()
    {
        UpdateAnchors();
    }

    /// <summary>
    /// This method is in charge of generating a certain number of anchors (according to the anchorNumber variable) 
    /// and position them randomly in the space. Bearing in mind that the room is three-dimensional
    /// </summary>
    void GenerateAnchors()
    {
        for (int i = 0; i < anchorNumber; i++)
        {
            float randomX = Random.Range(-roomSize / 2, roomSize / 2);
            float randomZ = Random.Range(-roomSize / 2, roomSize / 2);
            float randomY = Random.Range(0, roomSize);

            Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);

            GameObject anchor = Instantiate(anchorPrefab, randomPosition, Quaternion.identity);
            anchors.Add(anchor.GetComponent<AnchorContext>());
        }
    }

    /// <summary>
    /// This method calculates the user's distance to the different anchors, and changes the status of the anchors according to the result.
    /// </summary>
    void UpdateAnchors()
    {
        AnchorContext closestAnchor = null;
        minDistance = float.MaxValue;

        foreach (var anchor in anchors)
        {
            float distance = Vector3.Distance(anchor.transform.position, userCamera.position);
            if (distance < anchorReach)
            {
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestAnchor = anchor;
                }
            }

            // Change the status of anchors that are not close to each other
            anchor.SetColor(Color.red);
            anchor.EnableCollider(false);
        }

        // change the status of the nearest anchor
        if (closestAnchor != null)
        {
            closestAnchor.SetColor(Color.green);
            closestAnchor.EnableCollider(true);
        }
    }

    internal float GetMinDistance()
    {
        return minDistance;
    }

}
