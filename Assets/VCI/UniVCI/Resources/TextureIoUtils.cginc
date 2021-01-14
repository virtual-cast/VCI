static const float gamma_value = 2.2;
static const float gamma_max_brightness = 5;
static const float linear_max_brightness = pow(gamma_max_brightness, gamma_value);

inline float4 EncodeToRgbm(float4 col)
{
    // Encode to RGBM
    // Brightness to Alpha: f(x) = pow(x / pow(5, 2.2), 1 / 2.2)
    float brightness = clamp(max(col.r, max(col.g, col.b)), 0, linear_max_brightness);
    float multiplier = pow(brightness / linear_max_brightness, 1 / gamma_value);
    float quantized_multiplier = max(1, ceil(multiplier * 255.0)) / 255.0; // 8bit で表現できる値に丸める
    float quantized_brightness = linear_max_brightness * pow(quantized_multiplier, gamma_value);

    float4 rgbm;
    rgbm.rgb = col.rgb / quantized_brightness;
    rgbm.a = quantized_multiplier;
    
    return rgbm;
}

inline float4 DecodeFromRgbm(float4 rgbm)
{
    // Decode RGBM
    // Alpha To Brightness: f(x) = pow(5, 2.2) * pow(x, 2.2)
    float4 col;
    col.rgb = linear_max_brightness * pow(rgbm.a, gamma_value) * rgbm.rgb;
    col.a = 1;
    
    return col;
}

inline float4 EncodeToDldr(float4 col)
{
    // double LDR conversion
    float4 dldr;
    dldr.rgb = saturate(col.rgb * 0.5);
    dldr.a = 1;
    
    return dldr;
}

inline float4 DecodeFromDldr(float4 dldr)
{
    // double LDR decode
    float4 col;
    col.rgb = dldr.rgb * 2.0;
    col.a = 1;
    
    return col;
}