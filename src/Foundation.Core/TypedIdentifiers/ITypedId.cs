namespace Foundation.Core.TypedIdentifiers;

public interface ITypedId<T>
{
    public static bool TryParse(string value, out T? result)
    {
        if (Guid.TryParse(value, out var id))
        {
            result = (T)Activator.CreateInstance(typeof(T), id)!;
            return true;
        }

        result = (T)Activator.CreateInstance(typeof(T), Guid.Empty)!;
        return false;
    }
}