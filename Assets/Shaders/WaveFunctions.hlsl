float ssin(float x)
{
    return 1 - 2 * abs(sin(x));
}
float ddxssin(float x)
{
    return (-sin(2 * x) / abs(sin(x)));
}

void WaveFuntion2_float(float2 xz, float2 dir, float height, float minHeight, 
float count, float speed, float faza, float time, out float waveHeight)
{
    float time2 = min(time, minHeight);
    float2 ortoDir = float2(dir.y, -dir.x);
    float ret = height * ssin(count * dot(xz, dir) - speed * time + faza) / time2;
    float ret2 = height * ssin(count * dot(xz, ortoDir) - speed * time + faza) / time2;
    waveHeight = (ret + ret2) / 2;
}
void DDX_WaveFuntion2_float(float2 xz, float2 dir, float height, float minHeight,
float count, float speed, float faza, float time, out float waveHeight)
{
    float time2 = min(time, minHeight);
    float2 ortoDir = float2(dir.y, -dir.x);
    float xArg1 = count * dot(xz, dir) - speed * time + faza;
    float xArg2 = count * dot(xz, ortoDir) - speed * time + faza;
    float ret = dir.x * count * height * ddxssin(xArg1) / time2;
    float ret2 = ortoDir.x * count * height * ddxssin(xArg2) / time2;
    waveHeight = (ret + ret2) / 2;
}
void DDZ_WaveFuntion2_float(float2 xz, float2 dir, float height, float minHeight,
float count, float speed, float faza, float time, out float waveHeight)
{
    float time2 = min(time, minHeight);
    float2 ortoDir = float2(dir.y, -dir.x);
    float xArg1 = count * dot(xz, dir) - speed * time + faza;
    float xArg2 = count * dot(xz, ortoDir) - speed * time + faza;
    float ret = dir.y * count * height * ddxssin(xArg1) / time2;
    float ret2 = ortoDir.y * count * height * ddxssin(xArg2) / time2;
    waveHeight = (ret + ret2) / 2;
}
//Simpled
void SimpleWaveFuntion2_float(float2 xz, float2 dir, float height, float minHeight,
float count, float speed, float faza, float time, out float waveHeight)
{
    float time2 = min(time, minHeight);
    float ret = height * sin(count * dot(xz, dir) - speed * time + faza) / time2;
    waveHeight = ret;
}
void DDX_SimpleWaveFuntion2_float(float2 xz, float2 dir, float height, float minHeight,
float count, float speed, float faza, float time, out float waveHeight)
{
    float time2 = min(time, minHeight);
    float ret = dir.x * count * height * cos(count * dot(xz, dir) - speed * time + faza) / time2;
    waveHeight = ret;
}
void DDZ_SimpleWaveFuntion2_float(float2 xz, float2 dir, float height, float minHeight,
float count, float speed, float faza, float time, out float waveHeight)
{
    float time2 = min(time, minHeight);
    float ret = dir.y * count * height * cos(count * dot(xz, dir) - speed * time + faza) / time2;
    waveHeight = ret;
}