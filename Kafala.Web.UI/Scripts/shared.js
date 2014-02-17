/// <reference path="ThirdParty/jquery-vsdoc.js" />


$("form").submit(function (event) {

    var isValid = $(this).validate().valid();

    return isValid; //True will allow submission, false will not

});


    jQuery.fn.confirmDialog = function(messageText, dialogTitle, confirmButtonText, cancelButtonText, functionToCall) {

        var element = $(this[0]);
        if (!dialogTitle) {
            dialogTitle = "Confirm";
        }

        if (!confirmButtonText) {
            confirmButtonText = "Yes";
        }


        if (!cancelButtonText) {
            cancelButtonText = "No";
        }

        return $("#confirmModal").dialog({
      //      title: dialogTitle,
            modal: true,
            position: "center",
            resizable: false,
            
            buttons:
            [
                {
                    text: confirmButtonText,
                    click: function() {
                        $(this).dialog('close');
                        functionToCall();
                        return true;
                    }
                },
                {
                    text: cancelButtonText,
                    click: function() {
                        $(this).dialog('close');
                        return false;
                    }
                }        
            ]
        });

    };
    

    jQuery.fn.modalMessage = function (messageText, dialogTitle, dismissButtonText) {

        if (dismissButtonText == null) {
            dismissButtonText = "Dismiss";
        }

        if (dialogTitle == null) {
            dialogTitle = "Message";
        }
        
        $("#messageModelContent").html(messageText);
        $("#messageModelTitle").html(dialogTitle);
        $("#messageModelDismissButton").html(dismissButtonText);
        $("#messageModel").modal('show');
    };


    function ShowLoadingScreen(loaderElement) {
        if (loaderElement) {
            loaderElement.modal("show");
        } else {
            $("#processing-modal").modal('show');
        }
        
    }
    

    function HideLoadingScreen(loaderElement) {
        if (loaderElement) {
            loaderElement.modal("hide");
        } else {
            $("#processing-modal").modal('hide');
        }
    }


        function LoadContentIntoArea(url, areaId, title, dismissButtonText, hideButtonsAndTitle) {
            if (areaId == null) {
                areaId = "#detailsModel";
            }
            
            if (dismissButtonText == null) {
                dismissButtonText = "Dismiss";
            }


            if (title == null) {
                title = "Details";
            }

          
            
            

            ShowLoadingScreen();
            $.ajax(
                {
                    url: url
                })
                .success(function (data) {
                    HideLoadingScreen();

                    $("#detailModelContent").html(data);
                    $.validator.unobtrusive.parse($("#detailModelContent"));
                    $("#detailsModelTitle").html(title);
                    $("#detailsModelDismissButton").html(dismissButtonText);

                    if (hideButtonsAndTitle === "true" || hideButtonsAndTitle === "True") {
                        $("#detailsModelHeader").hide();
                        $("#detailsModelFooter").hide();
                    }
                    $("#detailsModel").modal('show');


                })
                .complete(function () {
                    HideLoadingScreen();
                    foundationInitialize();

                });

        }


        function LoadContentIntoPopOver(url, element) {
            hideAllPopovers();
            
            $(element).popover({
                animation: true,
                html: true,
                content: $("#processing-modal").html(),
            }).popover("show");

            $.ajax(
                {
                    url: url
                })
                .success(function(data) {
                    $(element).popover({
                        animation: true,
                        html: true,
                        content: data
                    }).popover("show");
                })
                    .complete(function () {
                        foundationInitialize();
                    });
        }

        function hideAllPopovers() {
            $('.showInBaloonOnMouseOver').each(function () {
                $(this).popover('destroy');
            });
        };
        
        $(document).on('click', function (e) {
            hideAllPopovers();
        });

        $(function () {
            foundationInitialize();

            $(document).ajaxError(function(xhr, ajaxOptions, thrownError) {
                HideLoadingScreen();
                
                $(document).modalMessage("An error has occurred while processing your request.", "Error", "Close");
            });
        });


       function foundationInitialize() {
            $(".datepicker").datepicker(
                {
                    onSelect: function () { }
                });

            $('.form').each(function () {
                $(this).valid();
            });

           
            $(".showInPopUpOnClick").click(function() {
                event.preventDefault();
                var url, title, dismissButton, hideHeaderAndButtons ;
                url = (this).getAttribute("href");
                title = (this).getAttribute("data-popup-title");
                dismissButton = (this).getAttribute("data-popup-dismissButtonText");
                hideHeaderAndButtons = (this).getAttribute("data-popup-hideButtonAndHeader");
                LoadContentIntoArea(url, "detailsModel" , title, dismissButton, hideHeaderAndButtons);
            });
            
            $(".showInBaloonOnMouseOver").click(function () {
                event.preventDefault();
                var url;
                url = (this).getAttribute("href");
                LoadContentIntoPopOver(url, this);
            });
            
            $(".showInBaloonOnMouseOver").blur(function () {
                event.preventDefault();
                (this).popover("destroy");
            });
            
           
           

            $("form").each(function () {
        
                var settings = $(this).data('validator').settings;

                var oldErrorPlacementFunction = settings.errorPlacement;
                var oldFail = settings.fail;
                var oldInvalidHandler = settings.invalidHandler;


                settings.highlight = function(element) {
                    $(element).closest('.form-group').addClass('has-error');
                };

                settings.unhighlight = function(element) {
                    $(element).closest('.form-group').removeClass('has-error');
                };

                settings.errorElement = "span";
                settings.errorClass = "help-block";

                settings.errorPlacement = function(error, element) {
                    if (element.parent('.input-group').length) {
                        error.insertAfter(element.parent());
                    } else {
                        error.insertAfter(element);
                    }

                    oldErrorPlacementFunction(error, element);
                    return false;
                };

                settings.fail = function(element) {
                    $(element).closest('.form-group').addClass('has-error');
                    oldFail(element);
                };

                settings.invalidHandler = function(event, validator) {
                    invalidHandler(event, validator);
                };
            });
        }

        // Log script errors using ELMAH
        window.onerror = function (msg, url, lineNo) {
            /// This method is based on jquery.ajax. Don't want to rely on jquery incase an issue loading that occurs.
            var data = {
                message: msg,
                scriptUrl: url,
                pageUrl: location.href,
                lineNumber: lineNo,
                stacktrace: window.printStackTrace().join('\n\n')
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
            //xhr.send(message);
            //alert(window.printStackTrace().join('\n\n'));
            return false;
        };

        // needed for table sorter 
        $(function () {
            $("table.sorter th[class~='SortableHeader']").click(function () {
                var ref = this.id;
                var sorthandler = $("input[id~='Sort']");
                sorthandler.val(ref);
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

        
