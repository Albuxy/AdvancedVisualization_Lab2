varying vec3 v_normal;
varying vec2 v_uv;
varying vec4 v_color;
varying vec3 v_position;
varying vec3 v_world_position;

uniform samplerCube u_texture;
uniform vec3 u_camera_position;

void main()
{
	vec2 uv = v_uv;
	vec3 coord= normalize(v_world_position - u_camera_position);
	vec3 direction= normalize(reflect(coord,v_normal));
	gl_FragColor = textureCube( u_texture, direction);
}