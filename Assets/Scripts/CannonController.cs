using UnityEngine;
using UnityEngine.UI;

public class CannonController : MonoBehaviour
{

    private const float PowerIncrement = 5;
    private const float MaxPower = 35;
    private const float MinPower = 5;

    [SerializeField]
    private Text _powerText;
    [SerializeField]
    private Text _angleText;
    public int Number_of_CannonBalls;

    public GameObject cannonBall;
    public Transform cannonBallSpawn;

    [Range (0.001f, 2.0f)]
    public float TranslationIncrement = .7f;

    private const float AngleIncrement = 5f;

    public float cannon_Power = 5;
    private float _angle = 0;

    public float speed = 10f;


    public void AngleUp()
    {
        if (_angle + AngleIncrement > 90f) return;
        // Debug.Log("Move Angle up");
        _angle += AngleIncrement;
        _angleText.text = _angle + "";
        transform.rotation = Quaternion.Euler(0, 0, _angle);
    }

    public void AngleDown()
    {
        if (_angle - AngleIncrement <= 0f) return;
        _angle -= AngleIncrement;
        _angleText.text = _angle + "";
        transform.rotation = Quaternion.Euler(0, 0, _angle);
    }

    public void PowerUp()
    {
        if (cannon_Power + PowerIncrement > MaxPower) return;
        cannon_Power += PowerIncrement;
        _powerText.text = cannon_Power + "";
    }
    public void PowerDown()
    {
        if (cannon_Power - PowerIncrement < MinPower) return;
        cannon_Power -= PowerIncrement;
        _powerText.text = cannon_Power + "";
    }
    public void ShootCannonBall()
    {
        var acannonBall = Instantiate(cannonBall, cannonBallSpawn.transform.position, Quaternion.identity);
        Rigidbody2D cannonBallRigidBody = acannonBall.GetComponent<Rigidbody2D>();
        cannonBallRigidBody.velocity = Quaternion.Euler(0, 0, _angle) * Vector3.right * cannon_Power; 
    }

    private void Awake()
    {
        Debug.Log("In Awake");
        Number_of_CannonBalls = 10;
    }
    // Use this for initialization
    void Start()
    {
        Debug.Log("In Start");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("In Update");
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // Move the cannon to the right
            transform.root.position += Vector3.right * TranslationIncrement;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Move the cannon to the left
            transform.root.position += Vector3.left * TranslationIncrement;
        }

        //Accelerometer
        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        if (dir.sqrMagnitude > 1)
            dir.Normalize();
        dir *= Time.deltaTime;
        transform.root.Translate(dir * speed);
    }

    // For physics
    private void FixedUpdate()
    {
        
    }

    // For UI stuff
    private void LateUpdate()
    {
        
    }
}
