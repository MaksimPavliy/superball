using SplineMesh;
using Superball;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Ball : MonoBehaviour
{
    [SerializeField] private GameObject groundEdge;
    [SerializeField] private ParticleSystem _deathParticles;
    public Spline spline;
    private CurveSample sample;
    private Rigidbody2D _rigidbody;
    private Vector3 gForce = new Vector3(0f, 0f, 9.8f);

    private bool enteredLeftTube;
    private bool enteredRightTube;

    private Vector3 velocity;
    private Vector3 inVelocity;

    private float tubeDistance;

    public int jumpCounter;

   /* private bool InTube => enteredRightTube && enteredLeftTube;*/

    bool freeFlight = true;
    private Vector2 tempVelocity;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.simulated = false;
        EventSignature();
    }

    private void EventSignature()
    {
        GameManager.instance.PlayPressed.AddListener(OnPlay);
        GameManager.instance.Won.AddListener(DoWin);
        GameManager.instance.Lose.AddListener(DoLose);
    }

    public void OnPlay()
    {
        _rigidbody.simulated = true;
    }

    public void DoWin()
    {
    }

    public void DoLose()
    {
        ParticleSystem ps = Instantiate(_deathParticles, transform.position, Quaternion.identity);
        ps.gameObject.transform.up = tempVelocity.normalized;
        ps.Play();
        Destroy(gameObject);
        Destroy(ps.gameObject, 2f);
    }

    private void OnDestroy()
    {
        if (!GameManager.instance) return;
        GameManager.instance.PlayPressed.RemoveListener(OnPlay);
        GameManager.instance.Won.RemoveListener(DoWin);
        GameManager.instance.Lose.RemoveListener(DoLose);
    }

    private void Update()
    {
        //вручную обрабатываем движение, если мячик находится внутри трубы
        if (enteredLeftTube || enteredRightTube)
        {
            //берём семпл на заданной дистанции
            sample = spline.GetSampleAtDistance(tubeDistance);

            //двигаем мячик к заданному семплу
            transform.localPosition = new Vector3(sample.location.x, -sample.location.z, transform.localPosition.z);

            //находим вектор направления движения в текущем семпле
            var direction = sample.Rotation * Vector3.forward;

            //вычисляем ускорение, как проекцию силы тяжести на направление движения
            var acceleration = Vector3.Project(gForce, direction).z;

            if (enteredLeftTube)
                acceleration = direction.z > 0 ? -acceleration : acceleration;

            else
                acceleration = direction.z > 0 ? acceleration : -acceleration;

            //изменяем скорость, используя ускорение
            velocity += new Vector3(0f, acceleration * Time.deltaTime, 0f);

            //определяем дистанцию для вычислеия следующего семпла
            //приплюсовываем если влетел слева и отнимаем, если влетел справа
            tubeDistance += velocity.magnitude * Time.deltaTime * (enteredLeftTube ? 1 : -1);

            //если дистанция выходит за границы сплайна, покидаем трубу
            if (tubeDistance >= spline.Length || tubeDistance <= 0)
            {
                ExitTube();
            }
        }
        else
        {
            velocity = _rigidbody.velocity;
        }
    }

    private void FixedUpdate()
    {
        tempVelocity = _rigidbody.velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ground") && groundEdge.GetComponent<BoxCollider2D>().enabled == true)
        {
            GameManager.instance.DoLose();
        }

        if (collision.CompareTag("leftTube") && velocity.y < 0)
        {
            //входим в трубу только если мы ещё не в ней
            if (freeFlight)
            {
                EnterTube();
                //при входе в левую трубу стартовая дистанция 0
                tubeDistance = 0;
                enteredLeftTube = true;
            }
        }

        if (collision.CompareTag("rightTube") && velocity.y < 0)
        {
            //входим в трубу только если мы ещё не в ней
            if (freeFlight)
            {
                EnterTube();
                //при входе в правую трубу стартовая дистанция равна длине трубы
                tubeDistance = spline.Length - 0.01f;
                enteredRightTube = true;
            }
        }

        if (collision.CompareTag("obstacle") && GetComponent<CircleCollider2D>().transform.position.y > groundEdge.transform.position.y)
        {
            GameManager.instance.DoLose();
        }
    }

    //входим в трубу
    private void EnterTube()
    {
        //здесь можно было бы сразу определять, с какой стороны мы влетели в трубу, определив дистанцию ближайшего семпла
        //но для этого нужно немного подтюнить плагин, а нам это сейчас не надо.

        groundEdge.GetComponent<BoxCollider2D>().enabled = false;

        //костыль - скорость влёта в трубу определяем только в первый раз и используем её дальше для входа и для выхода
        //сделано это для того, чтобы избежать погрешностей, которые полюбому возникнут из-за того, что при входе и выходе
        //мячик никогда не будет чётко в одной и той же точке и скорость будет немного теряться
        if (inVelocity == Vector3.zero)
        {
            inVelocity = _rigidbody.velocity;
        }
        //начальная скорость, которую используем для движения по трубе
        velocity = inVelocity;

        //во время движения по труде нам не нужно влияние физики на мячик
        _rigidbody.isKinematic = true;

        freeFlight = false;
    }
    //выход из трубы
    private void ExitTube()
    {
        enteredLeftTube = false;
        enteredRightTube = false;

        //на выходе из трубы снова включаем силы для ригидбоди и задаём начальную скорость, обратную скорости входа
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = -inVelocity;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("leftTube") && _rigidbody.velocity.y > 0 || collision.CompareTag("rightTube") && _rigidbody.velocity.y > 0)
        {
            //включаем свободный полёт только когда мячик уже покинул землю, чтобы не было повторных коллизий с трубой
            jumpCounter++;
            freeFlight = true;
            groundEdge.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
