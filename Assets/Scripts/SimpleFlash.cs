using System.Collections;

using UnityEngine;


public class SimpleFlash : MonoBehaviour
{

    [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration;

 
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Coroutine flashRoutine;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    public void Flash()
    {

        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    // Changes color of the sprite (used in player death)
    private IEnumerator FlashRoutine()
    {

        spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(duration);

        spriteRenderer.material = originalMaterial;

        flashRoutine = null;
    }

}
