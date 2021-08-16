using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveVector;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _moveVector.x = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        WalkPlayer(); 
    }

    private void WalkPlayer()
    {
        
       _rigidbody2D.velocity = new Vector2(_moveVector.x * _speed, _rigidbody2D.velocity.y);
    }


}
