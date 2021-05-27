using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AutomaticCanon : MonoBehaviour
{
    [Header("Prefabs to thrown")] [SerializeField]
    private GameObject ballPrefab;

    private GameObject textWind;

    [SerializeField] private GameObject ball;
    private AudioSource cannonSound;


    [SerializeField] private float timeToShoot;

    [Header("Thrown formula PARAMETERS")] [SerializeField] [Range(10, 30)]
    private float _vinit; // Vinit = Ball throw "force"

    [SerializeField] private Vector3 s; // S = space travelled

    public bool autoPass;
    [SerializeField] [Range(-31f, 10f)] private float gamma;

    public float alpha = 45f; // gamma y alpha = throw angle, alpha  will not change,  gamma  calculate by a slider
    private float _actualTime;
    private static bool _hasShoot;

    public static bool CanChangeValuesToBall = true;

    private void Awake()
    {
        s = transform.position;
        cannonSound = GetComponent<AudioSource>();
        //   ChageWind();
        alpha = 45f;
        StartCoroutine(NewBall());
    }

    void Update()
    {
        if (GameManager.isDestroyed)
        {
            autoPass = false;
        }

        _actualTime += Time.deltaTime;
        if (_actualTime >= timeToShoot)
        {
            _actualTime = 0.00f;
            cannonSound.Play();
            CreateBall();
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
                }

                Vector vectorBall = BallSimulation(ball.GetComponent<Ball>());
                ball.transform.position = vectorBall.toVector3();
            }
        }

        if (GameManager.isDestroyed)
        {
            autoPass = false;
        }
    }

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

    Vector ThrowBall()
    {
        Vector cosD = new Vector(CalculateThetaX(alpha, gamma), CalculateThetaY(alpha),
            CalculateThetaZ(alpha, gamma)); //Calculates Direction Cosines from alpha and gamma angles
        CanChangeValuesToBall = false;
        return new Vector(_vinit * cosD.xPos, _vinit * cosD.yPos, _vinit * cosD.zPos);
    }

    /// <summary>
    /// Formula for ball movement in all axis (X.Y,Z), obtained from Direction Cosines
    /// </summary>
    Vector BallSimulation(Ball ball)
    {
        ball.time += Time.deltaTime;

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
    /// Createn a ball and adding some parameters 
    /// </summary>
    void CreateBall()
    {
        Ball componentBall;
        cannonSound.Play();
        autoPass = true;
        if (ball == null)
        {
            ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            componentBall = ball.GetComponent<Ball>();
            componentBall.gravity = 9.8f;
            componentBall.time = 0f;
            componentBall.mass = 2f;
        }
    }

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


    IEnumerator NewBall()
    {
        if (autoPass)
        {
            if (ball != null) //Cuando el autopass detecta que no hay una pelota en escena
            {
                Vector
                    vectorBall =
                        BallSimulation(ball
                            .GetComponent<Ball>()); //busca el vector de movimiento de la pelota (que indica en todo momento donde esta la pelota) 
                ball.transform.position =
                    vectorBall.toVector3(); //Se le iguala este vector a la transformada de la posición
            }
            else
            {
                autoPass = false;
            }
        }

        yield return new WaitForSeconds(5);
    }
}