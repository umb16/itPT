using UnityEngine;

public static class Vector3Extensions
{
    public static bool EqualsWithThreshold(this Vector3 v1, Vector3 v2, float threshold)
    {
        if (Mathf.Abs(v1.x - v2.x) < threshold && Mathf.Abs(v1.y - v2.y) < threshold)
        {
            return Mathf.Abs(v1.z - v2.z) < threshold;
        }
        return false;
    }

    public static Vector3 Abs(this Vector3 v)
    {
        return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    }

    public static float DiamondAngleXZ4(this Vector3 v)
    {
        if (v.z >= 0f)
        {
            if (!(v.x >= 0f))
            {
                return 1f - v.x / (0f - v.x + v.z);
            }
            return v.z / (v.x + v.z);
        }
        if (!(v.x < 0f))
        {
            return 3f + v.x / (v.x - v.z);
        }
        return 2f - v.z / (0f - v.x - v.z);
    }

    public static float DiamondAngleXZ4(this Vector3 v1, Vector3 v2)
    {
        float num = DiamondAngleXZ4(v1);
        float num2 = DiamondAngleXZ4(v2) - num;
        if (num2 < 0f)
        {
            num2 += 4f;
        }
        return num2;
    }

    public static float DiamondAngleXZSign2(this Vector3 v1, Vector3 v2)
    {
        float num = DiamondAngleXZ4(v1, v2);
        if (num > 2f)
        {
            num = 0f - (4f - num);
        }
        return num;
    }

    public static float DiamondAngleXZ2(this Vector3 v1, Vector3 v2)
    {
        float num = DiamondAngleXZ4(v1);
        float num2 = DiamondAngleXZ4(v2);
        float num3 = (!(num < num2)) ? (num - num2) : (num2 - num);
        if (num3 > 2f)
        {
            num3 = 4f - num3;
        }
        return num3;
    }

    public static float SqrMagnitudeXZ(this Vector3 v)
    {
        return v.x * v.x + v.z * v.z;
    }

    public static float DotXZ(this Vector3 v1, Vector3 v2)
    {
        return v1.x * v2.x + v1.z * v2.z;
    }

    public static Vector3 RotateXZ(this Vector3 v, Vector3 rotator)
    {
        v = new Vector3(rotator.x, v.y, rotator.z) * v.x + new Vector3(rotator.z, v.y, 0f - rotator.x) * v.z;
        return v;
    }

    public static Vector3 DiamondRotateXZ(this Vector3 v, float angle)
    {
        return RotateXZ(v, DiamondToNormalVectorXZ(angle));
    }

    public static Vector3 RotateXZ90(this Vector3 v)
    {
        return new Vector3(v.z, v.y, 0f - v.x);
    }

    public static Vector3 Min(this Vector3 v1, Vector3 v2)
    {
        if (v1.x > v2.x)
        {
            v1.x = v2.x;
        }
        if (v1.y > v2.y)
        {
            v1.y = v2.y;
        }
        if (v1.z > v2.z)
        {
            v1.z = v2.z;
        }
        return v1;
    }

    public static Vector3 Max(this Vector3 v1, Vector3 v2)
    {
        if (v1.x < v2.x)
        {
            v1.x = v2.x;
        }
        if (v1.y < v2.y)
        {
            v1.y = v2.y;
        }
        if (v1.z < v2.z)
        {
            v1.z = v2.z;
        }
        return v1;
    }

    public static Vector3 DiamondToVectorXZ(float angle)
    {
        float x = (angle < 2f) ? (1f - angle) : (angle - 3f);
        float z = (!(angle < 3f)) ? (angle - 4f) : ((angle > 1f) ? (2f - angle) : angle);
        return new Vector3(x, 0f, z);
    }

    public static Vector3 DiamondToNormalVectorXZ(float angle)
    {
        return NormalizedXZ(DiamondToVectorXZ(angle));
    }

    public static Vector3 NormalizedXZ(this Vector3 v, bool clearY = false)
    {
        float num = Mathf.Sqrt(v.x * v.x + v.z * v.z);
        v.x /= num;
        v.z /= num;
        if (clearY)
        {
            v.y = 0f;
        }
        return v;
    }

    public static float MagnetudeXZ(this Vector3 v)
    {
        return Mathf.Sqrt(v.x * v.x + v.z * v.z);
    }
}
