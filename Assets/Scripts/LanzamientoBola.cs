using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class LanzamientoBola : MonoBehaviour
{
    [Header("Prefabs to thrown")] [SerializeField]
    private GameObject ballPrefab;

    private GameObject textWind;

    public static GameObject ball;
    private AudioSource cannonSound;


    [SerializeField] private float maxTimeChangeWind;
    [SerializeField] private GameObject smoke;

    [Header("Thrown formula PARAMETERS")] [SerializeField] [Range(10, 30)]
    public static float _vinit; // Vinit = Ball throw "force"

    [SerializeField] private Vector3 s; // S = space travelled

    public static bool autoPass;
    [SerializeField] [Range(-180f, 180f)] private float gamma;

    public float alpha = 45f; // gamma y alpha = throw angle, alpha  will not change,  gamma  calculate by a slider
    private float _actualTime;
    private static bool _hasShoot;
    public static bool CanChangeValuesToBall = true;

    private void Awake()
    {
        s = transform.position;
        GameManager.ChangeWind();
        alpha = 45f;
        cannonSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CreateBall();
            autoPass = true;
        }

        _actualTime += Time.deltaTime;
        if (_actualTime >= maxTimeChangeWind)
        {
            _actualTime = 0.00f;
            //ChageWind();
        }
    }

    private void FixedUpdate()
    {
        if (autoPass)
        {
            if (ball != null) //Cuando el autopass detecta que no hay una pelota en escena
            {
                if (CanChangeValuesToBall)
                {
                    _vinit = GameManager.force.value;
                    gamma = GameManager.angle.value;
                    cannonSound.Play();
                    smoke.GetComponent<VisualEffect>().SendEvent("Start");
                    smoke.GetComponent<VisualEffect>().SetFloat("Wind_Angle", GameManager._windGammaAngle);
                    smoke.GetComponent<VisualEffect>().SetFloat("Wind_Force", GameManager._vw);
                }

                Vector vectorBall = BallSimulation(ball.GetComponent<Ball>());
                ball.transform.position = vectorBall.toVector3();
            }
        }
    }

    #region Methods

    #region (cosThetas)

    /// <summary>
    /// X Direction Cosines calculated by length and projection formula 
    /// </summary>
    float CalculateThetaX(float alpha, float gamma)
    {
        float x = Mathf.Sin(gamma * Mathf.PI / 180.0f) * Mathf.Sin(alpha * Mathf.PI / 180.0f);
        return x;
    }

    /// <summary>
    /// Y Direction Cosines calculated by length and projection formula 
    /// </summary>
    float CalculateThetaY(float alpha)
    {
        float y = Mathf.Cos(alpha * Mathf.PI / 180.0f);
        return y;
    }

    /// <summary>
    /// Z Direction Cosines calculated by length and projection formula 
    /// </summary>
    float CalculateThetaZ(float alpha, float gamma)
    {
        float z = Mathf.Cos(gamma * Mathf.PI / 180.0f) * Mathf.Sin(alpha * Mathf.PI / 180.0f);
        return z;
    }

    #endregion


    /// <summary>
    /// Calculates ball's initial Position from directory cosine and lenght from the throwing object (gun, canon, etc)
    /// </summary>
    /// <returns></returns>
    Vector ThrowBall()
    {
        Vector cosD = new Vector(CalculateThetaX(alpha, gamma), CalculateThetaY(alpha),
            CalculateThetaZ(alpha, gamma)); //Calculates Direction Cosines from alpha and gamma angles

        //Change _VinitAtthe beggining of the throw, by the value of the Force Slider
        CanChangeValuesToBall = false;
        return new Vector(_vinit * cosD.xPos, _vinit * cosD.yPos, _vinit * cosD.zPos);
    }

    #region ball

    /// <summary>
    /// Formula for ball movement in all axis (X.Y,Z), obtained from Direction Cosines
    /// </summary>
    Vector BallSimulation(Ball ball)
    {
        ball.time += Time.deltaTime;
        if (ball.time > 1f)
            smoke.GetComponent<VisualEffect>().SendEvent("End");
        Vector V = ThrowBall();


        float x = s.x +
            ((ball.mass / GameManager._cd) * Mathf.Exp(-((GameManager._cd / ball.mass) * ball.time)) *
             (CalculateWindForceX() - V.xPos) -
             (-CalculateWindForceX() * ball.time)) - ((ball.mass / GameManager._cd) * (CalculateWindForceX() - V.xPos));

        float y = s.y +
                  (-(V.yPos + (ball.mass * ball.gravity) / GameManager._cd) * (ball.mass / GameManager._cd) *
                   Mathf.Exp(-(GameManager._cd * ball.time) / ball.mass) -
                   (ball.mass * ball.gravity * ball.time) / GameManager._cd) +
                  ((ball.mass / GameManager._cd) * (V.yPos + (ball.mass * ball.gravity) / GameManager._cd));

        float z = s.z +
            ((ball.mass / GameManager._cd) * Mathf.Exp(-((GameManager._cd / ball.mass) * ball.time)) *
             (CalculateWindForceZ() - V.zPos) -
             (-CalculateWindForceZ() * ball.time)) - ((ball.mass / GameManager._cd) * (CalculateWindForceZ() - V.zPos));


        return new Vector(x, y, z);
    }

    /// <summary>
    /// Create a ball and adding some parameters 
    /// </summary>
    void CreateBall()
    {
        Ball componentBall;
        if (ball == null)
        {
            ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            componentBall = ball.GetComponent<Ball>();
            componentBall.gravity = 9.8f;
            componentBall.time = 0f;
            componentBall.mass = 2f;


            if (componentBall.time > 0.5f)
                smoke.GetComponent<VisualEffect>().SendEvent("End");
        }
    }

    #endregion


    /// <summary>
    /// Some part from the formulas that are repeated on the big one
    /// </summary>
    float CalculateWindForceX()
    {
        return (-((GameManager._cw * GameManager._vw * Mathf.Sin(GameManager._windGammaAngle * Mathf.PI / 180.0f)) /
                  GameManager._cd));
    }

    /// <summary>
    /// Some part from the formulas that are repeated on the big one
    /// </summary>
    float CalculateWindForceZ()
    {
        return (-((GameManager._cw * GameManager._vw * Mathf.Cos(GameManager._windGammaAngle * Mathf.PI / 180.0f)) /
                  GameManager._cd));
    }

    #endregion
}