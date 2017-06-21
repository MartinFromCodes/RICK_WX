/*************Jquery Base64 Upload（Base6 图片上传）*/
/*************20160315 LQ.Chen***********************/
/*************参数说明*******************************
**_dom 调用该函数对象即input[type="file"]**
**_url 请求地址**
**_width 压缩宽度,0时不压缩**
**_data 提交的对象**
**_callback 回调函数**
****************************************************/
var Upload = function (_dom, _url, _width, _data, _callback) {
    var Dom = _dom;
    var quality = 100;
    var blob = URL.createObjectURL(Dom.files[0]) || webkitURL.createObjectURL(Dom.files[0]) || window.URL.createObjectURL(Dom.files[0]);
    var MIME = Dom.files[0].type;
    var img = new Image();
    img.src = blob;
    img.onload = function () {
        var cvs = document.createElement('canvas');
        var ctx = cvs.getContext('2d');
        var W = _width == 0 ? this.width : _width;
        cvs.width = W;
        cvs.height = W / this.width * this.height;
        ctx.drawImage(this, 0, 0, W, W / this.width * this.height);
        var base64 = cvs.toDataURL(MIME, quality / 100);
        _data.base64 = base64;
        //_data.MIME = MIME;
        $.ajax({
            type: 'POST',
            url: _url,
            data: _data,
            dataType: 'text',
            success: function (data, textStatus) {
                _callback(data, base64);
            },
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }
        });
    }
}

function GetImages() {
    var Dom = arguments[0]
    var quality = 100;
    var blob = URL.createObjectURL(Dom.files[0]) || webkitURL.createObjectURL(Dom.files[0]) || window.URL.createObjectURL(Dom.files[0]);
    var MIME = Dom.files[0].type;
    var img = new Image();
    img.src = blob;
    img.onload = function () {
        var cvs = document.createElement('canvas');
        var ctx = cvs.getContext('2d');
        var W = document.documentElement.clientWidth;
        cvs.width = W;
        cvs.height = W / this.width * this.height;
        ctx.drawImage(this, 0, 0, W, W / this.width * this.height);
        var base64 = cvs.toDataURL(MIME, quality / 100);
        var _img = null;
        if (document.getElementById("imgPanel").getElementsByTagName("img").length>0) {
            _img = document.getElementById("imgPanel").getElementsByTagName("img")[0];
        }
        else
        {
            _img = document.createElement("img");
        }
        _img.src = base64;
        _img.setAttribute("data-mime", MIME);
        if (document.getElementById("imgPanel").getElementsByTagName("img").length == 0) {
            document.getElementById("imgPanel").appendChild(_img);
        }
    }
    InitMove();
}
function InitMove() {
    RemoveClass(document.getElementById("imgPanel"), "none");
    if (document.getElementById("imgCut") != null) {
        document.getElementById("imgCut").addEventListener('touchstart',
        function (event) {
            event.preventDefault();
            var W = document.getElementById("imgCut").offsetWidth;
            var L = document.getElementById("imgCut").offsetLeft;
            var X = event.touches[0].clientX;
            var Y = event.touches[0].clientY;
            if (X > W + L - 30 && Y > W + L - 30) {
                document.getElementById("imgCut").style.backgroundColor = 'rgba(255,0,0,0.6)';
                document.ontouchmove = function (event) {
                    if (event.touches[0].clientX - L >= 70) {
                        document.getElementById("imgCut").style.width = event.touches[0].clientX - L + "px";
                        document.getElementById("imgCut").style.height = event.touches[0].clientX - L + "px";
                    }
                }
            }
            else {
                document.ontouchmove = function (event) {

                    document.getElementById("imgCut").style.left = event.touches[0].clientX - W / 2 + "px";
                    document.getElementById("imgCut").style.top = event.touches[0].clientY - W / 2 + "px";
                }
            }
            document.ontouchend = function (event) {
                document.getElementById("imgCut").style.backgroundColor = 'rgba(0,0,0,0.5)';
                document.ontouchmove = null;
            }
        });
    }
}
function HeadICOSave(_url, _data, _callback) {
    var _url = arguments[0];
    var cvs = document.createElement('canvas');
    var ctx = cvs.getContext('2d');
    var W = document.getElementById("imgCut").offsetWidth;
    var img = document.getElementById("imgPanel").getElementsByTagName("img")[0];
    var imgx = document.getElementById("imgCut").offsetLeft;
    var imgy = document.getElementById("imgCut").offsetTop;
    cvs.width = W;
    cvs.height = W;
    ctx.drawImage(img, imgx, imgy, W, W, 0, 0, W, W);
    var base64 = cvs.toDataURL(img.getAttribute('data-mime'), 1);
    _data.base64 = base64;
    $.ajax({
        type: 'POST',
        url: _url,
        data: _data,
        dataType: 'text',
        success: function (data, textStatus) {
            _callback(data, base64);
        },
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

