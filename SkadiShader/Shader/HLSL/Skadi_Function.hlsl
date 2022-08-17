#ifndef SKADI_FUNCTION
#define SKADI_FUNCTION

//==================//
// 0    : Line      //
// 1    : Sin       //
// 2    : Saw       //
// 3    : Triangle  //
// 4    : Square    //
//==================//
float FlickerWave(int flickerMode, float flickerSpeed)
{
    float speed = _Time.y*flickerSpeed*2.;
    float wave = 0.;
    if(flickerMode == 0)    wave = 1.;
    if(flickerMode == 1)    wave = (sin(speed*SKADI_PI)+1.)*.5;
    if(flickerMode == 2)    wave = frac(speed*.5+.5);
    if(flickerMode == 3)    wave = abs(2.*frac(speed*.5-.25)-1.);
    if(flickerMode == 4)    wave = step(.5, frac(speed*.5+.5));

    return wave;
}

float Remap(float val, float2 inMinMax, float2 outMinMax)
{
    return clamp(outMinMax.x+(val-inMinMax.x)*(outMinMax.y-outMinMax.x)/(inMinMax.y-inMinMax.x), outMinMax.x, outMinMax.y);
}

#endif