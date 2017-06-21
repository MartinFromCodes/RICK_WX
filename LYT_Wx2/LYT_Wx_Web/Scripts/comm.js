var IsDemo = false;
var u = navigator.userAgent, app = navigator.appVersion;
var isAndroid = u.indexOf('Android') > -1 || u.indexOf('Linux') > -1; //android终端或者uc浏览
var isiOS = !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/); //ios终端
var isWeiXin = !!u.match(/MicroMessenger/i);//微信终端
var Net_url = 'http://172.16.16.196:8080';
//var Net_url = http://172.16.16.196:8080'http://183.47.45.227:8081';
var Pixel = {
    W: Math.min(document.documentElement.clientWidth, 640),
    H: document.documentElement.clientHeight
};
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) { return unescape(r[2]); }
    return null;
};
function AddClass(o, cn) {
    var re = new RegExp("(\\s*|^)" + cn + "\\b", "g");
    o.className += o.className ? (re.test(o.className) ? "" : " " + cn) : cn;
};
function RemoveClass(o, cn) {
    var re = new RegExp("(\\s*|^)" + cn + "\\b", "g");
    var sName = o.className;
    o.className = sName.replace(re, "");
};
var FillZero = function () {
    var Num = parseInt(arguments[0]);
    return Num < 10 ? "0" + Num : Num;
};
String.prototype.Boolean = function () {
    if (this.toLocaleLowerCase() == 'true') {
        return true;
    }
    return false;
};
String.prototype.FillZero = function () {
    switch (String(arguments[0]).toLocaleLowerCase()) {
        case 'int':
            return parseInt(this) >= 10 ? parseInt(this) : parseInt("0" + this);
        default:
            return parseInt(this) >= 10 ? this : "0" + this;
    }
};
Date.prototype.ToString = function ()//时间转String
{
    var _this = this;
    var ref = null;
    var BackEmpty = false;
    var _year = _this.getFullYear();
    var _month = String(_this.getMonth() + 1).FillZero();
    var _day = String(_this.getDate()).FillZero();
    var _reg = _year + "-" + _month + "-" + _day;
    if (arguments[0] != undefined) {
        ref = arguments[0];
        ref = ref.replace('yyyy', _year);
        ref = ref.replace('MM', _month);
        ref = ref.replace('dd', _day);
        ref = ref.replace('HH', String(_this.getHours()).FillZero());
        ref = ref.replace('mm', String(_this.getMinutes()).FillZero());
        ref = ref.replace('ss', String(_this.getSeconds()).FillZero());
    }
    else {
        ref = _reg;
    }
    BackEmpty = (_reg === '1901-01-01' || _reg === '0001-01-01' || _reg === '2001-01-01') ? true : false;
    if (String(arguments[1]).Boolean() && BackEmpty) {
        return "";
    }
    return ref;
};
String.prototype.CreateHtml = function () {
    if (this.indexOf('<tr') > -1) {
        var div = document.createElement('div');
        div.innerHTML = '<table>' + this + '</table>';
        return div.firstChild.firstChild.firstChild;
    }
    else {
        var TagName = arguments[0] == undefined ? 'div' : arguments[0];
        var _temp = document.createElement(arguments[0]);
        _temp.innerHTML = this;
        return _temp.childNodes;
    }
};
String.prototype.NullReplaceEmpty = function () {
    if (this.replace(/\s/g, '') == 'null') {
        return "";
    }
    return this;
};
String.prototype.isNull = function () {
    if (this.replace(/\s/g, '') == 'null') {
        return true;
    }
    return false;
};
String.prototype.NotNull = function () {
    if (this.replace(/\s/g, '') != 'null') {
        return true;
    }
    return false;
};
String.prototype.NotEmpty = function () {
    if (this.replace(/\s/g, '').length > 0) {
        return true;
    }
    return false;
};
String.prototype.isNullOrEmpty = function () {
    if (this.replace(/\s/g, '').length == 0 || this.replace(/\s/g, '').toLocaleLowerCase() == 'null' || this.replace(/\s/g, '').toLocaleLowerCase() == 'undefined') {
        return true;
    }
    return false;
};
String.prototype.Format = function () {
    var arr = typeof (arguments[0]) == "string" ? arguments[0].ToArray(",") : arguments[0];
    if (arr.length == 0) {
        return this;
    };
    for (var s = this, i = 0; i < arr.length; i++) {
        s = s.replace(new RegExp("\\{" + i + "\\}", "g"), arr[i]);
    }
    return s;
};
String.prototype.FormatDate = function ()//格式化时间
{
    var _val = this.replace(new RegExp("-", 'g'), '/');
    return new Date(_val).ToString(arguments[0]);
};
String.prototype.ToArray = function () {
    if (arguments[0] != undefined) {
        return this.split(arguments[0]);
    }
    return this;
};
String.prototype.ToDate = function ()//返回js时间
{
    if (this.indexOf('0001-01-01') > -1) {
        return new Date('1901/01/01 00:00:00');
    }
    var _val = this.replace(new RegExp("-", 'g'), '/');
    return new Date(_val);
};
String.prototype.ReplaceSpecialWord = function () {
    var reg = new RegExp("[`~!@#$^&*()=|{}':;',\\[\\].<>/?~！@#￥……&*（）&mdash;—|{}【】‘；：”“'。，、？]");
    var _return = "";
    for (var i = 0; i < this.length; i++) {
        _return = _return + this.substr(i, 1).replace(reg, '');
    }
    return _return;
};
var Message = function () {
    var Fun = function () {
        this.Msg = null;//提示内容
        this.Timer = 30000;//关闭时间
        this.BtnName = null;
        this.CloseEvent = null;//关闭事件
        this.EnsureEvent = null;//确定事件
        this.EnsureName = null;//确定按钮名称
        this.CloseName = null;
    };
    var Filter = {
        Create: function () {
            var div = document.createElement("div");
            div.id = "filter";
            div.style.height = Pixel.H + "px";
            return div;
        },
        Remove: function () {
            if (document.getElementById("filter") != undefined) {
                document.getElementsByTagName("body")[0].removeChild(document.getElementById("filter"));
            }
        }
    };
    Fun.prototype.Box = function () {
        var FunCloseEvent = this.CloseEvent;
        var FunClose = this.Remove;
        var div = document.createElement("div");
        div.id = "msg";
        var _div = document.createElement("div");
        _div.innerHTML = '<h5>\u6e29\u99a8\u63d0\u793a\uff1a</h5><div>' + this.Msg + '</div>';
        div.appendChild(_div);
        var p = document.createElement("p");
        p.className = "tool";
        var _input = document.createElement("input");
        _input.type = "button";
        _input.className = "close";
        _input.value = "\u786e\u5b9a";
        _input.onclick = function () {
            FunClose();
            if (FunCloseEvent != null) {
                FunCloseEvent();
            }
        };
        p.appendChild(_input);
        div.appendChild(p);
        this.Create(div);
    };
    Fun.prototype.Confirm = function () {
        var FunCloseEvent = this.CloseEvent;
        var FunClose = this.Remove;
        var FunConfirmEvent = this.EnsureEvent;
        var div = document.createElement("div");
        div.id = "msg";
        var _div = document.createElement("div");
        _div.innerHTML = '<h5>\u6e29\u99a8\u63d0\u793a\uff1a</h5><div>' + this.Msg + '</div>';
        div.appendChild(_div);
        var p = document.createElement("p");
        p.className = "tool";
        var _input = document.createElement("input");
        _input.type = "button";
        _input.className = "ensure";
        _input.value = this.EnsureName != null ? this.EnsureName : "\u786e\u5b9a";
        _input.onclick = function () {
            FunClose();
            if (FunConfirmEvent != null) {
                FunConfirmEvent();
            }
        };
        p.appendChild(_input);
        _input = document.createElement("input");
        _input.type = "button";
        _input.className = "close";
        _input.value = this.CloseName != null ? this.CloseName : "\u53d6\u6d88";
        _input.onclick = function () {
            FunClose();
            if (FunCloseEvent != null) {
                FunCloseEvent();
            }
        };
        p.appendChild(_input);
        div.appendChild(p);
        this.Create(div);
    };
    Fun.prototype.Loading = function () {
        var _html = String(this.Msg).isNullOrEmpty() ? '\u52aa\u529b\u5904\u7406\u4e2d\u002e\u002e\u002e' : this.Msg;
        var FunClose = this.Remove;
        var div = document.createElement("div");
        div.id = "Loading";
        div.innerHTML = '<div><em class="c1"></em><em class="c2"></em><em class="c3"></em></div><p>' + _html + '</p>';
        this.Create(div);
        if (this.Timer > 0) {
            setTimeout(function () {
                if (document.getElementById("Loading") != undefined) {
                    Filter.Remove();
                    document.getElementsByTagName("body")[0].removeChild(document.getElementById("Loading"));
                }
            }, this.Timer);
        }
    };
    Fun.prototype.Alert = function () {
        var _html = this.Msg == null ? '\u6ca1\u6709\u66f4\u591a\u6570\u636e' : this.Msg;
        var div = document.createElement("div");
        div.id = "alert";
        div.innerHTML = _html;
        this.Create(div, true);
        div.style.marginLeft = -(div.offsetWidth / 2) + "px";
    };
    Fun.prototype.NoData = function () {
        var _html = this.Msg == null ? '\u6ca1\u6709\u66f4\u591a\u6570\u636e' : this.Msg;
        var div = document.createElement("div");
        div.id = "noData";
        var img = document.createElement("img");
        img.src = "../Images/msg.png";
        div.appendChild(img);
        var p = document.createElement("p");
        p.innerHTML = _html;
        div.appendChild(p);
        var _name = this.EnsureName == null ? "main" : this.EnsureName;
        this.Remove();
        if (document.getElementById(_name) != null) {
            document.getElementById(_name).innerHTML = "";
            document.getElementById(_name).appendChild(div);
        }
        else {
            document.getElementsByTagName("body")[0].appendChild(div);
        }
    };
    Fun.prototype.Create = function () {
        this.Remove();
        document.getElementsByTagName("body")[0].appendChild(arguments[0]);
        arguments[0].style.marginTop = -(arguments[0].offsetHeight / 2) + "px";
        if (arguments[1] == undefined) {
            document.getElementsByTagName("body")[0].appendChild(Filter.Create());
        }
    };
    Fun.prototype.Remove = function () {
        Filter.Remove();
        if (document.getElementById("msg") != undefined) {
            document.getElementsByTagName("body")[0].removeChild(document.getElementById("msg"));
        }
        if (document.getElementById("Loading") != undefined) {
            document.getElementsByTagName("body")[0].removeChild(document.getElementById("Loading"));
        }
    };
    return {
        Box: function () {
            var FunEvent = new Fun();
            FunEvent.Msg = arguments[0];
            if (arguments[1] != undefined) {
                FunEvent.CloseEvent = arguments[1];
            }
            FunEvent.Box();
        },
        Confirm: function () {
            var FunEvent = new Fun();
            FunEvent.Msg = arguments[0];
            if (arguments[1] != undefined) {
                FunEvent.EnsureEvent = arguments[1];
            }
            if (arguments[2] != undefined) {
                FunEvent.CloseEvent = arguments[2];
            }
            FunEvent.EnsureName = arguments[3] != undefined ? arguments[3] : null;
            FunEvent.CloseName = arguments[4] != undefined ? arguments[4] : null;
            FunEvent.Confirm();
        },
        Loading: function () {
            var FunEvent = new Fun();
            FunEvent.Msg = arguments[0] == undefined ? "" : arguments[0];
            FunEvent.Loading();
        },
        NoData: function () {
            var FunEvent = new Fun();
            FunEvent.Msg = arguments[0];
            FunEvent.EnsureName = arguments[1];
            FunEvent.NoData();
        },
        Alert: function () {
            var FunEvent = new Fun();
            FunEvent.Msg = arguments[0];
            FunEvent.Alert();
            setTimeout(function () {
                if (document.getElementById("alert") != undefined) {
                    document.getElementsByTagName("body")[0].removeChild(document.getElementById("alert"));
                }
            }, 3000);
        },
        Remove: function () {
            var FunEvent = new Fun();
            FunEvent.Remove();
        },
    };
}();
//显示与隐藏
function ShowOrHide() {
    var controllers = arguments[0];
    var dom = document.getElementById(arguments[1]);
    if (controllers.className.indexOf("cur") > -1) {
        AddClass(dom, "none");
        RemoveClass(controllers, "cur");
    }
    else {
        RemoveClass(dom, "none");
        AddClass(controllers, "cur");
    }
};
//滑动出现删除按钮
function DelInit() {
    var nl = document.getElementsByTagName("ul");
    for (var i = 0; i < nl.length; i++) {
        if (nl[i].className.indexOf("edit") > -1) {
            var _edit = nl[i].getElementsByTagName("li");
            for (var j = 0; j < _edit.length; j++) {
                _edit[j].addEventListener('touchstart',
                function (event) {
                    //event.preventDefault();
                    var touch = event.touches[0];
                    this.setAttribute("data-star", touch.pageX);
                    this.setAttribute("data-end", touch.pageX);
                });
                _edit[j].addEventListener('touchmove',
                function (event) {
                    this.setAttribute("data-end", event.targetTouches[0].pageX);
                });
                _edit[j].addEventListener('touchend',
                function (event) {
                    var star = this.getAttribute("data-star");
                    var end = this.getAttribute("data-end");
                    if (star - end > 50) {
                        this.className = "cur";
                    }
                    if (end - star > 50) {
                        this.className = "";
                    }
                });
            }
        }
    }
};
//AJAX 请求
function AJAXData() {
    if (typeof arguments[4] == "undefined" || arguments[4] == false) {
        Message.Loading();
    }
    var Url = arguments[0];
    var SumbitData = arguments[1];
    var SuccessEvent = arguments[2];
    var TimeOut = isNaN(parseInt(arguments[3])) ? 0 : parseInt(arguments[3]);
    var Method = SumbitData != null ? 'POST' : 'GET';
    if (TimeOut > 0) {
        var ajaxTimeoutTest = $.ajax({
            type: Method,
            timeout: TimeOut,
            url: Url,
            data: SumbitData,
            dataType: 'json',
            success: function (data, textStatus) {
                SuccessEvent(data);
            },
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                Message.Box("\u7f51\u7edc\u9519\u8bef：" + xmlHttpRequest.status);
            },
            complete: function (XMLHttpRequest, status) { //请求完成后最终执行参数
                if (status == 'timeout') {//超时,status还有success,error等值的情况
                    ajaxTimeoutTest.abort();
                    Message.Box("\u8bf7\u6c42\u8d85\u65f6");
                }
            }
        });
    }
    else {
        $.ajax({
            type: Method,
            url: Url,
            data: SumbitData,
            dataType: 'json',
            success: function (data, textStatus) {
                SuccessEvent(data);
            },
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                Message.Box("\u7f51\u7edc\u9519\u8bef：" + xmlHttpRequest.status);
            }
        });
    }
};
function AJAXDataJsonP() {
    if (typeof arguments[4] == "undefined" || arguments[4] == false) {
        Message.Loading();
    }
    var Url = arguments[0];
    var SumbitData = arguments[1];
    var SuccessEvent = arguments[2];
    var TimeOut = isNaN(parseInt(arguments[3])) ? 0 : parseInt(arguments[3]);
    var Method = SumbitData != null ? 'POST' : 'GET';
    if (TimeOut > 0) {
        var ajaxTimeoutTest = $.ajax({
            type: Method,
            timeout: TimeOut,
            url: Url,
            data: SumbitData,
            dataType: 'jsonp',
            jsonpCallback: "person",
            success: function (data) {
                SuccessEvent(data);
            },
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                Message.Box("\u7f51\u7edc\u9519\u8bef：" + xmlHttpRequest.status);
            },
            complete: function (XMLHttpRequest, status) { //请求完成后最终执行参数
                if (status == 'timeout') {//超时,status还有success,error等值的情况
                    ajaxTimeoutTest.abort();
                    Message.Box("\u8bf7\u6c42\u8d85\u65f6");
                }
            }
        });
    }
    else {
        $.ajax({
            type: Method,
            url: Url,
            data: SumbitData,
            dataType: 'jsonp',
            jsonpCallback: "person",
            success: function (data) {
                SuccessEvent(data);
            },
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                Message.Box("\u7f51\u7edc\u9519\u8bef：" + xmlHttpRequest.status);
            }
        });
    }
};

