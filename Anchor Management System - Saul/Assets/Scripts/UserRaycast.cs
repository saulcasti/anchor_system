using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserRaycast : MonoBehaviour
{
    [SerializeField]
    Camera camera;

    [SerializeField]
    TextMeshProUGUI distanceText;

    float raycastDistance = 100f;

    Ray ray;
    RaycastHit hit;
    void Update()
    {
        ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // the raycast is launched from the centre of the user's camera.

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            if(hit.collider.gameObject.tag == "Anchor")
            {

                distanceText.text = $"Distance: {AnchorManager._anchorManager.GetMinDistance():F2}m";
                SetPositionRotation(distanceText.transform.parent.parent, hit.point);
            }
            else
            {
                HideDistanceText();
            }
        }
        else
        {
            HideDistanceText();
        }
    }

    /// <summary>
    /// This method changes the position and rotation of the panel passed as a parameter, 
    /// taking into account the collision point of the raycast (also as a parameter).
    /// </summary>
    /// <param name="panel"></param>
    /// <param name="hitPoint"></param>
    void SetPositionRotation(Transform panel, Vector3 hitPoint)
    {

        // Rotar el panel para que mire hacia la cámara
        Vector3 direction =  panel.position - camera.transform.position;

        panel.position = hitPoint - direction.normalized * .5f;

        direction.y = 0; // Evitar que el panel se incline hacia arriba o hacia abajo
        panel.rotation = Quaternion.LookRotation(direction);

        panel.gameObject.SetActive(true);
    }

    void HideDistanceText()
    {
        distanceText.transform.parent.parent.gameObject.SetActive(false);
    }
}
