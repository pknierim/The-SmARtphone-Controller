using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObjectParameters
{
    [SerializeField]
    public ObjectType Type;

    [SerializeField]
    public ObjectQuality Quality;

    [Range(0, 100)]
    [SerializeField]
    public int Size;

    [SerializeField]
    public Color Color;

    public override bool Equals(object obj)
    {
        if (!(obj is ObjectParameters))
        {
            return false;
        }

        ObjectParameters structToCompare = (ObjectParameters)obj;

        if (this.Type == structToCompare.Type
            && this.Quality == structToCompare.Quality
            && this.Size == structToCompare.Size
            && this.Color == structToCompare.Color)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public static bool operator ==(ObjectParameters obj1, ObjectParameters obj2)
    {
        if (obj1.Type == obj2.Type
            && obj1.Quality == obj2.Quality
            && obj1.Size == obj2.Size
            && obj1.Color == obj2.Color)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator !=(ObjectParameters obj1, ObjectParameters obj2)
    {
        if (obj1.Type == obj2.Type
            && obj1.Quality == obj2.Quality
            && obj1.Size == obj2.Size
            && obj1.Color == obj2.Color)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override int GetHashCode()
    {
        var hashCode = 170851647;
        hashCode = hashCode * -1521134295 + Type.GetHashCode();
        hashCode = hashCode * -1521134295 + Quality.GetHashCode();
        hashCode = hashCode * -1521134295 + Size.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(Color);
        return hashCode;
    }
}

public enum ObjectType
{
    Teapot,
    Bunny,
    Monkey,
    Dragon,
    Buddha,
    Angel
}

public enum ObjectQuality
{
    High, Medium, Low
}
