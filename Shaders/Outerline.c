//Esse código é executado em cada linha de pixel por vez.
varying vec2 v_vTexcoord;
varying vec4 v_vColour;
uniform float PixelAlt;
uniform float PixelLarg;
void main()
{
    vec2 offsetx;
    offsetx.x = PixelLarg;
    vec2 offsety;
    offsety.y = PixelAlt;
    /*Obtém só valores positivos para o origin_alpha de cada pixel, impedindo assim que ele chegue em valores negativos
    O alpha obterá os valores positivos do origin_alpha, portanto, não conseguirá ficar invisível, já que está em módulo*/
    float origin_alpha = abs(texture2D(gm_BaseTexture, v_vTexcoord).a); 
    float alpha = origin_alpha;
    vec4 origin_color = vec4(0.025,0.025,0.025,0);
    //Obtém o pixel na maior/menor distância e soma ao alpha
    alpha += max(alpha,texture2D(gm_BaseTexture, v_vTexcoord + offsetx).a);
    alpha += max(alpha,texture2D(gm_BaseTexture, v_vTexcoord - offsetx).a);
    alpha += max(alpha,texture2D(gm_BaseTexture, v_vTexcoord + offsety).a);
    alpha += max(alpha,texture2D(gm_BaseTexture, v_vTexcoord - offsety).a);
    //Muda o image_blend 
    gl_FragColor = (v_vColour*(1.0 - origin_alpha) + texture2D(gm_BaseTexture,v_vTexcoord))*(origin_color/(origin_alpha));
    gl_FragColor.a = alpha;
    /*v_vColour é o vetor de cores do sprite, multiplicar (1.0 - origin_alpha) quer dizer
    que a cada pixel ele vai ganhando transparência*/
}
