/// <reference path="ThirdParty/jquery-vsdoc.js" />

// Log script errors using ELMAH
window.onerror = function (msg, url, lineNo) {
    /// This method is based on jquery.ajax. Don't want to rely on jquery incase an issue loading that occurs.
    var data = {
        message: msg,
        scriptUrl: url,
        pageUrl: location.href,
        lineNumber: lineNo,
        stacktrace: printStackTrace().join('\n\n')
    };

    var xhr = window.ActiveXObject ? new ActiveXObject("Microsoft.XMLHTTP") : new XMLHttpRequest();

    xhr.open('POST', '/Error/LogClientError');
    xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xhr.setRequestHeader("X-Requested-With", "XMLHttpRequest");
    xhr.setRequestHeader("Accept", "application/json, text/javascript, */*");

    var a = [];
    for (var v in data) {
        a.push(encodeURIComponent(v) + "=" + encodeURIComponent(data[v]));
    }

    var message = a.join("&").replace(/%20/g, "+");
    xhr.send(message);
    return false;
};

// needed for table sorter 
$(function () {
    $("table.results th[class~='header']").click(function () {
        var ref = this.id;
        $("#Sort").val(ref);
        $(this).parents("form").submit();
    });

    //$("table.results tbody tr:odd").addClass("alt");

});

function startLoading(selector) {
    $(selector).attr("disabled", true);
    $("<img id=\"ajaxLoader\" src=\"{0}\" alt=\"loading\" />".format("/content/images/ajax-loader.gif")).insertAfter(selector);
}

function finishLoading(selector) {
    $(selector).removeAttr("disabled");
    $(selector).next("img").remove();
}

function startLoadingArea(selector) {
    $(selector).html("<img id=\"ajaxLoader\" src=\"{0}\" alt=\"loading\" />".format("/content/images/ajax-loader.gif"));
}

function modalConfirm(dialogId, callback) {
    $.colorbox({ width: "500", inline: true, href: "#" + dialogId, escKey: false, overlayClose: false, title: "Please confirm", fixed: true,
        // IE6 ignores z-index for selects, so we need to hide all of them and then show them again after the colorbox is closed
        onLoad: function () {
            $('select').each(function () {
                $(this).hide();
            });

            $('#cboxClose').remove();
        }
    });

    $('#yesBtn').live('click', function (e) {
        $.colorbox.close();
        $('select').each(function () {
            $(this).show();
        });
        callback(true);
    });

    $('#noBtn').live('click', function (e) {
        $.colorbox.close();
        $('select').each(function () {
            $(this).show();
        });
        callback(false);
    });
}

function modalMessage(dialogId) {
    $.colorbox({ width: "500", inline: true, href: "#" + dialogId, escKey: false, overlayClose: false, title: "Information", fixed: true,
        onLoad: function () {
            $('#cboxClose').remove();
        }
    });

    $('#okBtn').live('click', function (e) {
        $.colorbox.close();
        callback(false);
    });
}
