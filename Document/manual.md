# 使い方

# 目次
* [シェーダーの種類](#シェーダーの種類)
* [Info](#Info)
* [Main](#Main)
  * [BlendMode](##BlendMode)
  * [UseUVScroll](##UseUVScroll)
* [UseOutline](#UseOutline)
* [UseEmission](#UseEmission)
  * [Flicker](##Flicker)


# シェーダーの種類
|名前|説明|
|---|---|
|Skadi_Sprite_Lit|ライティングの影響を受けます。|
|Skadi_Sprite_Lit_Outline|ライティングの影響を受けます。<br>アウトラインの設定が可能です。|
|Skadi_Sprite_Unlit|ライティングの影響を受けないです。|
|Skadi_Sprite_Unlit_Outline|ライティングの影響を受けないです。<br>アウトラインの設定が可能です。|

# Info
|名前|説明|
|---|---|
|Version|現在のバージョンを表記しています。|
|How To Use(Japanese)|日本語のドキュメントに移動します。|

# Main
|名前|説明|プロパティ|
|---|---|---|
|Main Texture|メインとなるテクスチャです。|_MainTex|
|OE Texture|アウトライン(R要素)、エミッション(G要素)を指定したテクスチャです。|_OETex|
|LightingMask Texture|ライティングの影響を受ける場所をマスクできます。|_LightingMask|

## BlendMode
|名前|説明|プロパティ|
|---|---|---|
|BlendMode|`SpriteRenderer`の`Color`の合成方法を変更します。|_BlendMode|
|Multi|`SpriteRenderer`の`Color`で乗算します。|0|
|Fill|`SpriteRenderer`の`Color`で加算します。|1|

## UseUVScroll 
|名前|説明|プロパティ|
|---|---|---|
|UseUVScroll|UVスクロールを有効化します。|_UseUVScroll|
|UV Scroll X|X方向にUVをスクロールします。|_UVScroll_X|
|UV Scroll Y|Y方向にUVをスクロールします。|_UVScroll_Y|

# UseOutline
|名前|説明|プロパティ|
|---|---|---|
|UseOutline|アウトラインを有効化します。|_UseOutline|
|OutlineColor|アウトラインの色を指定します。|_OutlineColor|

# UseEmission
|名前|説明|プロパティ|
|---|---|---|
|UseEmission|発光を有効化します。|_UseEmission|
|EmissionPower|発光強度です。|_EmissionPower|

## Flicker
発光を点滅させます。
|名前|説明|プロパティ|
|---|---|---|
|Line|点滅しません。|0|
|Sin|Sinカーブを使用して点滅します。|1|
|Saw|のこぎり波を使用して点滅します。|2|
|Triangle|三角波を使用して点滅します。|3|
|Square|矩形波を使用して点滅します。|4|

|名前|説明|プロパティ|
|---|---|----|
|Frequency|1秒間に点滅する回数です。|_Frequency|
