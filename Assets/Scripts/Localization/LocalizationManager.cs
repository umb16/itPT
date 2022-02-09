using System;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    [SerializeField] TextAsset _textAsset;

    public Action LocalizationChanged;
    public LocalizationDB LocalizationDB;

    public string GetPhrase(LocKeys key) => LocalizationDB.GetPhrase(key);

    private void Awake()
    {
        LocalizationDB = new LocalizationDB(JavaLangStrings.ConverJavaLocalToDict(_textAsset.text));
    }

    private void OnLocalizationChanged()
    {
        LocalizationChanged?.Invoke();
    }
}
