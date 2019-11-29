using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;

public class but : MonoBehaviour
{
    [SerializeField] private Text _buttonText;
    [SerializeField] private string _defaultKeyName;
    [SerializeField] private KeyCode _defaultKeyCode; //Клавиша ключа aoaoao

    public KeyCode keyCode { get; set; }
    private IEnumerator coroutine; //магия из раннера - не трогать 
    private string tmpKey;
    public Text buttonText
    { get { return _buttonText; } }
    public string defaultKeyName
    {  get { return _defaultKeyName; } }
    public KeyCode defaultKeyCode
    {  get { return _defaultKeyCode; } }
    public void ButtonSetKey() { // режим ожидания нажатия на клавиатуру
        tmpKey = _buttonText.text;
        _buttonText.text = "???";
        coroutine = Wait();
        StartCoroutine(coroutine);
    }
    //Для ескейпа сделал отмену, а так ждем кнопошку
    IEnumerator Wait() {
        while (true) {
            yield return null; //Это зевс ультует (Хули полез сюда, иди вон) 

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _buttonText.text = tmpKey;
                StopCoroutine(coroutine);
            }

            foreach (KeyCode k in KeyCode.GetValues(typeof(KeyCode))) {
                if (Input.GetKeyDown(k) && !Input.GetKeyDown(KeyCode.Escape)) {
                    keyCode = k;
                    _buttonText.text = k.ToString();
                    StopCoroutine(coroutine);
                }
            }
        }
    }
}
