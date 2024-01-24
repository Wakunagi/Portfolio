Shader "Custom/spr_cameraColor"
{
    Properties{
        _MainTex("MainTex", 2D) = ""{}
        _red("Red", Range(0.0, 1.0)) = 0.5
        _green("Green", Range(0.0, 1.0)) = 0.5
        _blue("Blue", Range(0.0, 1.0)) = 0.5
    }

        SubShader{
                Pass {
                    CGPROGRAM

                    #include "UnityCG.cginc"

                    #pragma vertex vert_img
                    #pragma fragment frag
                    sampler2D _MainTex;

                    fixed _red;
                    fixed _green;
                    fixed _blue;

                    int cName, scName;
                    
                    int ColorChecker(fixed r, fixed g, fixed b) {
                        r = (int)(r * 10) / 10.0f;
                        g = (int)(g * 10) / 10.0f;
                        b = (int)(b * 10) / 10.0f;

                        int name = 0;

                        name = 
                            (r > g && r > b) ? ((g > b) ? 5 : 1) 
                            : (r == g && r > b) ? 3 
                            : (b > r && b > g) ?((r > g) ? 4 : 2 ) 
                            : (g > r && g > b) ? 6 
                            : (r < 0.5f && g < 0.5f && b < 0.5f) ? 7 
                            : 0;

                        return name;
                    }


                    fixed4 frag(v2f_img i) : COLOR {

                        fixed4 c = tex2D(_MainTex, i.uv);

                        cName = ColorChecker((fixed)c.r, (fixed)c.g, (fixed)c.b);
                        scName = ColorChecker(_red, _green, _blue);

                        fixed r, b, g;
                        int Name;

                        Name = 
                            (cName == scName) ? cName
                            : (cName > 0 && scName > 0) ? cName + scName + 1
                            : cName + scName;
                        Name = (Name > 7) ? 7 : Name;

                        r = (Name == 1 || Name == 3 || Name == 5 || Name == 0 ) ? 1
                            : (Name == 4) ? 0.5f
                            : 0;
                        g = (Name == 3 || Name == 6 || Name == 0 ) ? 1
                            : (Name == 5) ? 0.5f
                            : 0;
                        b = (Name == 2 || Name == 4 || Name == 0 ) ? 1 : 0;
                            
                        r = (Name != 0) ? (r - 0.5f) * 0.4f + 0.5f : r;
                        g = (Name != 0) ? (g - 0.5f) * 0.4f + 0.5f : g;
                        b = (Name != 0) ? (b - 0.5f) * 0.4f + 0.5f : b;


                        fixed max = 0;
                        max = 
                            (c.r > c.g && c.r > c.b) ? c.r
                            : (c.g > c.b) ? c.g
                            : c.b;
                        
                        c.rgb = fixed3(r,g,b)*max;
                        return c;
                        
                    }
                    
                        
                        ENDCG
                }
            
            
        }
}
