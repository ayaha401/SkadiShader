# SkadiSpriteShaderProvider
## 説明
SkadiShaderのプロパティを変更します。

## 対応シェーダー
* Skadi_Sprite_Lit
* Skadi_Sprite_Lit_Outline
* Skadi_Sprite_Unlit
* Skadi_Sprite_Unlit_Outline

## 目次
* エラー説明
* メソッド一覧

## メソッド一覧
|メソッド名|説明|
|---|---|
|SetColor|マテリアルの色を変えるます。SpriteRenderer.Colorとほぼ同義|
|SetOutlineColor|アウトラインのカラーを変更します。|
|EnableOutline|アウトラインを有効、無効化します。|
|EnableEmission|エミッションを有効、無効化します。|
|SetEmissionPower|エミッション強度を設定します。|
|SetFlickerMode|エミッションの点滅モードを切り替えます。|
|SetFrequency|一秒間に点滅する回数を変えます。|
|EnableUVScroll|UVスクロールを有効、無効化します。|
|SetUVScrollSpeed|UVスクロールの速度を設定します。|

# SkadiSpriteShaderProvider.SetColor
``` C#
public static void SetColor(SpriteRenderer renderer, Color col, ColorBlendMode blendMode = ColorBlendMode.Multi)
```

|引数|説明|
|---|---|
|renderer|マテリアルの付いているSpriteRenderer|
|col|合成したい色|
|blendMode|合成モード|
|ColorBlendMode.Multi|乗算。SpriteRenderer.Colorと同じ|
|ColorBlendMode.Fill|塗りつぶし|

## 説明
マテリアルの色を変えるます。SpriteRenderer.Colorとほぼ同義

# SkadiSpriteShaderProvider.SetOutlineColor
```C#
public static void SetOutlineColor(SpriteRenderer renderer, Color col)
```

|引数|説明|
|---|---|
|renderer|マテリアルの付いているSpriteRenderer|
|col|変更したい色|

## 説明
アウトラインのカラーを変更します。

# SkadiSpriteShaderProvider.EnableOutline
```C#
public static void EnableOutline(SpriteRenderer renderer, bool isEnable)
```

|引数|説明|
|---|---|
|renderer|マテリアルの付いているSpriteRenderer|
|isEnable|アウトラインの有効、無効化|

## 説明
アウトラインを有効、無効化します。

# SkadiSpriteShaderProvider.EnableEmission
```C#
public static void EnableEmission(SpriteRenderer renderer, bool isEnable)
```

|引数|説明|
|---|---|
|renderer|マテリアルの付いているSpriteRenderer|
|isEnable|エミッションの有効、無効化|

## 説明
エミッションを有効、無効化します。

# SkadiSpriteShaderProvider.SetEmissionPower
```C#
public static void SetEmissionPower(SpriteRenderer renderer, float power)
```

|引数|説明|
|---|---|
|renderer|マテリアルの付いているSpriteRenderer|
|power|エミッション強度|

## 説明
エミッション強度を設定します。

# SkadiSpriteShaderProvider.SetFlickerMode
```C#
public static void SetFlickerMode(SpriteRenderer renderer, FlickerMode mode)
```

|引数|説明|
|---|---|
|renderer|マテリアルの付いているSpriteRenderer|
|mode|点滅モード|
|FlickerMode.Line|点滅しません|
|FlickerMode.Sin|Sin波を使います|
|FlickerMode.Saw|のこぎり波を使います|
|FlickerMode.Triangle|三角波を使います|
|FlickerMode.Square|矩形波を使います|

## 説明
エミッションの点滅モードを切り替えます。

# SkadiSpriteShaderProvider.SetFrequency
```C#
public static void SetFrequency(SpriteRenderer renderer, float frequency)
```

|引数|説明|
|---|---|
|renderer|マテリアルの付いているSpriteRenderer|
|frequency|振動数|

## 説明
一秒間に点滅する回数を変えます。

# SkadiSpriteShaderProvider.EnableUVScroll
```C#
public static void EnableUVScroll(SpriteRenderer renderer, bool isEnable)
```

|引数|説明|
|---|---|
|renderer|マテリアルの付いているSpriteRenderer|
|isEnable|UVスクロールの有効、無効化|

## 説明
UVスクロールを有効、無効化します。

# SkadiSpriteShaderProvider.SetUVScrollSpeed

```C#
public static void SetUVScrollSpeed(SpriteRenderer renderer, Vector2 vector2)
```

|引数|説明|
|---|---|
|renderer|マテリアルの付いているSpriteRenderer
|vector2|X、Y方向の速度|

## 説明
UVスクロールの速度を設定します。