//选项卡滑动切换
function TabInit() {
    var dl = document.getElementsByTagName("dl");
    var ctrl = arguments[0];
    for (var i = 0; i < dl.length; i++) {
        if (dl[i].getAttribute("data-event") == "Tab") {
            var nl = dl[i].getElementsByTagName("dt")[0].getElementsByTagName("span");
            for (var j = 0; j < nl.length; j++) {
                (function (_j, _nl, Tab) {
                    nl[j].onclick = function () {
                        var _ul = Tab.getElementsByTagName("dd")[0].getElementsByTagName("ul");
                        for (var k = 0; k < _nl.length; k++) {
                            if (k == _j) {
                                AddClass(_nl[k], "cur");
                                if (Tab.getAttribute("data-ctrl") != null) {
                                    eval(Tab.getAttribute("data-ctrl"));
                                }
                                else {
                                    if (_ul.length > k) {
                                        RemoveClass(_ul[k], "none");
                                    }
                                    window.scroll(0, 0);
                                }
                            }
                            else {
                                RemoveClass(_nl[k], "cur");
                                if (Tab.getAttribute("data-ctrl") != null) {
                                    //eval(Tab.getAttribute("data-ctrl"));
                                }
                                else {
                                    if (_ul.length > k) {
                                        AddClass(_ul[k], "none");
                                    }
                                }
                            }
                        }
                    }
                })(j, nl, dl[i]);
            }
        }
    }
};


