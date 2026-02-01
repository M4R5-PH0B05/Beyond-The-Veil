using NUnit.Framework.Constraints;
using UnityEngine;

public class MaskController : MonoBehaviour
{

    [SerializeField] private CharacterController characterController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void CurrentMask()
    {
        CharacterController.MaskState curMaskState = characterController.m_maskState;
        if (curMaskState == CharacterController.MaskState.none)
        {
            spriteRenderer.enabled = false;
        }
        else if (curMaskState == CharacterController.MaskState.climbingmask)
        {
            spriteRenderer.enabled = true;
            spriteRenderer.sprite = sprites[0];
        }
        else if (curMaskState == CharacterController.MaskState.doubleJump)
        {
            spriteRenderer.enabled = true;
            spriteRenderer.sprite = sprites[1];
        }
        else if (curMaskState == CharacterController.MaskState.grapple)
        {
            spriteRenderer.enabled = true;
            spriteRenderer.sprite = sprites[2];
        }
        else if (curMaskState == CharacterController.MaskState.wallTangibility)
        {
            spriteRenderer.enabled = true;
            spriteRenderer.sprite = sprites[3];
        }
        else
        {
            Debug.LogError("Current Mask not Value");
        }






    }
    
}
