// Constants
#define PI 3.14159265359
#define RECIPROCAL_PI 0.3183098861837697
#define GAMMA 2.2
#define INV_GAMMA 0.45

varying vec3 v_position;
varying vec3 v_world_position;
varying vec3 v_normal;
varying vec2 v_uv;
varying vec4 v_color;

//Implement atributes phong
uniform vec3 u_ambient_light;
uniform vec3 u_light_position;
uniform vec3 u_light_color;
uniform vec3 u_camera_position;

uniform vec4 u_color;

uniform samplerCube u_texture;

uniform sampler2D u_albedo_texture;
uniform sampler2D u_roughness_texture;
uniform sampler2D u_metalness_texture;


void main()
{
    vec2 uv = v_uv;
    vec4 color = u_color;

    //Calculate NdotL
    vec3 N = normalize(v_normal);
    vec3 L = normalize(u_light_position - v_world_position);
    float NdotL = dot(N,L);
	NdotL = clamp(NdotL, 0.0, 1.0);

    //Calculate microface - H
    vec3 V = normalize(u_camera_position - v_world_position);
    vec3 H = normalize(V  + L);

    //Calculate metalness, albedo, roughtness
    float metalness = texture(u_metalness_texture, uv).x;

    float roughtness = texture(u_roughness_texture, uv).x;

    //Albedo
    vec4 color_texture = texture(u_albedo_texture, uv);

    //Calculate Fo - diffuse
    vec3 fo = (color_texture.xyz * metalness) + (vec3(0.04)*(1 - metalness));

    vec3 albedo = color_texture*(1 - metalness);

	//Calculate f(l,v)

		//Calculate f_diff
		vec3 fdiff = calculate_fdiff(albedo);

		//Calculate f_espec
		vec3 f_espec = calculate_fespec(NdotL,fo, roughtness, H, N, V, L);

		vec3 funct = fdiff + fspec;

    color.xyz *= f * NdotL * u_light_color;
    

	gl_FragColor = color;
}

vec3 calculate_fdiff(vec3 albedo){

	return albedo/PI

}

vec3 calculate_fspec(float NdotL, vec3 Fo, vec3 roughtness, vec3 M, vec3 N, vec3 V){

    //Calculate F(l,n)
    vec3 f = Fo + (1-Fo)(1-NdotL)^5;

    //Calculate D(h)
    float NdotM = dot(N,M);
	NdotM = clamp(NdotM, 0.0, 1.0);
    alpha = roughtness^4;
    float den = PI * (((NdotM)^2)*(alpha - 1) + 1)^2;
    float Dm = alpha / den;

    //Calculate G(l,v,h)
    float NdotV = dot(N,V);
	NdotV = clamp(NdotV, 0.0, 1.0);
    float k = ((roughtness+1)^2)/8
    float G = NdotV/(NdotV * (1 - roughtness) + k);

    float denominator = 4 * NdotL * NdotV;

	return f * Dm * G / denominator

}
