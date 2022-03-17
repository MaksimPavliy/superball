using FriendsGamesTools;
using SplineMesh;
using Superball;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Superball
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private GameObject groundEdge;
        [SerializeField] private ParticleSystem _deathParticles;
        private Pipe previousPipe;
        public Pipe currentPipe = null;
        private CurveSample sample;
        private Rigidbody2D _rigidbody;
        private Vector3 gForce = new Vector3(0f, 0f, 9.8f);
        private bool _inTube = false;
        private Vector3 velocity;
        private Vector3 inVelocity;
        private Vector3 _localGravity;

        private float _tubeDistance;

        public int jumpCounter;

        /* private bool InTube => enteredRightTube && enteredLeftTube;*/

        public event Action JumpSucceded;

        private Vector2 tempVelocity;

        private SuperballGeneralConfig _config => SuperballGeneralConfig.instance;
        private float _sensitivityTouch => _config.sensitivityTouch;
        public float maxOffset = 5.4f;
        private int _tubeMoveDirectionSign = 1;
        private float _tubeSpeed;

        private bool _touchingTheEntrance = false;
        private void Start()
        {
            Joystick.instance.Dragged += OnDragged;
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.simulated = false;
            EventSignature();
        }

        private void EventSignature()
        {
            GameManager.instance.PlayPressed.AddListener(OnPlay);
            GameManager.instance.OnLevelLost.AddListener(LevelDone);
        }

        public void OnPlay()
        {
            _rigidbody.simulated = true;
        }

        public void LevelDone()
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
            GameManager.instance.OnLevelLost.RemoveListener(LevelDone);
        }

        private void Update()
        {
            //вручную обрабатываем движение, если мячик находится внутри трубы
            if (_inTube)
            {
                //берём семпл на заданной дистанции
                sample = currentPipe.GetSampleAtDistance(_tubeDistance);

                var sampleWorldPosition=currentPipe.GetSampleWorldPosition(sample);
                var sampleWorldDirection = currentPipe.GetSampleWorldDirection(sample);
                //двигаем мячик к заданному семплу
                transform.position = sampleWorldPosition;

                //находим вектор направления движения в текущем семпле
                var direction = sampleWorldDirection;

                //вычисляем ускорение, как проекцию силы тяжести на направление движения
                //  var acceleration = Vector3.Project(gForce, direction).z;
                //TODO make length dependency;

                var acceleration = Vector3.Project(_localGravity, direction);

                //   acceleration = _localGravity* _tubeMoveDirectionSign *(_tubeDistance>=currentPipe.Length/2f?1:-1);
                // acceleration *= (_tubeDistance >= currentPipe.Length / 2f ? 1 : -1);
                //if (_tuveMoveDirectionSign>1)
                //    acceleration = direction.z > 0 ? -acceleration : acceleration;

                //else
                //    acceleration = direction.z > 0 ? acceleration : -acceleration;

                //изменяем скорость, используя ускорение
                //     velocity += new Vector3(0f, acceleration * Time.deltaTime, 0f);
                velocity += acceleration * Time.deltaTime;// new Vector3(0f, acceleration * Time.deltaTime, 0f);
                Debug.Log($"{acceleration} {velocity}");
              //  velocity = direction * _tubeSpeed;
                //определяем дистанцию для вычислеия следующего семпла
                //приплюсовываем если влетел слева и отнимаем, если влетел справа
                _tubeDistance += velocity.magnitude * Time.deltaTime * _tubeMoveDirectionSign;
                _tubeDistance = Mathf.Clamp(_tubeDistance, 0, currentPipe.Length);
                //если дистанция выходит за границы сплайна, покидаем трубу
                if (_tubeDistance >= currentPipe.Length || _tubeDistance <= 0)
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
            _touchingTheEntrance = false;
               tempVelocity = _rigidbody.velocity;
            
            if (!_inTube)
            {
                var position = transform.position + Vector3.right * Joystick.instance.dragDir.x * _sensitivityTouch * Time.deltaTime*20f;
                /*position.x = Mathf.Clamp(position.x, -maxOffset * 1.5f, maxOffset * 1.5f);*/
                transform.position = position;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("pipeEntrance"))
            {
                _touchingTheEntrance = true;
            }
        }
      
        private void OnTriggerEnter2D(Collider2D collision)
        {   
            //if (collision.CompareTag("ground") && groundEdge.GetComponent<BoxCollider2D>().enabled == true)
            //{
            //    GameManager.instance.OnLose();
            //    Joystick.instance.Dragged -= OnDragged;
            //}

            if (collision.CompareTag("pipeEntrance"))// && velocity.y < 0)
            {
                if (!currentPipe)
                {
                    //входим в трубу только если мы ещё не в ней
                    EnterTube(collision);
                    //при входе в левую трубу стартовая дистанция 0

                }
            }

            //if (collision.CompareTag("rightTube") && velocity.y < 0)
            //{
            //    //входим в трубу только если мы ещё не в ней
            //    if (freeFlight)
            //    {
                    
            //        EnterTube(collision);
            //        //при входе в правую трубу стартовая дистанция равна длине трубы
            //        tubeDistance = currentPipe.Length - 0.01f;
                   
            //    }
            //}

            if (collision.CompareTag("obstacle") && GetComponent<CircleCollider2D>().transform.position.y > groundEdge.transform.position.y)
            {
                GameManager.instance.OnLose();
                Joystick.instance.Dragged -= OnDragged;
            }
        }

        private void OnDragged(Vector2 dir)
        {
            //if (!enteredLeftTube || !enteredRightTube)
            //{
            //    var position = transform.position + Vector3.right * dir.x * _sensitivityTouch * Time.deltaTime;
            //    /*position.x = Mathf.Clamp(position.x, -maxOffset * 1.5f, maxOffset * 1.5f);*/
            //    transform.position = position;
            //}
        }

        //входим в трубу
        private void EnterTube(Collider2D collision)
        {
            var pipeEntrance=collision.gameObject.GetComponent<PipeEntrance>();
            _localGravity = _rigidbody.velocity.normalized * gForce.magnitude;

            previousPipe = currentPipe;
            currentPipe = pipeEntrance.Pipe;
            _tubeMoveDirectionSign = pipeEntrance.DirectionSign;
            _inTube = true;
            _tubeDistance = _tubeMoveDirectionSign > 0 ? 0 : currentPipe.Length;
            //здесь можно было бы сразу определять, с какой стороны мы влетели в трубу, определив дистанцию ближайшего семпла
            //но для этого нужно немного подтюнить плагин, а нам это сейчас не надо.
            groundEdge.GetComponent<BoxCollider2D>().enabled = false;
            _tubeSpeed = _rigidbody.velocity.magnitude;

            //костыль - скорость влёта в трубу определяем только в первый раз и используем её дальше для входа и для выхода
            //сделано это для того, чтобы избежать погрешностей, которые полюбому возникнут из-за того, что при входе и выходе
            //мячик никогда не будет чётко в одной и той же точке и скорость будет немного теряться
            //if (previousPipe != currentPipe)
            //{
            //    inVelocity = new Vector3(0, -8, 0);

            //}
            //начальная скорость, которую используем для движения по трубе
          //  inVelocity = new Vector3(0, -8, 0);
            velocity = _rigidbody.velocity;

            //во время движения по труде нам не нужно влияние физики на мячик
            _rigidbody.isKinematic = true;

            ScoreManager.instance.UpdateScore();
        }
        IEnumerator AwaitLeavingPipe()
        {
            yield return new WaitUntil(() => !_touchingTheEntrance);
            Debug.Log("LEaving Pipe");
            currentPipe = null;
        }
        //выход из трубы
        private void ExitTube()
        {
            _inTube = false;

            //на выходе из трубы снова включаем силы для ригидбоди и задаём начальную скорость, обратную скорости входа
            _rigidbody.isKinematic = false;
            //   inVelocity += inVelocity * 0.2f;
            _rigidbody.velocity = velocity * _tubeMoveDirectionSign * 1.1f;
            StartCoroutine(AwaitLeavingPipe());
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            //if (collision.CompareTag("pipeEntrance") && _rigidbody.velocity.y > 0)
            //{
            //    //включаем свободный полёт только когда мячик уже покинул землю, чтобы не было повторных коллизий с трубой
            //    // jumpCounter++;
            //    JumpSucceded?.Invoke();
            //    freeFlight = true;
            //    groundEdge.GetComponent<BoxCollider2D>().enabled = true;
            //}
        }
    }
}