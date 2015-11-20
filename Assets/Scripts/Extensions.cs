/*
 * developer     : brian g. tria
 * creation date : 2015.11.04
 * 
 */

using UnityEngine;
using System.Collections;

public static class Extensions 
{
    public static IntVector2 ToIntVector2 (this Vector2 p_v2)
    {
        return new IntVector2 ((int) p_v2.x, (int) p_v2.y);
    }
}

public struct IntVector2
{
    public int x;
    public int y;
    
    public IntVector2 (int p_x, int p_y)
    {
        x = p_x;
        y = p_y;
    }
    
    public override string ToString ()
    {
        return "(" + this.x + ", " + this.y + ")";
    }
    
    public IntVector2 Sum (IntVector2 p_iv2)
    {
        return new IntVector2 (this.x + p_iv2.x, this.y + p_iv2.y);
    }
}