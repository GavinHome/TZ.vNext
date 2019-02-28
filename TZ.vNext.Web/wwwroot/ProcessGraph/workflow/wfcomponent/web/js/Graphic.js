IE4 = ! (navigator.appVersion.charAt(0) < "4" || navigator.appName == "Netscape")

var xo=0
var yo=0
var Ox = -1
var Oy = -1
var lineWidth=1

var rad = Math.PI/180
var maxY = 400
var color = "#228B22"

function print(str) {	
	document.write(str)
}

function orgY(y) {
	return maxY-y
}
function outPlot(x,y,w,h) {
	print('<div style="position:absolute;left:'+x+';top:'+y+';height:'+h+';width:'+w+';font-size:1px;background-color:'+color+'"></div>')
}

function Plot(x,y) {
	outPlot(x,y,1,1)
	if(Ox>=0 || Oy>=0) {
		ShowLine(Ox,Oy,x-Ox,y-Oy)		
	}
	Ox = x
	Oy = y
}

function ShowLine(x,y,w,h) {
	if(w<0) {
		x += w
		w = Math.abs(w)
	}
	if(h<0) {
		y += h
		h = Math.abs(h)
	}
	//if(w<1) w=1
	//if(h<1) h=1
	if(w<=1){
		if( lineWidth/2>=1 )
			x = x-lineWidth/2
		w=lineWidth
	}
	if(h<=1){
		if( lineWidth/2>=1 )
			y=y-lineWidth/2
		h=lineWidth
	}
	outPlot(x,y,Math.round(w),Math.round(h))
}

function LineTo(x,y) {
	Line(xo,yo,x,y)
}

function sign(n) {
	if(n>0)
		return 1
	if(n<0)
		return -1
	return n
}

function Line(x1,y1,x2,y2) {
	if (x1 > x2)
	{
		var _x2 = x2;
		var _y2 = y2;
		x2 = x1;
		y2 = y1;
		x1 = _x2;
		y1 = _y2;
	}
	var dx = x2-x1, dy = Math.abs(y2-y1),
	x = x1, y = y1,
	yIncr = (y1 > y2)? -1 : 1;

	if (dx >= dy)
	{
		var pr = dy<<1,
		pru = pr - (dx<<1),
		p = pr-dx,
		ox = x;
		while ((dx--) > 0)
		{
			++x;
			if (p > 0)
			{
				this.mkDiv(ox, y, x-ox, 1);
				y += yIncr;
				p += pru;
				ox = x;
			}
			else p += pr;
		}
		this.mkDiv(ox, y, x2-ox+1, 1);
	}

	else
	{
		var pr = dx<<1,
		pru = pr - (dy<<1),
		p = pr-dy,
		oy = y;
		if (y2 <= y1)
		{
			while ((dy--) > 0)
			{
				if (p > 0)
				{
					this.mkDiv(x++, y, 1, oy-y+1);
					y += yIncr;
					p += pru;
					oy = y;
				}
				else
				{
					y += yIncr;
					p += pr;
				}
			}
			this.mkDiv(x2, y2, 1, oy-y2+1);
		}
		else
		{
			while ((dy--) > 0)
			{
				y += yIncr;
				if (p > 0)
				{
					this.mkDiv(x++, oy, 1, y-oy);
					p += pru;
					oy = y;
				}
				else p += pr;
			}
			this.mkDiv(x2, oy, 1, y2-oy+1);
		}
	}
} 

function MoveTo(x,y) {	
	Ox = Oy = -1
	xo = Math.round(x)
	yo = Math.round(y)
}

// 圆
function Cir(x,y,r) {		
	MoveTo(x+r,y)
	for(i=0;i<=360;i+=5) {
		LineTo(r*Math.cos(i*rad)+x,r*Math.sin(i*rad)+y)
	}
}

// 弧形
function Arc(x,y,r,a1,a2) {
	MoveTo(r*Math.cos(a1*rad)+x,r*Math.sin(a1*rad)+y)
	for(i=a1;i<=a2;i++) {
		LineTo(r*Math.cos(i*rad)+x,r*Math.sin(i*rad)+y)
	}
}

