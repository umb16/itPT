//autogenerated
public enum LocKeys
{
    //Открыть
    Open,
    //Использовать
    Use,
    //Взять
    Take,
    //Фомка
    Crobaw,
    //Ящик
    Box,
    //Коробка
    Box2,
}
public static class LocKeyConverter
{
    public static string Convert(LocKeys key)
    {
        switch (key)
        {
            case LocKeys.Open:
                return "Open";
            case LocKeys.Use:
                return "Use";
            case LocKeys.Take:
                return "Take";
            case LocKeys.Crobaw:
                return "Crobaw";
            case LocKeys.Box:
                return "Box";
            case LocKeys.Box2:
                return "Box2";
                default: return "";
        }
    }
}