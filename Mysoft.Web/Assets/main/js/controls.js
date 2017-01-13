var MTable = function ($dom, $pagination) {
    this.$dom = $dom;
    this.$tbody = null;
    this.$pagination = $pagination;
    this.trRender = null;
    this.options = {
        IdKey: "Id"
    }
    this.paginationControl = new Pagination($pagination);
}
MTable.prototype = {
    Init: function () {
        this.$tbody = this.$dom.find('tbody');
        this.trRender = template.compile(this.$dom.find('.tbodyTemplate').html());

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

    },
    //data的结构{TotalCount,  Data =[{},{Id,Name}], PageIndex, PageSize}
    Draw: function (data) {
        this.Empty();
        for (var i = 0; i < data.Data.length; i++) {
            var row = data.Data[i];
            row.i = i + 1;
            this.Add(row);
        }

        //分页
        this.paginationControl.Draw(data);
        var control = this;
        this.$tbody.find('tr').unbind('click').bind('click', function () {
            control.$tbody.find('tr').removeClass('success');
            $(this).addClass("success")
        }).bind('dblclick', function () {
            alert(1);
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
    OnDoubleClick: function () {

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


$.fn.MTable = function () {
    var t = new MTable($(this));
    t.Init();
    return t;
}