using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{

    public Stack<GameObject> arrowStack = new Stack<GameObject>();

    [SerializeField] private GameObject arrowPrefab;

    private void ShootArrow()
    {
        


        if(arrowStack.Count > 0)
        {
            GameObject arrow = arrowStack.Pop();
            
            arrow.SetActive(true);

            Arrow arrowComponent = arrow.GetComponent<Arrow>();

            arrowComponent.rb.velocity = new Vector2(arrowComponent.speed, 0);

        }
        else
        {
            GameObject arrow = Instantiate(arrowPrefab);

            arrow.transform.position = gameObject.transform.position;

            Arrow arrowComponent = arrow.GetComponent<Arrow>();

            arrowComponent.shooter = this;
            
        }

    }






}
