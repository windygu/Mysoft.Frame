


var MTable = function ($dom, $pagination) {
    this.$dom = $dom;
    this.$tbody = null;
    this.$pagination = $pagination;
    this.trRender = null;
    this.options = {
        IdKey: "Id",
        dataSource: null,

    }
    this.paginationControl = new Pagination($pagination);
}
MTable.prototype = {
    _bindOptions: function (defaultOptions, selfOptions) {
        if (!selfOptions)
            return;
        for (var key in defaultOptions) {
            if (selfOptions[key] !== undefined) {
                defaultOptions[key] = selfOptions[key];
            }
        }
    },
    Init: function (selfOptions) {
        this.$tbody = this.$dom.find('tbody');
        this.trRender = template.compile(this.$dom.find('.tbodyTemplate').html());
        this._bindOptions(this.options, selfOptions);
        this.Refresh();
        return this;
    },
    Add: function (row) {
        var trhtml = this.trRender(row);
        $(trhtml).attr("id", row[this.options.IdKey]).appendTo(this.$tbody).data(row);
    },
    Remove: function (id) {
        this.$tbody.find('tr[id=' + id + ']').remove();
    },
  
    Refresh: function () {
        var type = typeof (this.options.dataSource);
        var c = this; 
        if (type === "string") {
            var index = layer.load(1, {
                shade: [0.1, '#fff'] //0.1透明度的白色背景
            });
            $.ajax({
                url: this.options.dataSource,
                success: function (result) {
                    c.Draw(result);
                    layer.close(index);
                },
                error: function (xhr) {
                    layer.close(index);
                }
            })
        } else if (this.options.dataSource) {
            c.Draw(this.options.dataSource);
        }
    },
    //data的结构{TotalCount,  Data =[{},{Id,Name}], PageIndex, PageSize}
    Draw: function (data) {
        this.Empty();
        if (typeof (data) === "object" && !Array.isArray(data)) {
            //不是数组
            for (var i = 0; i < data.Data.length; i++) {
                var row = data.Data[i];
                row.i = i + 1;
                this.Add(row);
            }
            //分页
            this.paginationControl.Draw(data); 
        }
        else {
            //直接是数组
            for (var i = 0; i < data.length; i++) {
                var row = data[i];
                row.i = i + 1;
                this.Add(row);
            }
        }

        var control = this;
        this.$tbody.find('tr').unbind('click').bind('click', function () {
            control.$tbody.find('tr').removeClass('success');
            $(this).addClass("success")
        }).bind('dblclick', function () {
            control.OnDoubleClick($(this).data(), $(this));
        })
        return this;
    },
    Empty: function () {
        this.$tbody.html("");
    },

    GetSeletedRows: function () {
        var data = [];
        this.$tbody.find("tr.success").each(function () {
            data.push($(this).data());
        })
        return data;
    },
    OnDoubleClick: function (row) {

    }
}

var Pagination = function ($dom) {
    this.$dom = $dom;
    this.options = {
        type: 1
    }
}
Pagination.prototype = {
    Draw: function (pageinfo) {
        this.pageSize = pageinfo.PageSize;
        this.pageIndex = pageinfo.PageIndex;
        this.totalCount = pageinfo.TotalCount;
        this.totalPage = Math.ceil(this.totalCount / this.pageSize);
        this.pageType = pageinfo.PageType ? pageinfo.PageType : this.options.type;
        this["_draw_" + this.pageType]();

    },
    _draw_1: function () {
        var control = this;
        var container = '<div></div>'
        var ul = '<ul class="pagination"></ul>';
        var pre = '<li class=""><a href="#"><i class="glyphicon glyphicon-step-backward"></i></a></li> <li><a href="#"><i class="glyphicon glyphicon-backward"></i></a></li>';
        var item = '<li><a href="#">2</a></li>';
        var next = '<li><a href="#"><i class="glyphicon glyphicon-forward"></i></a></li> <li><a href="#"><i class="glyphicon glyphicon-step-forward"></i></a></li>';
        var countText = '<div class="pagination-totaltext"><span></span></div>'
        var pages = [];
        if (this.totalPage < 10) {
            for (var i = 0; i < this.totalPage; i++) {
                pages[i] = i + 1;
            }
        } else {
            if (this.pageIndex <= 3) {
                pages = [1, 2, 3, 4, 5, 0];
            } else if (this.pageIndex >= this.totalPage - 3) {
                pages = [0, this.totalPage - 4, this.totalPage - 3, this.totalPage - 2, this.totalPage - 1, this.totalPage];
            } else {
                pages = [0, this.pageIndex - 2, this.pageIndex - 1, this.pageIndex, this.pageIndex + 1, this.pageIndex + 2, 0];
            }
        }
        var $container = $(container);
        var $html = $(ul);
        var $pre = $(pre);
        var $next = $(next);
        if (this.pageIndex == 1) {
            $pre.attr("disabled", "disabled").addClass("disabled")
        }
        $pre.first().find('a').attr('pageValue', 1);
        $pre.last().find('a').attr('pageValue', this.pageIndex - 1)
        $pre.appendTo($html);
        for (var i = 0; i < pages.length; i++) {
            var $item = $(item);
            if (pages[i] == this.pageIndex) {
                $item.addClass("active");
            }
            if (pages[i] > 0) {
                $item.find('a').html(pages[i])
            } else {
                $item.addClass("disabled").find('a').html("...")
            }
            $item.appendTo($html);
        }
        if (this.pageIndex == this.totalPage) {
            $(pre).find("li").addClass("disabled")
        }
        $next.first().find('a').attr('pageValue', this.pageIndex + 1);
        $next.last().find('a').attr('pageValue', this.totalPage)
        $next.appendTo($html);


        $countText = $(countText);
        $countText.find('span').text("共" + this.totalPage + "页、" + this.totalCount + "行");

        //$html.appendTo($container);
        //$countText.appendTo($container);
        $html.appendTo(this.$dom);
        $countText.appendTo(this.$dom);

        // $container  .appendTo(this.$dom);
        $html.find('li a').click(function () {
            if (!$(this).parents('li').hasClass('disabled')) {
                var newpage = $(this).attr('pageValue');
                if (!newpage)
                    newpage = $(this).text();
                control.OnChange(parseInt(newpage));
            }
        })
    },
    OnChange: function (page) {
        alert(page)
    }

}


