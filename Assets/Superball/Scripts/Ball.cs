using SplineMesh;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Ball : MonoBehaviour
{
    [SerializeField] private GameObject groundEdge;
    [SerializeField] GameObject deathParticles;
    public Spline spline;
    private CurveSample sample;
    private Rigidbody2D rb;
    private Vector3 gForce = new Vector3(0f, 0f, 9.8f);

    private bool enteredLeftTube;
    private bool enteredRightTube;

    private Vector3 velocity;
    private Vector3 inVelocity;

    private float tubeDistance;

    public int jumpCounter;

   /* private bool InTube => enteredRightTube && enteredLeftTube;*/

    bool freeFlight = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            velocity = rb.velocity;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ground" && groundEdge.GetComponent<BoxCollider2D>().enabled == true)
        {
            Destroy(gameObject);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(deathParticles, 2f);
        }

        if (collision.tag == "leftTube" && velocity.y < 0)
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

        if (collision.tag == "rightTube" && velocity.y < 0)
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

        if (collision.tag == "obstacle" && GetComponent<CircleCollider2D>().transform.position.y > groundEdge.transform.position.y)
        {
            Destroy(gameObject);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(deathParticles, 2f);
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
            inVelocity = rb.velocity;
        }
        //начальная скорость, которую используем для движения по трубе
        velocity = inVelocity;

        //во время движения по труде нам не нужно влияние физики на мячик
        rb.isKinematic = true;

        freeFlight = false;
    }
    //выход из трубы
    private void ExitTube()
    {
        enteredLeftTube = false;
        enteredRightTube = false;

        //на выходе из трубы снова включаем силы для ригидбоди и задаём начальную скорость, обратную скорости входа
        rb.isKinematic = false;
        rb.velocity = -inVelocity;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "leftTube" && rb.velocity.y > 0 || collision.tag == "rightTube" && rb.velocity.y > 0)
        {
            //включаем свободный полёт только когда мячик уже покинул землю, чтобы не было повторных коллизий с трубой
            jumpCounter++;
            freeFlight = true;
            groundEdge.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
