﻿using UnityEngine;
using UnityEngine.UI;

namespace SingularityGroup.HotReload.Demo {
    class HotReloadDemo : MonoBehaviour {
        public static IDemo Demo {get; set;} = new FallbackDemo();
        
        public GameObject cube;
        public Text informationText;
        public Button openWindowButton;
        public Button openScriptButton;
        public TextAsset thisScript;
        
        void Start() {
            if(Application.isEditor) {
                openWindowButton.onClick.AddListener(Demo.OpenHotReloadWindow);
                openScriptButton.onClick.AddListener(() => Demo.OpenScriptFile(thisScript));
            } else {
                openWindowButton.gameObject.SetActive(false);
                openScriptButton.gameObject.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update() {
            // Edit the vector to rotate the cube in the scene differently or change the speed
            var speed = 100;
            cube.transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * speed);
            
            // Uncomment this code to scale the cube
            // cube.transform.localScale = Mathf.Sin(Time.time) * Vector3.one;
            
            // Uncomment this code to make the cube move from left to right and back
            // var newPos = cube.transform.position += (cube.transform.localScale.x < 0.5 ? Vector3.left : Vector3.right) * Time.deltaTime;
            // if(Mathf.Abs(newPos.x) > 10) {
            //     cube.transform.position = Vector3.zero;
            // }
            
            if (IsHotReloadRunning()) {
                informationText.text = "Hot Reload is running";
            } else {
                informationText.text = "Hot Reload is not running";
            }
        }

        static bool IsHotReloadRunning() => Demo.IsServerRunning();
    }
}
