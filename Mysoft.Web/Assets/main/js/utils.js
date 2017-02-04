function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

Ajax = function (obj) {
    $.ajax({
        url: obj.url,
        data: obj.data,
        success: function (result) {
            if (result && result.Result) {

            }
        }, error: function (xhr) {

        }
    })
}

IsArray = function (o) {
    return Object.prototype.toString.call(o) === '[object Array]';
}