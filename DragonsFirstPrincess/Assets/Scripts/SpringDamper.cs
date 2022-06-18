using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://theorangeduck.com/page/spring-roll-call
public class SpringDamper
{
    public float Stiffness { get; set; }
    public float Damping { get; set; }
    public float TargetVelocity { get; set; }

    public SpringDamper(float stiffness, float damping)
    {
        Stiffness = stiffness;
        Damping = damping;
    }

    public void Dampen(float value, float velocity, float targetValue, float dt, out float newValue, out float newVelocity)
    {
        newVelocity = velocity + dt * Stiffness * (targetValue - value) + dt * Damping * (TargetVelocity - velocity);
        newValue = value + dt * velocity;
    }
    public void Dampen(Vector2 value, Vector2 velocity, Vector2 targetValue, float dt, out Vector2 newValue, out Vector2 newVelocity)
    {
        newVelocity = new Vector2();
        newValue = new Vector2();

        Dampen(value.x, velocity.x, targetValue.x, dt, out newValue.x, out newVelocity.x);
        Dampen(value.y, velocity.y, targetValue.y, dt, out newValue.y, out newVelocity.y);
    }
    public void Dampen(Vector3 value, Vector3 velocity, Vector3 targetValue, float dt, out Vector3 newValue, out Vector3 newVelocity)
    {
        newVelocity = new Vector3();
        newValue = new Vector3();

        Dampen(value.x, velocity.x, targetValue.x, dt, out newValue.x, out newVelocity.x);
        Dampen(value.y, velocity.y, targetValue.y, dt, out newValue.y, out newVelocity.y);
        Dampen(value.z, velocity.z, targetValue.z, dt, out newValue.z, out newVelocity.z);
    }
}
