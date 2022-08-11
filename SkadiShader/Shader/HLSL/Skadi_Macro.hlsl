#ifndef SKADI_MACRO
#define SKADI_MACRO

#define SKADI_EPS .000001
#define SKADI_PI 3.14159265359
#define SKADI_TAU 6.2831850718

#define COMPARE_EPS(n) max(n,SKADI_EPS)
#define SKADI_ROT_Z(a)  float2x2(cos(a),sin(a),-sin(a),cos(a))
#define SKADI_DEG2RAD(x) SKADI_PI/180*x

#endif