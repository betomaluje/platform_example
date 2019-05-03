using UnityEngine;
using DG.Tweening;

public class Axe : MonoBehaviour
{
    public Weapon weapon;
    public float rotationSpeed;
    public LayerMask layerMask;

    private Rigidbody2D rb;
    private bool isBeingThrown = false;
    private bool shouldRotate = false;
    private TrailRenderer trail;

    private bool isPulling = false;
    private Transform weaponHolderTransform;
    private Vector3 originalPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
        trail.enabled = false;
    }

    private void Update()
    {
        if (shouldRotate)
        {
            transform.localEulerAngles += Vector3.back * rotationSpeed * Time.deltaTime;
        }

        if(isPulling)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (isBeingThrown && (layerMask & 1 << hitInfo.gameObject.layer) == 1 << hitInfo.gameObject.layer)
        {
            shouldRotate = false;
            trail.enabled = false;
            // hit something so we need to stuck it there            
            rb.bodyType = RigidbodyType2D.Static;
        }

        if (!hitInfo.gameObject.CompareTag("Player") && !hitInfo.gameObject.CompareTag("GUI"))
        {
            EnemyController enemyController = hitInfo.GetComponent<EnemyController>();

            if (enemyController != null)
            {
                enemyController.ApplyDamage(weapon.attack);
            }            
        }        
    }

    public void ToggleThrow()
    {
        if(isBeingThrown)
        {
            Retrieve();
        } else
        {
            Throw();
        }
    }

    public void Throw()
    {
        if(isBeingThrown)
        {
            return;
        }

        trail.enabled = true;
        isBeingThrown = true;
        weaponHolderTransform = transform.parent.transform;        
        // now we throw it
        rb.transform.parent = null;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(transform.right * 15, ForceMode2D.Impulse);

        // we activate the rotation
        shouldRotate = true;
    }

    public void Retrieve()
    {
        if (!isBeingThrown)
        {
            return;
        }

        rb.bodyType = RigidbodyType2D.Dynamic;

        trail.enabled = true;
        originalPos = weaponHolderTransform.position;
        isPulling = true;
        shouldRotate = true;

        DOTween.Sequence()
            .Append(transform.DOLocalRotate(Vector3.zero, .05f).SetEase(Ease.InOutSine))
            .Append(transform.DOLocalMove(originalPos, 0.5f, false))
            .OnComplete(FinishRetrieve);                  
    }

    public void FinishRetrieve()
    {
        isPulling = false;
        isBeingThrown = false;
        shouldRotate = false;
        trail.enabled = false;

        transform.SetParent(weaponHolderTransform, false);
        transform.localPosition = Vector3.zero;

        transform.rotation = weaponHolderTransform.rotation;

        rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
