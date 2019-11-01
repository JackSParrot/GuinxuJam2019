using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject Menu;
    public DialogController dc;
    public GameObject panel2;
    public GameObject loading;


    public void Quit()
    {
        Application.Quit();
    }


    public void Play()
    {
        StartCoroutine(playct());
        Menu.gameObject.SetActive(false);
    }

    IEnumerator playct()
    {
        dc.PlaySequence(new List<MessageInfo>
        {
            new MessageInfo{ leftImage = 0, rightImage = -1, message = "Hola Soy Manolo, un aventurero con poca suerte. He decidido recorrer el mundo a ver si consigo algo aparte de llevarme collejas de mi hermano mayor."},
            new MessageInfo{ leftImage = 0, rightImage = -1, message = "He decidido que voy a ser el guerrero mas grande que jamás haya existido, aunque de momento no me va muy bien, en todos sitios que he ofrecido mi ayuda se han reido de mi"},
            new MessageInfo{ leftImage = -1, rightImage = 1, message = "Hola joven, no he podido evitar escucharte. ¿Con quién hablas?"},
            new MessageInfo{ leftImage = 0, rightImage = -1, message = "Con el jugador que va a probar este experimento que ha hecho JackSParrot para la GameJam de Guinxu."},
            new MessageInfo{ leftImage = -1, rightImage = 1, message = "Si, claro. Tiene mucho sentido(Este tipo está loco)"},
            new MessageInfo{ leftImage = -1, rightImage = 1, message = "He podido ver que tienes la constitucion ósea idónea para convertirte en un gran maestro"},
            new MessageInfo{ leftImage = 0, rightImage = -1, message = "Si ¿Verdad? Voy a convertirme en el guerrero mas grande de todos los tiempos"},
            new MessageInfo{ leftImage = -1, rightImage = 1, message = "(Desde luego que está loco)"},
            new MessageInfo{ leftImage = -1, rightImage = 1, message = "Pues estás de suerte, justo tengo conmigo la varita del gran mago Marlín, que murió hace ya un milenio. Ha sido el mago mas grande de todos los tiempos, y todos sus poderes están en esta varita, y aquel que la posea, se convertirá en el hechicero mas grande del mundo."},
            new MessageInfo{ leftImage = -1, rightImage = 1, message = "Como veo que tienes madera de heroe, te la dejo por el módico precio de 1000 piezas de oro..."},
            new MessageInfo{ leftImage = 0, rightImage = -1, message = "Pero juntando todo lo que tengo solo me llega a 50 piezas de oro :("},
            new MessageInfo{ leftImage = -1, rightImage = 1, message = "No importa, eso valdrá. No voy a interponerme en tu destino. Tu estás destinado a ser el mejor(y el mas primo xD te voy a pelar)."},
            new MessageInfo{ leftImage = 0, rightImage = -1, message = "De acuerdo, toma, todo mi dinero."},
            new MessageInfo{ leftImage = -1, rightImage = 1, message = "Ten, aquí tienes la varita..."}
        });
        do
        {
            yield return null;
        }
        while (dc.gameObject.activeSelf);
        panel2.SetActive(true);
        dc.PlaySequence(new List<MessageInfo>
        {
            new MessageInfo{ leftImage = 0, rightImage = -1, message = "Bien!! ahora seguro que me dan trabajo. Mira, ahí hay un pueblo."},
            new MessageInfo{ leftImage = 0, rightImage = -1, message = "Hola buenas gentes de este pueblo."},
            new MessageInfo{ leftImage = 0, rightImage = -1, message = "Soy el gran archimago Marlín, y he venido a ofreceros mis servicios.  ¿En qué puede ayudaros el archimago mas grande del mundo?"},
            new MessageInfo{ leftImage = -1, rightImage = 5, message = "Por fin alguien que nos puede ayudar, en el mausoleo del pueblo ha aparecido una horda de no muertos que no nos deja dormir. Necesitamos que os encarguéis de ellos oh gran Marlín."},
            new MessageInfo{ leftImage = 0, rightImage = -1, message = "Mi tarifa es de 1000 monedas de oro!!! ¿Podréis pagar al gran Marlin?"},
            new MessageInfo{ leftImage = -1, rightImage = 5, message = "Eso es practicamente la mitad del oro del pueblo. Pero si conseguís limpiar la cripta, os lo pagaremos, gran hechicero."},
            new MessageInfo{ leftImage = 0, rightImage = -1, message = "Pues directo a la mazmorra, volveré enseguida."}
        });
        do
        {
            yield return null;
        }
        while (dc.gameObject.activeSelf);
        loading.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene("gameplay");
    }
}
