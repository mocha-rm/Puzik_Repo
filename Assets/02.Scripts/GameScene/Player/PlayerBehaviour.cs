using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene.Rule;


namespace GameScene.Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        //Player 의 부품 중 1

        private Vector3 LeftScale = new Vector3(-1f, 1f, 1f);


        private float _maxSpeed;

        private float _speed;

        private Animator ani;


        private Coroutine routine = null;


        public void Init(float maxSpeed, Vector3 pos)
        {
            ani = GetComponent<Animator>();

            _maxSpeed = maxSpeed;

            transform.position = pos;

            if (Random.Range(0f, 1f) > 0.5f)
            {
                _speed = maxSpeed;
            }
            else
            {
                _speed = -maxSpeed;
            }

            transform.localScale = _speed.Equals(maxSpeed) ? Vector3.one : LeftScale;

            Debug.Log($"Player Initialized");
        }


        public void Clear()
        {
            Debug.Log($"Player Cleared");
        }


        public void Move()
        {
            ani.SetBool("Run", true);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }

        public void SetDirection() //UI 버튼이 조작해야 되는 것들은 BloC?
        {
            if (routine != null)
            {
                StopCoroutine(routine);
            }

            routine = StartCoroutine(_speed > 0 ? IcyTurnLeft() : IcyTurnRight());
        }



        private IEnumerator IcyTurnLeft()
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);

            float controlValue = 0.05f;

            while (true)
            {
                _speed -= controlValue;
                if (_speed < -_maxSpeed)
                {
                    break;
                }
                yield return null;
            }

            yield return null;
        }

        private IEnumerator IcyTurnRight()
        {
            transform.localScale = Vector3.one;

            float controlValue = 0.05f;

            while (true)
            {
                _speed += controlValue;
                if (_speed > _maxSpeed)
                {
                    break;
                }
                yield return null;
            }

            yield return null;
        }

        public void SuperSlide() //필살기
        {
            ani.SetTrigger("SuperSlide");
            //Rigidbody 2D 이용해서 옆으로 뛰는 느낌 주면 좋을 듯
        }
    }
}

