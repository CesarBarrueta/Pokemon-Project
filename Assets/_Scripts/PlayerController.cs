using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private bool isMoving;
    public float speed;
    private Vector2 input;
    private Animator _animator;
    public LayerMask solidObjectsLayer, pokemonLayer;
    public int PokemonPercentage;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if(input.x !=0)
            {
                input.y = 0;
            }

            if (input != Vector2.zero)
            {
                _animator.SetFloat("moveX", input.x);
                _animator.SetFloat("moveY", input.y);
                var targetPosition = transform.position;
                targetPosition.x += input.x;
                targetPosition.y += input.y;

                if(IsAvailable(targetPosition))
                {
                    StartCoroutine(MoveTowards(targetPosition));
                }
                
            }
        }
    }

    private void LateUpdate()
    {
        _animator.SetBool("is Moving", isMoving);
    }

    IEnumerator MoveTowards (Vector3 destination) 
    {
        isMoving = true;
        while(Vector3.Distance(transform.position, destination) > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null; //Espera hasta que termine el frame
        }

        transform.position = destination;
        isMoving = false;

        CheckForPokemonEncounter();
    }

    /// <summary>
    /// Comprueba que la zona a acceder esté disponible (no sea sólido)
    /// </summary>
    /// <param name="target">Zona a acceder</param>
    /// <returns>Devuelve true si el target está disponible</returns>
    private bool IsAvailable(Vector3 target)
    {
        if(Physics2D.OverlapCircle(target, 0.2f, solidObjectsLayer) != null)
        {
            return false;
        }

        return true;
    }

    private void CheckForPokemonEncounter ()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, pokemonLayer) != null)
        {
            if (Random.Range(0,100) < PokemonPercentage)
            {
                Debug.Log("Empezar batalla pokemon");
            }
        }
    }

}
