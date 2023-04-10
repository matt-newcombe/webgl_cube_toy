using System;
using UnityEngine;
using Unity.Mathematics;

// Tweening with the critically damped spring model
// http://mathproofs.blogspot.com/2013/07/critically-damped-spring-smoothing.html

// There isn't a suitable CDSpring<T> generic implementation as:

// 1.) Generic<T> Math operators are not supported until .NET 7
// 2.) dynamic key operator acts like 'object' which means value
// types are boxed and therefore Step becomes a GC producing operation
[Serializable]
public class CDSpringFloat
{
    public float Pos;
    public float Velocity;
    public float Omega;

    public CDSpringFloat(float pos, float omega)
    {
        Pos = pos;
        Velocity = 0f;
        Omega = omega;
    }
    
    public void MoveTowards(float G)
    {
        // Variables follow formula convention from
        // linked math article
        float dt = Time.deltaTime;
        float xn = Pos;
        float xDn = Velocity; // First derivative
        float w_dt = Omega * dt;
        float wSQ_dt = Omega * Omega * dt;

        float numerator = xDn - wSQ_dt * (xn - G);
        float denominator = (1 + w_dt) * (1 + w_dt);
        
        // Set new velocity
        Velocity = numerator / denominator;
        
        // Set new position
        Pos += dt * Velocity;
    }
}

// Unity.Mathematics for potential SIMD optimisations
public struct CDSpringVector3
{
    public float3 Pos;
    public float3 Velocity;
    public float Omega;

    public CDSpringVector3(Vector3 pos, float omega)
    {
        Pos = pos;
        Velocity = Vector3.zero;
        Omega = omega;
    }

    public void MoveTowards(float3 G)
    {
        float dt = Time.deltaTime;
        float3 xn = Pos;
        float3 xDn = Velocity; // First derivative
        float w_dt = Omega * dt;
        float wSQ_dt = Omega * Omega * dt;

        Vector3 numerator = xDn - wSQ_dt * (xn - G);
        float denominator = (1 + w_dt) * (1 + w_dt);
        
        // Set new velocity
        Velocity = numerator / denominator;
        
        // Set new position
        Pos += dt * Velocity;
    }
}