// 扇形
function Pei(x,y,r,a1,a2) {
	MoveTo(x,y)
	for(var i=a1;i<=a2;i++) {
		LineTo(r*Math.cos(i*rad)+x,r*Math.sin(i*rad)+y)
	}
	LineTo(x,y)
}

// 弹出扇形
function PopPei(x,y,r,a1,a2) {
	dx = r*Math.cos((a1+(a2-a1)/2)*rad)/10
	dy = r*Math.sin((a1+(a2-a1)/2)*rad)/10
	x += dx
	y += dy
	MoveTo(x,y)
	for(var i=a1;i<=a2;i++) {
		LineTo(r*Math.cos(i*rad)+x,r*Math.sin(i*rad)+y)
	}
	LineTo(x,y)
}

// 矩形
function Rect(x,y,w,h) {	MoveTo(x,y)
	LineTo(x+w,y)
	LineTo(x+w,y+h)
	LineTo(x,y+h)
	LineTo(x,y)
}

// 准星
function zhunxing(x,y) {
	var ox = xo
	var oy = yo
	var oColor = color
	color = "#000000"
	Line(x-5,y,x+6,y)
	Line(x,y-6,x,y+5)
	print('<div style="position:absolute;font-size:10pt;left:'+(x+5)+';top:'+orgY(y+5)+';">['+x+','+y+']</div>')
	color = oColor
	xo = ox
	yo = oy
}
// 标注
function biaozhuStr(x,y,s) {
	return '<div style="position:absolute;font-size:10pt;left:'+x+';top:'+orgY(y)+';">'+s+'</div>'
}
function biaozhu(x,y,s,t) {
	var ox = xo
	var oy = yo
	var oColor = color
	point = "p01.gif"
	if(t==1) {
		print(biaozhuStr(x-5,y+6,"·"+s))
	}
	if(t==2) {
		print(biaozhuStr(xo+x*Math.cos(y*rad)-10,yo+x*Math.sin(y*rad),s))
	}
	color = oColor
	xo = ox
	yo = oy
}

function mkDiv(x, y, w, h)
{
	print('<div style="position:absolute;'+
		'left:' + x + 'px;'+
		'top:' + y + 'px;'+
		'width:' + w + 'px;'+
		'height:' + h + 'px;'+
		'clip:rect(0,'+w+'px,'+h+'px,0);'+
		'background-color:' + this.color +
		';overflow:hidden'+
		';"><\/div>')
}

function fillPolygon(array_x, array_y)
	{
		var i;
		var y;
		var miny, maxy;
		var x1, y1;
		var x2, y2;
		var ind1, ind2;
		var ints;

		var n = array_x.length;

		if (!n) return;
		miny = array_y[0];
		maxy = array_y[0];
		for (i = 1; i < n; i++)
		{
			if (array_y[i] < miny)
				miny = array_y[i];

			if (array_y[i] > maxy)
				maxy = array_y[i];
		}
		for (y = miny; y <= maxy; y++)
		{
			var polyInts = new Array();
			ints = 0;
			for (i = 0; i < n; i++)
			{
				if (!i)
				{
					ind1 = n-1;
					ind2 = 0;
				}
				else
				{
					ind1 = i-1;
					ind2 = i;
				}
				y1 = array_y[ind1];
				y2 = array_y[ind2];
				if (y1 < y2)
				{
					x1 = array_x[ind1];
					x2 = array_x[ind2];
				}
				else if (y1 > y2)
				{
					y2 = array_y[ind1];
					y1 = array_y[ind2];
					x2 = array_x[ind1];
					x1 = array_x[ind2];
				}
				else continue;

				if ((y >= y1) && (y < y2))
					polyInts[ints++] = Math.round((y-y1) * (x2-x1) / (y2-y1) + x1);

				else if ((y == maxy) && (y > y1) && (y <= y2))
					polyInts[ints++] = Math.round((y-y1) * (x2-x1) / (y2-y1) + x1);
			}
			polyInts.sort(integer_compare);

			for (i = 0; i < ints; i+=2)
			{
				w = polyInts[i+1]-polyInts[i]
				this.mkDiv(polyInts[i], y, polyInts[i+1]-polyInts[i]+1, 1);
			}
		}
	}

function integer_compare(x,y)
{
	return (x < y) ? -1 : ((x > y)*1);
}