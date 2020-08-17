/*
Gerstner Wave formuls from
https://developer.nvidia.com/gpugems/gpugems/part-i-natural-effects/chapter-1-effective-water-simulation-physical-models
*/


void _GerstnerWave_Add(
float3 vertexPosition, float2 direction,
float waveLen, float amplitude, float stepness, float speed,
    inout float3 position,
    inout float3 tangent,
    inout float3 normal)
{
    const static float M_PI = 3.14159274;
    const static float WAVES_COUNT = 3;
    float time = _Time.y;
    float g = 9.8;
    float w = sqrt(g * (2 * M_PI / waveLen));
    float wa = w * amplitude;
    
    float q = stepness / (wa * WAVES_COUNT);
    float xArg = w * dot(direction, vertexPosition.xz) + speed * time;
    
    //position(checked)
    float px = q * amplitude * direction.x * cos(xArg);
    float pz = q * amplitude * direction.y * cos(xArg);
    float py = amplitude * sin(xArg);
    position += float3(px, py, pz);
    //tangent(checked)
    float tx = -q * direction.x * direction.x * wa * sin(xArg);
    float tz = -q * direction.x * direction.y * wa * sin(xArg);
    float ty = direction.x * wa * cos(xArg);
    tangent += float3(tx, ty, tz);
    //normal(checked)
    float nx = -direction.x * wa * cos(xArg);
    float nz = -direction.y * wa * cos(xArg);
    float ny = -q * wa * sin(xArg);
    normal += float3(nx, ny, nz);
}
void GerstnerWave_float(float3 vertexPosition, 
float2 direction1, float4 params1,
float2 direction2, float4 params2,
float2 direction3, float4 params3,
    out float3 position,
    out float3 tangent,
    out float3 normal)
{
    position = float3(vertexPosition.x, vertexPosition.y, vertexPosition.z);
    tangent = float3(1, 0, 0);
    normal = float3(0, 1, 0);
    
    _GerstnerWave_Add(vertexPosition, direction1, 
     params1.x, params1.y, params1.z, params1.w,
     position, tangent, normal);
    _GerstnerWave_Add(vertexPosition, direction2, 
     params2.x, params2.y, params2.z, params2.w,
     position, tangent, normal);
    _GerstnerWave_Add(vertexPosition, direction3, 
     params3.x, params3.y, params3.z, params3.w,
     position, tangent, normal);
    
}

void GerstnerWave_Raw_float(float3 vertexPosition,
float2 direction1, float4 params1,
float2 direction2, float4 params2,
float2 direction3, float4 params3,
    out float3 position,
    out float3 tangent,
    out float3 normal)
{
    position = float3(0, 0, 0);
    tangent = float3(0, 0, 0);
    normal = float3(0, 0, 0);
    
    _GerstnerWave_Add(vertexPosition, direction1, params1.x, params1.y, params1.z, params1.w,
     position, tangent, normal);
    _GerstnerWave_Add(vertexPosition, direction2, params2.x, params2.y, params2.z, params2.w,
     position, tangent, normal);
    _GerstnerWave_Add(vertexPosition, direction3, params3.x, params3.y, params3.z, params3.w,
     position, tangent, normal);
    
}