function TabCut() {
    var dl = document.getElementsByTagName("dl");
    var ctrl = arguments[0];
    for (var i = 0; i < dl.length; i++) {
        if (dl[i].getAttribute("data-event") == "CutTab") {
            var nl = dl[i].getElementsByTagName("dt")[0].getElementsByTagName("span");
            for (var j = 0; j < nl.length; j++) {
                (function (_j, _k, Tab) {
                    nl[j].onclick = function () {
                        var _ul = new Array();
                        var _sul = Tab.getElementsByTagName("dd");
                        for (var l = 0; l < _sul.length; l++) {
                            if (_sul[l].getAttribute("data-cut") != null) {
                                _ul.push(_sul[l]);
                            }
                        }
                        for (var k = 0; k < _k; k++) {
                            if (k == _j) {
                                AddClass(nl[k], "cur");
                                if (_ul.length > k) {
                                    RemoveClass(_ul[k], "none");
                                }
                            }
                            else {
                                RemoveClass(nl[k], "cur");
                                if (_ul.length > k) {
                                    AddClass(_ul[k], "none");
                                }
                            }
                        }
                    }
                })(j, nl.length, dl[i]);
            }
        }
    }
};
//单选按钮
function RadioInit() {
    var ul = document.getElementsByTagName("ul");
    for (var i = 0; i < ul.length; i++) {
        if (ul[i].getAttribute("data-event") == "Radio") {
            var nl = ul[i].getElementsByTagName("li");
            for (var j = 0; j < nl.length; j++) {
                (function (_j, _k) {
                    nl[j].onclick = function () {
                        for (var k = 0; k < _k; k++) {
                            if (k == _j) {
                                AddClass(this, "cur");
                            }
                            else {
                                RemoveClass(nl[k], "cur");
                            }
                        }
                    }
                })(j, nl.length);
            }
        }
    }
};
//复选按钮
function CheckBoxInit() {
    var ul = document.getElementsByTagName("ul");
    for (var i = 0; i < ul.length; i++) {
        if (ul[i].getAttribute("data-event") == "CheckBox") {
            var nl = ul[i].getElementsByTagName("li");
            for (var j = 0; j < nl.length; j++) {
                nl[j].onclick = function () {
                    if (this.className.indexOf("cur") > -1) {
                        RemoveClass(this, "cur");
                    }
                    else {
                        AddClass(this, "cur");
                    }
                }
            };
        }
    }
};
//url跳转
function Redirect() {
    var Url = arguments[0];
    var Params = arguments[1];
    if (Url == null) {
        return;
    }
    if (!String(Params).isNullOrEmpty()) {
        Url = Url + "?" + Params;
    }
    window.location = Url;
};
//Html排序
function OrderBy() {
    var _nl = arguments[0], _sort = arguments[1], _dom = arguments[2];
    var _arr = [];
    for (var i = 0; i < _nl.length; i++) {
        _arr.push(_nl[i]);
    }
    if (arguments[3] != undefined) {
        _arr.sort(function (a, b) { return b.getAttribute(_sort) - a.getAttribute(_sort) });
    }
    else {
        _arr.sort(function (a, b) { return a.getAttribute(_sort) - b.getAttribute(_sort) });
    }
    for (var j = 0; j < _arr.length; j++) {
        _dom.appendChild(_arr[j]);
    }
};
var db = null;
//WebSQL
function CreateDataBase() {
    db = openDatabase('DbWebDataBase', '1.0', 'DBWEBSQL', 2 * 1024);
};
function CreateTable() {
    var TableName = arguments[0];
    if (db != null) {
        db.transaction(function (tx) {
            tx.executeSql('CREATE TABLE IF NOT EXISTS ' + TableName);
        });
    }
};
function InsertData() {
    var TableName = arguments[0];
    var ColumeLen = arguments[1];
    var TableValue = arguments[2];
    if (db != null) {
        db.transaction(function (tx) {
            tx.executeSql("insert into " + TableName + " values(" + ColumeLen + ")", TableValue, function () { }, function (tx, error) { alert(error.message); });
        });
    }
};
function SelectData() {
    var TableName = arguments[0];
    var fun = arguments[1];
    var _results = null;
    db.transaction(function (tx) {
        tx.executeSql('SELECT * FROM ' + TableName, [], function (tx, results) {
            fun(results);
        }, null);
    });
};

