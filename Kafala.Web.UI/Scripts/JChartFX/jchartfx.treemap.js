(function(){var f={Version:"7.1.5044.21402"};window.cfx.treemap=f;var j=window.cfx,i=window.sfx;f.TreeMapLayout={Naive:0,Squarified:1};var n=function g(){g._ic();this.d=this.g=null;this.j=this.h=0;this.f=null;this._0TreeMap()};f.TreeMap=n;n.prototype={_0TreeMap:function(){this.setLayout(1);this.e=!1;return this},getLayout:function(){return this.h},setLayout:function(g){this.h=g;switch(this.h){case 0:this.g=(new o)._0dP();break;case 1:this.g=(new p)._0dO()}},getRandomLocations:function(){return this.e},
setRandomLocations:function(g){this.e=g},getTemplate:function(){return null==this.d?null:this.d.u()},setTemplate:function(g){null==this.d&&(this.d=(new j.vector._Zt)._0_Zt());this.d.su(g)},m:function(g,e,b){if(this.j!=g.c&&!b){e=(new i.U)._0U();this.j=g.c;if(null==this.f||this.f.length==g.d)this.f=Array(g.d);for(b=0;b<g.d;b++)this.f[b]=e.c(2)}},reset:function(){},icV:function(){return 1},icW:function(){return 142745857},icU:function(g,e){switch(e){case 4:return 1;default:return null}},icX:function(g,
e,b){e=b.m_bDetecting;g.a=1;g.b=0;var a=new j.bh;b.bl(a);var a=b.a.b.iaH(),c=!1;e?(b.A=1,b.e=0,b.m=0,b.o=a):b.e=b.m;var d=(new k)._01dM(b,a);this.e&&this.m(d,b,e);this.g.b(d,b);for(var m=b.P,l=0;l<a;l++){b.aw();d.f=i.a.d(b.G);d.g=d.f/d.h;this.e&&(d.e=0!=this.f[l]);b.e==b.o?d.b._cf(d.a):this.g.c(d);if(0!=d.f)if(d.c-=d.c*d.g,d.h-=d.f,b.ao(b.d,b.e,!1),b.N(!0),b.ax(b.d,b.e),e){if(b.c.idP(b.x,d.b),b.c.idz(b.n,d.b),b.detectCheck()){c=!0;break}}else{var h=i.d.n(d.b);m&&(h.w*=b.aU,h.h*=b.aU,h.x+=(d.b.w-h.w)/
2,h.y+=(d.b.h-h.h)/2);if(null!=this.d&&!i.b.o(this.d.u())){for(var f=this.d.id().Sb();!0==f.SI();)this.d.l(f.SK(),b,this,h);this.k(b,this.d,i.d.n(d.b)._nc())}else b.c.idQ(b.x,h),b.c.idA(b.n,h)}b.an(0,1)}this.g.d(d,b);c?(g.b=0,g.a=g.b):(b.ak-=a-1,b.e=b.o,null!=this.d&&this.d._dispose1(!1))},k:function(g,e,b){var a=1;2==g.m_highlightState&&(a=0.25);e.q(g.c,b,a,0)}};n._dt("CWTT",i.Sy,0,j.icT);var k=function e(){e._ic();this.g=0;this.b=new i.c;this.f=0;this.e=!1;this.c=this.d=0;this.a=new i.c;this.h=
0};k.prototype={_i1:function(e,b){this.a._cf(e.v);this.h=e.J;this.d=b;this.b._i();this.c=this.a.w*this.a.h;this.f=this.g=0;this.e=!0},_i:function(){this.g=0;this.b._i();this.f=0;this.e=!1;this.c=this.d=0;this.a._i();this.h=0},_01dM:function(e,b){this.b=new i.c;this.a=new i.c;this._i1(e,b);return this},_0dM:function(){this.b=new i.c;this.a=new i.c;this._i();return this},_cf:function(e){this.g=e.g;this.b._cf(e.b);this.f=e.f;this.e=e.e;this.d=e.d;this.c=e.c;this.a._cf(e.a);this.h=e.h;return this},_nc:function(){var e=
new k;e._cf(this);return e}};k._dt("CWTT",i.W,0);f=function b(){b._ic()};f.prototype={_0dN:function(){return this},d:function(){},b:function(){}};f._dt("CWTT",i.Sy,0);var p=function a(){a._ic();this.e=this.k=this.g=this.l=this.r=0;this.n=null;this.h=!1;this.i=this.m=this.j=this.p=this.q=this.f=0};p.prototype={_0dO:function(){this._bc._0dN.call(this);return this},v:function(a,c,d){this.m=this.j;for(var m=0,f=0;f<c;f++,a++)var h=this.t(a,f,c),h=i.a.n(h/d,d/h),m=i.a.n(h,m);return m},d:function(){this.n=
null},u:function(a,c){this.j=c.w>c.h?c.h:c.w;var d=i.D.b,f=1,l=0;this.f=this.i=0;for(var h=a;h<this.q;h++,f++){var k=this.o(h);this.i+=k;var l=this.r*this.i/this.j,j=this.v(a,f,l);if(1>j||j>d){this.i-=k;f--;break}this.f=i.a.j(l);d=j}return f},t:function(a,c,d){var a=this.o(a),f=0;c==d-1?f=this.m:(f=i.a.j(this.j*(a/this.i)),this.m-=f);return f},o:function(a){a=this.n.getItem(this.p,a);return a=1.0E108==a||i.D.g(a)?0:i.a.d(a)},c:function(a){var c=a.a.w>a.a.h;this.g-this.k>=this.l&&(this.k=this.g,this.h=
a.e,this.l=this.u(this.g,a.a),c?(a.b.w=this.f,this.h?(this.e=a.a.y,a.b.x=a.a.x):(this.e=a.a.c(),a.b.x=a.a.g()-this.f)):(a.b.h=this.f,this.h?(this.e=a.a.x,a.b.y=a.a.y):(this.e=a.a.g(),a.b.y=a.a.c()-this.f)),this.m=this.j);var d=this.t(this.g,this.g-this.k,this.l);c?(a.b.h=d,this.h?(a.b.y=this.e,this.e+=d):(this.e-=d,a.b.y=this.e)):(a.b.w=d,this.h?(a.b.x=this.e,this.e+=d):(this.e-=d,a.b.x=this.e));this.g++;if(this.g-this.k>=this.l||this.g==a.d-1)c?(a.a.w-=this.f,this.h&&(a.a.x+=this.f)):(a.a.h-=this.f,
this.h&&(a.a.y+=this.f))},b:function(a,c){this.p=c.d;this.n=c.a.b.iaO();this.r=a.c/a.h;this.q=a.d;this.k=this.l=this.g=0}};p._dt("CWTT",f,0);var o=function c(){c._ic()};o.prototype={_0dP:function(){this._bc._0dN.call(this);return this},c:function(c){c.b.sf(c.a.f());c.a.w>c.a.h?(c.b.h=c.a.h,c.b.w=c.a.w*c.g,c.e?c.a.x+=c.b.w:c.b.x=c.a.g()-c.b.w,c.a.w-=c.b.w):(c.b.w=c.a.w,c.b.h=c.a.h*c.g,c.e?c.a.y+=c.b.h:c.b.y=c.a.c()-c.b.h,c.a.h-=c.b.h)}};o._dt("CWTT",f,0)})();
