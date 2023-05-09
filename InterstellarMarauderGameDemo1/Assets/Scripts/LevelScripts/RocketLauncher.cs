using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//namespace MyGame.LevelScripts
//{

    public class RocketLauncher : MonoBehaviour
    {
        public int powerUpLevelRequirement = 0;

        public Rocket rocket;
        Vector2 direction;

        public bool autoShootRocket = false;
        public float shootRocketIntervalSeconds = 0.5f;
        public float shootRocketDelaySeconds = 0.0f;
        float shootRocketTimer = 0f;
        float delayRocketTimer = 0f;

        public bool isActive = false;
        bool rocketfire;



        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

            if (!isActive)
            {
                return;
            }

            direction = (transform.localRotation * Vector2.right).normalized;

            if (autoShootRocket)
            {
                if (delayRocketTimer >= shootRocketDelaySeconds)
                {
                    if (shootRocketTimer >= shootRocketIntervalSeconds)
                    {
                        ShootRocket();
                        shootRocketTimer = 0;
                    }
                    else
                    {
                        shootRocketTimer += Time.deltaTime;
                    }
                }
                else
                {
                    delayRocketTimer += Time.deltaTime;
                }
            }
        }

        public void ShootRocket()
        {
           
            
                GameObject go = Instantiate(rocket.gameObject, transform.position, Quaternion.identity);
                Rocket goRocket = go.GetComponent<Rocket>();
                goRocket.direction = direction;
     
            
        }
    }
//}