function SetContent() {
    var Main = document.getElementById("main");
    if (Main != null) {
        var Remove = null;
        if (arguments[0] == null) {
            Remove = parseInt(Main.getAttribute("data-calc"));
        }
        else {
            Remove = parseInt(arguments[0]);
        }
        Main.style.height = document.documentElement.clientHeight - Remove + "px";
    }
};
var Color = ['#60baff', '#b283ff', '#ff70b0', '#60f6ff', '#54e7b8', '#86ed5d', '#c2e94c', '#ffdb5f', '#ff6060', '#ff9460', '#ff84fb', '#728dff'];
(function () {
    TabInit();
    TabCut();
    RadioInit();
    CheckBoxInit();
    SetContent();
    if (!isAndroid) {
        document.documentElement.style.webkitTouchCallout = "none"; //禁止弹出菜单
        document.documentElement.style.webkitUserSelect = "none";//禁止选中
    }
})();
window.onresize = function () {
    SetContent();
};
function convertImgToBase64(url, callback, width, height, outputFormat) {
    var canvas = document.createElement('CANVAS'),
      ctx = canvas.getContext('2d'),
      img = new Image;
    var _width = width || 60;
    var _height = height || 60;
    img.crossOrigin = 'Anonymous';
    img.onload = function () {
        canvas.height = _width;
        canvas.width = _height;
        ctx.drawImage(img, 0, 0, _width, _height);
        var dataURL = canvas.toDataURL(outputFormat || 'image/jpeg');
        callback.call(this, dataURL);
        canvas = null;
    };
    img.src = url;
}
var SharePop = {
    show: function () {
        var data = arguments[0];
        var node = document.querySelector("#share_weapper");
        var filter = document.querySelector("#filter");
        if (node == null) {
            node = document.createElement("div");
            node.id = "share_weapper";
            var content = document.createElement("div");
            content.className = 'share_weapper share_weapper_animation';
            var tit = document.createElement("div");
            tit.className = "title";
            tit.innerText = '\u5206\u4eab';
            var cancel = document.createElement("div");
            cancel.className = 'cancel';
            cancel.innerText = " \u53d6\u6d88";
            cancel.onclick = function () {
                node.remove();
                filter.remove();
            };
            var ul = document.createElement("ul");
            var li1 = document.createElement("li");
            var li2 = document.createElement("li");
            li1.innerText = '\u5fae\u4fe1\u670b\u53cb\u5708';
            li2.innerText = '\u5fae\u4fe1\u597d\u53cb';
            li1.onclick = function () {
                data.scene = 1;
                Andoggy.ShareLink(data);
                node.remove();
                filter.remove();
            };
            li2.onclick = function () {
                data.scene = 0;
                Andoggy.ShareLink(data);
                node.remove();
                filter.remove();
            };
            ul.appendChild(li1);
            ul.appendChild(li2);
            content.appendChild(tit);
            content.appendChild(ul);
            content.appendChild(cancel);
            node.appendChild(content);
            document.querySelector("body").appendChild(node);
            setTimeout(function () {
                AddClass(content, "share_weapper_open");
            }, 50);
        }
        if (filter == null) {
            filter = document.createElement("div");
            filter.id = "filter";
            document.querySelector("body").appendChild(filter);
        }
    }
};
var GoTop = function () {
    var _body = document.getElementsByTagName("body")[0];
    window.onscroll = function () {
        var _scrollTop = document.documentElement.scrollTop + document.body.scrollTop;
        if (Pixel.H / 2 < _scrollTop) {
            if (!document.getElementById("goTop")) {
                var div = document.createElement("div");
                div.id = "goTop";
                div.className = "goTop";
                div.innerHTML = '<span class="arrow"><span class="arrowTop"></span></span>';
                div.onclick = function () {
                    document.documentElement.scrollTop = document.body.scrollTop = 0;
                };
                _body.appendChild(div);
            }
        }
        else {
            if (document.getElementById("goTop")) {
                _body.removeChild(document.getElementById("goTop"));
            }
        }
    };
};
var ShowCart = function (callback) {
    var _body = document.getElementsByTagName("body")[0];
    if (!document.getElementById("showCart")) {
        var div = document.createElement("div");
        div.id = "showCart";
        div.className = "showCart";
        div.innerHTML = '<span class="arrow"></span>';
        div.onclick = function () {
            if (typeof (callback) == "function") {
                callback();
            }
        };
        _body.appendChild(div);
    }
};
var shopApp = (function (doc) {
    // 是否执行运动的标志量
    var flagMove = false;
    // 根据两点坐标以及曲率确定运动曲线函数（也就是确定a, b的值）
    /* 公式： y = a*x*x + b*x + c;
    */
    var a = 0, b = 0, c = 0;
    var moveStyle = "margin", testDiv = doc.createElement("div");
    var element = null;
    var defaults = {
        speed: 166.67, // 每帧移动的像素大小，每帧（对于大部分显示屏）大约16~17毫秒
        curvature: 0.001,  // 实际指焦点到准线的距离，你可以抽象成曲率，这里模拟扔物体的抛物线，因此是开口向下的
        progress: function () { },
        complete: function () { }
    };
    var params = {};
    var DragDom = {
        Create: function () {
            DragDom.Remove();
            var node = doc.createElement("div");
            node.style.position = "fixed";
            node.id = "TempDragDom";
            node.style.width = "48px";
            node.style.height = "64px";
            node.style.left = node.clientLeft;
            doc.body.appendChild(node);
            return node;
        },
        Remove: function () {
            var node = doc.getElementById("TempDragDom");
            if (node != null) {
                node.remove();
            }
        }
    };
    var exports = {
        mark: function () { return this; },
        position: function () { return this; },
        move: function () { return this; },
        init: function () { return this; }
    };

    exports.init = function (node, target, options) {
        options = options || {};
        for (var key in defaults) {
            params[key] = options[key] || defaults[key];
        }
        /* 确定移动的方式 
         * IE6-IE8 是margin位移
         * IE9+使用transform
        */
        var moveStyle = "margin", testDiv = doc.createElement("div");
        if ("oninput" in testDiv) {
            ["", "ms", "webkit"].forEach(function (prefix) {
                var transform = prefix + (prefix ? "T" : "t") + "ransform";
                if (transform in testDiv.style) {
                    moveStyle = transform;
                };
            });
        };
        a = params.curvature;
        flagMove = true;
        element = DragDom.Create();
        if (element && target && element.nodeType == 1 && target.nodeType == 1) {
            var rectElement = {}, rectTarget = {};
            // 移动元素的中心点位置，目标元素的中心点位置
            var centerElement = {}, centerTarget = {};
            // 目标元素的坐标位置
            var coordElement = {}, coordTarget = {};
            var scrollTop = document.documentElement.scrollTop || document.body.scrollTop;
            if (document.getElementById("main")) {
                scrollTop = document.getElementById("main").scrollTop;
            }
            element.style.left = node.offsetLeft + "px";
            element.style.top = node.offsetTop - scrollTop + "px";
            var img = document.createElement("img");
            img.src = node.querySelector("img").src;
            img.style.width = "48px";
            img.style.height = "64px";
            element.appendChild(img);
            exports.mark = function () {
                if (flagMove == false) return this;
                if (typeof coordElement.x == "undefined") this.position();
                element.setAttribute("data-center", [coordElement.x, coordElement.y].join());
                target.setAttribute("data-center", [coordTarget.x, coordTarget.y].join());
                return this;
            };
            exports.position = function () {
                if (flagMove == false) { return this; };

                var scrollLeft = document.documentElement.scrollLeft || document.body.scrollLeft,
                    scrollTop = document.documentElement.scrollTop || document.body.scrollTop;

                // 初始位置
                if (moveStyle == "margin") {
                    element.style.marginLeft = element.style.marginTop = "0px";
                } else {
                    element.style[moveStyle] = "translate(0, 0)";
                }

                // 四边缘的坐标
                rectElement = element.getBoundingClientRect();
                rectTarget = target.getBoundingClientRect();

                // 移动元素的中心点坐标
                centerElement = {
                    x: rectElement.left + (rectElement.right - rectElement.left) / 2 + scrollLeft,
                    y: rectElement.top + (rectElement.bottom - rectElement.top) / 2 + scrollTop
                };

                // 目标元素的中心点位置
                centerTarget = {
                    x: rectTarget.left + (rectTarget.right - rectTarget.left) / 2 + scrollLeft,
                    y: rectTarget.top + (rectTarget.bottom - rectTarget.top) / 2 + scrollTop
                };

                // 转换成相对坐标位置
                coordElement = {
                    x: 0,
                    y: 0
                };
                coordTarget = {
                    x: -1 * (centerElement.x - centerTarget.x),
                    y: -1 * (centerElement.y - centerTarget.y)
                };

                /*
                 * 因为经过(0, 0), 因此c = 0
                 * 于是：
                 * y = a * x*x + b*x;
                 * y1 = a * x1*x1 + b*x1;
                 * y2 = a * x2*x2 + b*x2;
                 * 利用第二个坐标：
                 * b = (y2+ a*x2*x2) / x2
                */
                // 于是
                b = (coordTarget.y - a * coordTarget.x * coordTarget.x) / coordTarget.x;

                return this;
            };
            exports.move = function () {
                // 如果曲线运动还没有结束，不再执行新的运动
                if (flagMove == false) return this;

                var startx = 0, rate = coordTarget.x > 0 ? 1 : -1;

                var step = function () {
                    // 切线 y'=2ax+b
                    var tangent = 2 * a * startx + b; // = y / x
                    // y*y + x*x = speed
                    // (tangent * x)^2 + x*x = speed
                    // x = Math.sqr(speed / (tangent * tangent + 1));
                    startx = startx + rate * Math.sqrt(params.speed / (tangent * tangent + 1));

                    // 防止过界
                    if ((rate == 1 && startx > coordTarget.x) || (rate == -1 && startx < coordTarget.x)) {
                        startx = coordTarget.x;
                    }
                    var x = startx, y = a * x * x + b * x;
                    // x, y目前是坐标，需要转换成定位的像素值
                    if (moveStyle == "margin") {
                        element.style.marginLeft = x + "px";
                        element.style.marginTop = y + "px";
                    } else {
                        element.style[moveStyle] = "translate(" + [x + "px", y + "px"].join() + ")";
                    }

                    if (startx !== coordTarget.x) {
                        params.progress(x, y);
                        window.requestAnimationFrame(step);
                    } else {
                        //回调
                        params.complete();
                        DragDom.Remove();
                        flagMove = true;
                    }
                };
                window.requestAnimationFrame(step);
                flagMove = false;

                return this;
            };
            this.mark().move()
        }
    };
    return exports;
})(document);

function GetDigest() {
    var message = arguments[0];
    var result = '';
    $.ajax({
        type: 'POST',
        url: '\WeiXinAPI\\GetDigest',
        data: {plainMessage:message},
        dataType: 'text',
        success: function (data, textStatus) {
            if (data.length > 0) {
                result = data;
                return result;
            }
        },
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            Message.Box("\u7f51\u7edc\u9519\u8bef：" + xmlHttpRequest.status);
        }
    });
}
