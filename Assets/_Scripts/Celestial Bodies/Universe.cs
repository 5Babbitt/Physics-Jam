using System;

public class Universe
{
    public static float gravitationalConstant = 0.001f;
    public static float physicsTimeStep = 0.01f;

    public static Action<GravityBody> OnGravityBodyDestroyed;
    public static Action<GravityBody> OnGravityBodyAdded;
}
