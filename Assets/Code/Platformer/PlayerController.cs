using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        Rigidbody2D _rigidbody2D;
        public Transform aimPivot;
        public GameObject projectilePrefab;

        public int jumpsLeft;
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKey(KeyCode.A))
            {
                _rigidbody2D.AddForce(Vector2.left * 18f * Time.deltaTime, ForceMode2D.Impulse);
            }

            if (Input.GetKey(KeyCode.D))
            {
                _rigidbody2D.AddForce(Vector2.right * 18f * Time.deltaTime, ForceMode2D.Impulse);
            }

            if(Input.GetMouseButtonDown(0))
            {
                GameObject newProjectile = Instantiate(projectilePrefab);
                newProjectile.transform.position = transform.position;
                newProjectile.transform.rotation = aimPivot.rotation;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(jumpsLeft > 0)
                {
                    jumpsLeft--;
                    _rigidbody2D.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
                }
            }

            Vector3 mousePosition = Input.mousePosition;
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

            float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
            float angleToMouse = radiansToMouse * Mathf.Rad2Deg;


            aimPivot.rotation = Quaternion.Euler(0,0,angleToMouse);
        }

        void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.7f);

                for(int i = 0;i<hits.Length;i++)
                {
                    RaycastHit2D hit = hits[i];

                    if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        jumpsLeft = 2;
                    }
                }
            }
        }
    }

}

