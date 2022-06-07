// Pcx - Point cloud importer & renderer for Unity
// https://github.com/keijiro/Pcx

#define PCX_MAX_BRIGHTNESS 16

uint PcxEncodeColor(float3 rgb)
{
    float y = max(max(rgb.r, rgb.g), rgb.b);
    y = clamp(ceil(y * 255 / PCX_MAX_BRIGHTNESS), 1, 255);
    rgb *= 255 * 255 / (y * PCX_MAX_BRIGHTNESS);
    uint4 i = float4(rgb, y);
    return i.x | (i.y << 8) | (i.z << 16) | (i.w << 24);
}

float3 PcxDecodeColor(uint data)
{
    float r = (data      ) & 0xff;
    float g = (data >>  8) & 0xff;
    float b = (data >> 16) & 0xff;
    float a = (data >> 24) & 0xff;
    return float3(r, g, b) * a * PCX_MAX_BRIGHTNESS / (255 * 255);
}
