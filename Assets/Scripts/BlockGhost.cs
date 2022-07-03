using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BlockGhost : MonoBehaviour
{
    [SerializeField] Image ghostImage;
    // Start is called before the first frame update
    void Start()
    {
        if (ghostImage == null) {
            ghostImage = GetComponent<Image>();
        }
        ghostImage.color = Color.clear;

    }

    // Update is called once per frame
    void Update()
    {
       if (EventSystem.current.currentSelectedGameObject is GameObject selectedblock){
            var blockImage = selectedblock.GetComponent<Image>();
            if (blockImage){
                ghostImage.color=blockImage.color;
            }
            transform.position = selectedblock.transform.position;
       } 
    }
}
