using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropIgnore : MonoBehaviour
{
    [SerializeField] Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image> ();

    }

    // Update is called once per frame
    void Update()
    {

        if (EventSystem.current.currentSelectedGameObject != null&& image.raycastTarget == true){
            image.raycastTarget = false;
        }
        if (EventSystem.current.currentSelectedGameObject == null&& image.raycastTarget == false){
            image.raycastTarget = true;
        }
    }
}
