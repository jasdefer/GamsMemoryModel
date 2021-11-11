namespace GamsMemoryModel;

/// <summary>
/// Represents a key for any gams symbol.
/// It is just a set of strings which uniquely identifies a gams symbol.
/// </summary>
public class GamsKey : IEquatable<GamsKey>
{
    /// <summary>
    /// Create a new gams key.
    /// </summary>
    /// <param name="keys">A collection of keys.</param>
    [JsonConstructor]
    public GamsKey(IEnumerable<string> keys)
    {
        Keys = keys.ToArray();
    }

    /// <summary>
    /// Create a new gams key.
    /// </summary>
    /// <param name="keys">A collection of keys.</param>
    public GamsKey(params string[] keys)
    {
        Keys = keys.ToArray();
    }

    /// <summary>
    /// The collection of keys.
    /// </summary>
    public IReadOnlyList<string> Keys { get; }

    /// <summary>
    /// The dimension of the keys.
    /// </summary>
    [JsonIgnore]
    public int Length => Keys.Count;

    /// <summary>
    /// Return the key at the given index.
    /// </summary>
    /// <param name="index">The index of the requested key.</param>
    /// <returns>Returns the requested key.</returns>
    public string this[int index]
    {
        get => Keys[index];
    }

    #region overrides
    /// <summary>
    /// Check if this key is equal to another object.
    /// </summary>
    /// <param name="obj">The object to which this <see cref="GamsKey"/> is compared.</param>
    /// <returns>True, if all keys are equal. False otherwise.</returns>

    public override bool Equals(object obj)
    {
        return Equals(obj as GamsKey);
    }

    /// <summary>
    /// Check if this key is equal to another <see cref="GamsKey"/>.
    /// </summary>
    /// <param name="other">The <see cref="GamsKey"/> to which this <see cref="GamsKey"/> is compared.</param>
    /// <returns>True, if all keys are equal. False otherwise.</returns>
    public bool Equals(GamsKey other)
    {
        if (other is null ||
            other.Length != Length)
        {
            return false;
        }
        for (int i = 0; i < Length; i++)
        {
            if (other[i] != this[i])
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Check if two <see cref="GamsKey"/> are eqaul.
    /// </summary>
    /// <param name="left">The first <see cref="GamsKey"/>.</param>
    /// <param name="right">The second <see cref="GamsKey"/>.</param>
    /// <returns>True, if all keys are equal. False otherwise.</returns>
    public static bool operator ==(GamsKey left, GamsKey right)
    {
        return EqualityComparer<GamsKey>.Default.Equals(left, right);
    }

    /// <summary>
    /// Check if two <see cref="GamsKey"/> are not eqaul.
    /// </summary>
    /// <param name="left">The first <see cref="GamsKey"/>.</param>
    /// <param name="right">The second <see cref="GamsKey"/>.</param>
    /// <returns>False, if all keys are equal. True otherwise.</returns>
    public static bool operator !=(GamsKey left, GamsKey right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Get a mostly unique integer for this set of keys.
    /// </summary>
    /// <returns>Returns a mostly unique integer for this set of keys.</returns>
    public override int GetHashCode()
    {
        var hascode = Length.GetHashCode();
        for (int i = 0; i < Length; i++)
        {
            hascode = HashCode.Combine(hascode, this[i]);
        }
        return hascode;
    }

    /// <summary>
    /// Convert this <see cref="GamsKey"/> as a string.
    /// </summary>
    /// <returns>Returns all keys separated by a semicolon ';'.</returns>
    public override string ToString()
    {
        return string.Join(';', Keys);
    }
    #endregion
}
