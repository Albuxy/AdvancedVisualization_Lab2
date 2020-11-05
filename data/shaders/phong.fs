
varying vec3 v_position;
varying vec3 v_world_position;
varying vec3 v_normal;
varying vec2 v_uv;
varying vec4 v_color;

uniform vec4 u_color;
uniform sampler2D u_texture;

//Implement atributes phong
uniform vec3 u_ambient_light;
uniform vec3 u_light_position;
uniform vec3 u_light_color;
uniform vec3 u_camera_position;

void main()
{
	vec2 uv = v_uv;
	vec4 color = u_color;
	vec3 N = normalize(v_normal);
	color *= texture(u_texture, uv);

	//----Phong equation---

	vec3 light = vec3(0.0);

		//--Ambient
		vec3 ambient = vec3(0.0);
		ambient = u_ambient_light;

		//--Diffuse---
		vec3 diffuse = vec3(0.0);
		vec3 L;

		//Ligh vector is the same for all pixels
		L = normalize(u_light_position - v_world_position);
		float NdotL = dot(N,L);
		NdotL = clamp(NdotL, 0.0, 1.0);
		diffuse = NdotL * u_light_color;

		//--Specular--
		vec3 specular = vec3(0.0);
		vec3 V = normalize(u_camera_position - v_world_position);
		vec3 R = normalize(reflect(-L, N));
		float RdotV = dot(V,R);
		RdotV = clamp(RdotV, 0.0, 1.0);
		float spec = pow(RdotV, 32);
		specular = spec * u_light_color;

		//---Total Light----
		light = ambient + diffuse + specular;
		
	color.xyz *= light;

	gl_FragColor = color;
}
