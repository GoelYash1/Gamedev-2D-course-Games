using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] Color32 hasPackageColor = new Color32(1,1,1,1);
    [SerializeField] Color32 noPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] float destroyDelay = 0.5f;
    bool hasPackage;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ouch! Look where you going Flurry");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Package") && !hasPackage)
        {
            Debug.Log("You have found a Lost item. Hurry Flurry!");
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            Destroy(collision.gameObject,destroyDelay);
        }
        if(collision.CompareTag("Customer"))
        {
            if (hasPackage)
            {
                Debug.Log("You have delivered the item to its owner, Thanks Lossless!");
                hasPackage = false;
                spriteRenderer.color = noPackageColor;
            }
            else
            {
                Debug.Log("Go Away!! Come with a Package");
            }
        }
    }
}
