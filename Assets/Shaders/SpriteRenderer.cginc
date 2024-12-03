#ifndef SPRITE_RENDERER_INCLUDED
#define SPRITE_RENDERER_INCLUDED

#ifdef UNITY_INSTANCING_ENABLED

	UNITY_INSTANCING_BUFFER_START(PerDrawSprite)
		// SpriteRenderer.Color while Non-Batched/Instanced.
		UNITY_DEFINE_INSTANCED_PROP(fixed4, unity_SpriteRendererColorArray)
		// this could be smaller but that's how bit each entry is regardless of type
		UNITY_DEFINE_INSTANCED_PROP(fixed2, unity_SpriteFlipArray)
	UNITY_INSTANCING_BUFFER_END(PerDrawSprite)

	#define _RendererColor  UNITY_ACCESS_INSTANCED_PROP(PerDrawSprite, unity_SpriteRendererColorArray)
	#define _Flip           UNITY_ACCESS_INSTANCED_PROP(PerDrawSprite, unity_SpriteFlipArray)

#endif // instancing

CBUFFER_START(UnityPerDrawSprite)
#ifndef UNITY_INSTANCING_ENABLED
	fixed4 _RendererColor;
	fixed2 _Flip;
#endif
	float _EnableExternalAlpha;
CBUFFER_END

#endif