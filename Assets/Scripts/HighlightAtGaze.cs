using Tobii.G2OM;
using UnityEngine;
using UnityEngine.UI;

namespace Tobii.XR.Examples.DevTools
{
    //Monobehaviour which implements the "IGazeFocusable" interface, meaning it will be called on when the object receives focus
    public class HighlightAtGaze : MonoBehaviour, IGazeFocusable
    {
        private static readonly int _baseColor = Shader.PropertyToID("_BaseColor");
        public Color highlightColor = Color.red;
        public float animationTime = 0.1f;
        private Renderer _renderer;
        private Color _originalColor;

        private GameObject[] pontos;
        private Color _targetColor;
        private bool itsFocused = false;
        private float timerCountStart;
        private int i = 0; //needed to progress the game

        //The method of the "IGazeFocusable" interface, which will be called when this object receives or loses focus

        public void GazeFocusChanged(bool hasFocus)
        {

            if (hasFocus)
            {
                _targetColor = highlightColor;
            }
            //If this object lost focus, fade the object's color to it's original color
            else
            {
                _targetColor = _originalColor;
            }
            //If this object received focus, fade the object's color to highlight color
        }

        private void Start()
        {
            _renderer = GetComponent<Renderer>(); //get component from gameobject associated
            _originalColor = _renderer.material.color; //get color from material associated to component renderer 
            _targetColor = _originalColor; //set value of _targetColor to _originalColor 

        }

        private void Update()
        {
            ExerciseTrail();

            if (!itsFocused)
            {
                //This lerp will fade the color of the object
                changeColors(_targetColor);
            }
        }

        public void ExerciseTrail()
        {
            GameObject[] points = GameObject.FindGameObjectsWithTag("Ponto");
            if (TobiiXR.FocusedObjects.Count > 0 && TobiiXR.FocusedObjects[0].GameObject.name == points[i].name) //Tobii is focusing on one object and the object's name is Ponto i(Clone)
            {
                changeColors(Color.white);
                itsFocused = true; //Focused
                timerCountStart += Time.deltaTime; //Count to 2 seconds
                points[i].transform.GetChild(0).GetComponentInChildren<Image>().fillAmount = timerCountStart / 2; //Fill up the progress circle                                                                                             
                if (timerCountStart >= 2) //If 2 seconds have passed
                {
                    points[i].transform.GetChild(0).GetComponentInChildren<Image>().fillAmount = 0; //Reset the progress circle

                    if (points[i].gameObject.GetComponent<HighlightAtGaze>().enabled) //Check to see if script is enabled on the gameobject
                    {
                        points[i].gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green); //Set the ponto's color to green to display sucess
                        points[i].gameObject.GetComponent<HighlightAtGaze>().enabled = !points[i].gameObject.GetComponent<HighlightAtGaze>().enabled; //Deactivate script on the gameobject
                    }
                    timerCountStart = 0; //Reset Timer
                    i++; //Next ponto
                    itsFocused = false; //Not focused
                }
            }
            else
            {
                points[i].transform.GetChild(0).GetComponentInChildren<Image>().fillAmount = 0; //Reset the targeted gameobject's progress circle
                timerCountStart = 0; //Reset timer
                itsFocused = false; //Not focused
            }
        }

        public void changeColors(Color _targetColor)
        {
            if (_renderer.material.HasProperty(_baseColor)) // new rendering pipeline (lightweight, hd, universal...)
            {
                _renderer.material.SetColor(_baseColor, Color.Lerp(_renderer.material.GetColor(_baseColor), _targetColor, Time.deltaTime * (1 / animationTime)));
            }
            else // old standard rendering pipline
            {
                _renderer.material.color = Color.Lerp(_renderer.material.color, _targetColor, Time.deltaTime * (1 / animationTime));
            }
        }
    }
}

