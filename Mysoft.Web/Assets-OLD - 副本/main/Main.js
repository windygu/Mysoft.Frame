var utils = {

 
}


var MTable = function ($dom) {
    this.$dom = $dom;
    this._options = {


    }
}
MTable.prototype.InitOptions = function (self) {
    for (var key in this._options) {
        if (self[key]) {
            this._options[key]=self[key]
        }
    }
}