shader_type canvas_item;

uniform float vignette_radius : hint_range(0.0, 1.0) = 0.5;
uniform float vignette_softness : hint_range(0.0, 1.0) = 0.5;
uniform vec4 vignette_color : source_color = vec4(0.0, 0.0, 0.0, 1.0);

void fragment() {
    vec2 uv = SCREEN_UV;  // Координаты экрана
    vec2 center = vec2(0.5, 0.5);  // Центр экрана
    float dist = distance(uv, center);  // Расстояние от центра
    float vignette = smoothstep(vignette_radius, vignette_radius - vignette_softness, dist);
    COLOR = mix(vignette_color, vec4(0.0, 0.0, 0.0, 0.0), vignette);  // Затемнение по краям
}