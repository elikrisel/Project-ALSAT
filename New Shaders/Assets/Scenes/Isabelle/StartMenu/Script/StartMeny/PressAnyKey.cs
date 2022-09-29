using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PressAnyKey : MonoBehaviour
{
 
        public GameObject menuLogic;
        private bool hasClicked = false;
        [SerializeField] GameObject[] characters;
        [Range(0,10)]
        [SerializeField] float rate_of_invisiblity;
    // Vid mus nere så spawnar den och laddar till menulogic script och gör det menulogic säger
        private void OnMouseDown()
        {
         
            if (hasClicked == false)
            {
                menuLogic.GetComponent<MenuLogic>().SpawnMenu();
                hasClicked = true;
            }

        
            

        }

    void make_characters_invisible() {

        for (int i = 0; i < characters.Length; i++) {
          var mat=  characters[i].GetComponent<Renderer>().material;
            mat.SetFloat("_Invisible", Mathf.Lerp(mat.GetFloat("_Invisible"), 0, Time.deltaTime * rate_of_invisiblity)); 
        }

    }

    // Om man trycker på en knappt så kör den som ovan
        void Update()
        {

            if (Input.anyKeyDown)
            {

                if (hasClicked == false)
                {
                    menuLogic.GetComponent<MenuLogic>().SpawnMenu();
                    hasClicked = true;
                }

            
        }
        if (characters[0] == null)
        {
            hasClicked = false;
        }
        if (hasClicked) {
            make_characters_invisible();
        }

    }
}
