using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationDB
{
    Dictionary<string, string> _db = new Dictionary<string, string>();

    public LocalizationDB(Dictionary<string, string> db)
    {
        _db = db;
    }

    public string GetPhrase(LocKeys key)
    {
        if(_db == null)
            return "###localization string not found###";
        if (_db.TryGetValue(LocKeyConverter.Convert(key), out string value))
            return value;
        return "###localization string not found###";
    }
}