$.fn.MTable = function (options) {
    var t = new MTable($(this));
    t.Init(options);
    return t;
}

$.fn.setText = function (txt) {
    if (this.attr('ng-text') !== undefined) {
        this.val(txt);
    }
}
$.fn.getVal = function () {
    var val;
    if (this.is(':Checkbox')) {
        val = this.prop('checked');
    } else if (this.attr('ng-text') !== undefined) {
        return this.data("value");
    } else {
        val = this.val();
    }
    return val;
};
$.fn.setVal = function (val) {
    if (this.is(':Checkbox')) {
        this.prop('checked', val);
    } else if (this.attr('ng-text') !== undefined) {
        this.data("value", val);
    } else {
        this.val(val);
    }
};

var MForm = function ($form) {
    this.$form = $form;
    this.data = null;
}
MForm.prototype = {
    Bind: function (data) {
        this.Reset();
        var c = this;
        if (!data) {
            this.data = {};
            this.$form.find('[ng-bind]').each(function () {
                var name = $(this).attr('ng-bind');
                var val = $(this).getVal();
                c.data[name] = val;
            })
        } else {
            this.data = data;
        } 
        this.$form.find('[ng-bind]').each(function () {
            var name = $(this).attr('ng-bind');
            var val = $(this).getVal();
            $(this).setVal(c.data[name])
            $(this).unbind('change').change(function () {
                c.data[name] = $(this).getVal();
            })
        })
        return this;
    },
    GetData: function () {
        return this.data;
    },
    SetItem: function (key, val)
    {
        this.data[key] = val;
        this.$form.find('[ng-bind="'+key+'"]').each(function () {
            $(this).setVal(val)
        })
    },
    Reset: function () {
        this.$form[0].reset();
    }
}

var MLookUp = function (dom){
    this.$dom = dom;
    this.$content = null;
    this.$ipt = null;
    this.options = {
        idKey: "Id",
        textKey:"Name",
        dataSource: null,       
        content: 
            '<div class="table-responsive">'+
                '<table id="tab-MetaClassLookUp" class="table table-hover table-striped no-margins">'+
                    '<thead>'+
                        '<tr class="navy-bg">'+
                            '<th width="60px" style="text-align:center">序号</th>'+
                            '<th style="text-align:center">名称</th>'+
                            '<th style="text-align:center">表名</th>'+
                            '<th style="text-align:center">状态</th>'+
                        '</tr>'+
                    '</thead>'+
                    '<tbody class="tbodyTemplate">'+
                        '<tr>'+
                            '<td align="center">{{i}}</td>'+
                            '<td align="center">{{Name}}</td>'+
                            '<td align="center">{{TableName}}</td>'+
                            '<td align="center">{{Id}}</td>'+
                        '</tr>'+
                    '</tbody>'+
                '</table> '+
            '</div>'
    }
}
MLookUp.prototype = {
    _bindOptions: function (defaultOptions, selfOptions) {
        if (!selfOptions)
            return;
        for (var key in defaultOptions) {
            if (selfOptions[key] !== undefined) {
                defaultOptions[key] = selfOptions[key];
            }
        }
    },
    _initUI: function () {
        this.$content = $(this.options.content);
        this.$content.hide().appendTo($('body'));
        this.tableContent = new MTable(this.$content);
        this.tableContent.Init({ dataSource: this.options.dataSource });
        var c = this;
        this.tableContent.OnDoubleClick = function (row) {
            c.OnSelected(row);
            if (c._layerIndex) {
                layer.close(c._layerIndex);
            }
        }
        //初始化弹出按钮
       
        if (this.$dom.is('input')) {
            this.$ipt = this.$dom;
            this.$dom.click(function () {
                c.Open();
            })
        } else {
            this.$dom.find('input,button').click(function () {
                c.Open();
            })
            this.$ipt = this.$dom.find('input');
        }
        

    },
    Init: function (selfOptions) {
        this._bindOptions(this.options, selfOptions);
        this._initUI();
    },
    Open: function () {
        var c = this;
        this._layerIndex = layer.open({
            type: 1,
            skin: 'layui-layer-rim', //加上边框
            area: ['60%', '60%'], //宽高
            content: this.$content,//['/Control/metaclasslookup','no'],
            btn: ['确认', '取消'],
            yes: function (index, layero) {
                var rows = c.tableContent.GetSeletedRows();
                if (rows.length > 0) {
                    c.OnSelected(rows[0]);
                    layer.close(index);
                }
            },
            cancel: function () {

            }
        });
    },
    OnSelected:function(data){
        this.$ipt.setText(data[this.options.textKey])
        this.$ipt.setVal(data[this.options.idKey])
    }

}

