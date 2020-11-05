
varying vec3 v_position;
varying vec3 v_world_position;
varying vec3 v_normal;
varying vec2 v_uv;
varying vec4 v_color;

uniform samplerCube u_texture;

void main()
{
	vec2 uv = v_uv;
	gl_FragColor =textureCube(u_texture, v_position);
}
