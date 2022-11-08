/// <reference path="jquery-3.3.1.js" />

// Ajax data types..
var dataType = {
    applicationJson: 'application/json; charset=utf-8',
    json: 'json',
    text: 'text/plan',
    html: 'html'
};
var type = {
    get: 'GET',
    post: 'POST',
    put: 'PUT',
    delete: 'Delete'
};

var preparationType = {
    production: "Production",
    packaging: "Packaging"
};

var reqStatus = {
    pending: "Pending",
    reject: "Rejected",
    recheck: "Rechecked",
    approved: "Approved",
    accepted: "Accepted",
    canceled: "Canceled",
    checked: "Checked",
    handOver: "HandOver",
    receive_return: "Receive-Return",
};

var flag = {
    view: "View",
    search: "Search",
    info: "Info",
    detail: "Detail",
    report: "Report"
}

var stockStatus = {
    stockIn: "Stock-In",
    stockOut: "Stock-Out",
    stockReturn: "Stock-Return"
};

var jobOrderStatus = {
    pendingJobOrder: "Pending-JobOrder",
    customerApproved: "Customer-Approved",
    customerDisapproved: "Customer-Disapproved",
    deliveryDone: "Delivery-Done"
};
var returnStatus = {
    stockReturn: "Stock-Return",
    stockClosed: "Stock-Closed"
};

// Execuation Status ...
var execuStatus = {
    successSave: 'Data has been save successfully',
    successEdit: 'Data has been updated successfully',
    successDelete: 'Data has been deleted successfully',
    fail: 'Something went wrong',
    reqStatusSave: "Requisition status has been changed successfully",
    reqStatusFail: "Requisition status has been failed to change"
};

var stockReturnFlag = {
    assemblyLine: "AssemblyLine",
    assemblyRepair: "Assembly-Repair",
    assemblyRepairFaulty: "Assembly-Repair-Faulty",
    packagingLine: "PackagingLine",
    packagingRepair: "Packaging-Repair",
    packagingRepairFaulty: "Packaging-Repair-Faulty",
};

//e
//var eee = {
//    qtyunit = "Quantity Unit not possible same";
//};

function toggleAlert(msg) {
    var al = bootbox.alert(msg);
    al.show();
    setTimeout(function () { al.modal('hide'); }, 1000);
}

function toastrErrorAlert(msg) {
    toastr.error(msg).fadeOut(1000);
}

function toastrSuccessAlert(msg) {
    toastr.success(msg).fadeOut(1000);
}

function toastrWarningAlert(msg) {
    toastr.warning(msg).fadeOut(1000);
}

function toastrInfoAlert(msg) {
    toastr.info(msg).fadeOut(1000);
}




// Loading dropdown using ajax..
// All the Parameters are required accept contextKey
function LoadDropDown(url, type, elementId, contextKey) {
    if (contextKey === undefined && $.trim(contextKey) === '') {
        $.ajax({
            dataType: 'json',
            type: type,
            url: url,
            success: function (result) {
                consoleLog(result);
                $.each(result, function (index, item) {
                    var option = "<option value='" + item.value + "'>" + item.text + "</option>";
                    elementId.append(option);
                });
            },
            error: function (err) {
                consoleLog(err);
            }
        });
    }
    else {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: type,
            url: url,
            data: contextKey,
            success: function (result) {
                consoleLog(result);
                $.each(result, function (index, item) {
                    var option = "<option value='" + item.value + "'>" + item.text + "</option>";
                    elementId.append(option);
                });
            },
            error: function (err) {
                consoleLog(err);
            }
        });
    }
}

function LoadDropDown2(url, type, elementId, contextKey) {
    if (contextKey === undefined && $.trim(contextKey) === '') {
        $.ajax({
            dataType: 'json',
            type: type,
            url: url,
            success: function (result) {
                consoleLog(result);
                $.each(result, function (index, item) {
                    var option = "<option value='" + item.value + "'>" + item.text + "</option>";
                    elementId.append(option);
                });
                elementId.val('7');
                elementId.trigger('change');
            },
            error: function (err) {
                consoleLog(err);
            }
        });
    }
    else {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: type,
            url: url,
            data: contextKey,
            success: function (result) {
                consoleLog(result);
            },
            error: function (err) {
                consoleLog(err);
            }
        });
    }
}

function LoadDropDown3(url, type, elementId, defaultvalue) {

    $.ajax({
        dataType: 'json',
        type: type,
        url: url,
        success: function (result) {
            consoleLog(result);
            $.each(result, function (index, item) {
                var option = "<option value='" + item.value + "'>" + item.text + "</option>";
                elementId.append(option);
            });
            elementId.val(defaultvalue);
        },
        error: function (err) {
            consoleLog(err);
        }
    });

}

function LoadDropDown4(url, type, elementId, contextKey, defaultvalue) {
    if (contextKey === undefined && $.trim(contextKey) === '') {
        $.ajax({
            dataType: 'json',
            type: type,
            url: url,
            success: function (result) {
                consoleLog(result);
                //consoleLog("default value :" + defaultvalue);
                $.each(result, function (index, item) {
                    var option = "<option value='" + item.value + "'>" + item.text + "</option>";
                    elementId.append(option);
                });
                if (defaultvalue !== null && defaultvalue.toString() !== "") {
                    elementId.val(defaultvalue);
                }
               

            },
            error: function (err) {
                consoleLog(err);
            }
        });
    }
    else {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: type,
            url: url,
            data: contextKey,
            success: function (result) {
                consoleLog(result);
                $.each(result, function (index, item) {
                    var option = "<option value='" + item.value + "'>" + item.text + "</option>";
                    elementId.append(option);
                });
                if (defaultvalue !== null && defaultvalue.toString() !== "") {
                    elementId.val(defaultvalue);
                }
                //elementId.val(defaultvalue);
            },
            error: function (err) {
                consoleLog(err);
            }
        });
    }
}

// Checking user existence
// All the Parameters are required 
function IsUserExist(username, id, isEdit) {
    var returnVal;
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '/Common/IsUserExist',
        async: false,
        data: JSON.stringify({ username: username, id: id, isEdit: isEdit }),
        success: function (result) {
            consoleLog(result);
            returnVal = result;
        },
        error: function (err) {
            consoleLog(err);
        }
    });
    return returnVal;
}

// Checking user email existence
// All the Parameters are required 
function IsEmailExist(id, email, isEdit) {
    var returnVal;
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '/Common/IsEmailExist',
        async: false,
        data: JSON.stringify({ id: id, email: email, isEdit: isEdit }),
        success: function (result) {
            consoleLog(result);
            returnVal = result;
        },
        error: function (err) {
            consoleLog(err);
        }
    });
    return returnVal;
}

// Parameterized Ajax Get Request
// All the Parameters are required 
function getReqWithData(dataType, type, url, data) {
    return $.ajax({
        dataType: dataType,
        type: type,
        url: url,
        data: data,
        success: function (result) {
            //consoleLog(result);
        },
        error: function (err) {
            consoleLog(err);
        }
    });
}

// Parameterized Ajax POST Request
// All the Parameters are required 
function postReqWithData(contentType, dataType, type, url, data) {
    return $.ajax({
        contentType: contentType,
        dataType: dataType,
        type: type,
        url: url,
        data: data,
        success: function (result) {
            consoleLog(result);
            //alert(result);
        },
        error: function (err) {
            consoleLog(err);
        }
    });
}

// Parameterized Ajax POST Request
// All the Parameters are required 
function postReqWithToken(contentType, dataType, type, url, data, token) {
    return $.ajax({
        contentType: contentType,
        dataType: dataType,
        type: type,
        url: url,
        headers: token,
        data: data,
        success: function (result) {
            consoleLog(result);
            //alert(result);
        },
        error: function (err) {
            consoleLog(err);
            //alert(result);
        }
    });
}

// when() function for save/edit
// All the Parameters are required
function fnSaveWhen(obj) {
    $.when(obj).then(function (res) {
        consoleLog(res);
        if (res === true) {
            alert(execuStatus.successSave);
            fnReloadPage(true);
        }
        else {
            alert(execuStatus.fail);
        }
    }).fail(function (err) {
        consoleLog(err);
    });
}

// when() function for delete
// All the Parameters are required
function fnDeleteWhen(obj, tblId, rowIndex) {
    $.when(obj).then(function (res) {
        consoleLog(res);
        if (res === true) {
            alert(execuStatus.successDelete);
            removeTableRow(tblId, rowIndex);
        }
        else {
            alert(execuStatus.fail);
        }
    }).fail(function (err) {
        consoleLog(err);
    });
}

// Reloading the current page
// All the Parameters are required [Boolean Parameter]
function fnReloadPage(isReload) {
    if (isReload) {
        location.reload();
    }
}

// Removing a row from the table with id
// All the Parameters are required 
function removeTableRow(tblId, rowIndex) {
    var tbl = tblId + ' tr';
    $(tbl).eq(rowIndex).css("background-color", "#ff6347").fadeOut(800,
        function () {
            $(tbl).eq(rowIndex).remove();
        });
}

// token
function getToken() {
    var headers = {};
    headers['__RequestVerificationToken'] = $('input[name=__RequestVerificationToken]').val();
    return headers;
}

// Reload the entrire child ui
function UIReloading(url) {
    $.ajax({
        url: url,
        success: function (data) {
            $("div#renderBody").empty();
            //var myNode = document.getElementById("renderBody");
            //while (myNode.firstChild) {
            //    myNode.removeChild(myNode.firstChild);
            //}
            //consoleLog(data);
            $("div#renderBody").html(data);
        }
    });
}

// Modal Black Screen Disappearing From UI
function modalBackdropRemove() {
    $('.modal-backdrop').remove();
}


function ajaxBooleanChecker(data, url, token) {
    var returnVal = false;
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: url,
        async: false,
        data: data,
        headers: token,
        success: function (result) {
            consoleLog(result);
            returnVal = result;
        },
        error: function (err) {
            consoleLog(err);
        }
    });
    return returnVal;
}

function ajaxBooleanChecker2(data, url) {
    var returnVal = false;
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: url,
        async: false,
        data: data,
        success: function (result) {
            consoleLog(result);
            returnVal = result;
        },
        error: function (err) {
            consoleLog(err);
        }
    });
    return returnVal;
}

function ajaxValueReturnable(data, url, token) {
    var returnVal;
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: url,
        async: false,
        data: data,
        headers: token,
        success: function (result) {
            consoleLog(result);
            returnVal = result;
        },
        error: function (err) {
            consoleLog(err);
        }
    });
    return returnVal;
}

// Changing Modal Heading And Button Text
function fnModalHeadChange(HeadText) {
    if ($.trim(HeadText) !== '') {
        $("#modalHeading").empty();
        $("#modalHeading").text(HeadText);
        //consoleLog($("div.modal-header:contains('Update')"));
        var btnText = HeadText.indexOf("Update") > -1 ? 'Update' : 'Save';
        //consoleLog(btnText);
        $("#btnSubmit").empty();
        $("#btnSubmit").text(btnText);
    }
}

// Try ParseInt
function TryParseInt(str, defaultValue) {
    var retValue = defaultValue;
    if (str !== null) {
        if (str.length > 0) {
            if (!isNaN(str)) {
                retValue = parseInt(str);
            }
        }
    }
    return retValue;
}

// Try ParseFloat
function TryParseFloat(str, defaultValue) {
    var retValue = defaultValue;
    if (str !== null) {
        if (str.length > 0) {
            if (!isNaN(str)) {
                retValue = parseFloat(str);
            }
        }
    }
    return retValue;
}

// The month name with format [MM,MMM]
function getMonthName(monthNo, format) {
    var monthName = '';
    if (format === 'MM') {
        switch (monthNo) {
            case 1:
                monthName = 'Jan';
                break;
            case 2:
                monthName = 'Feb';
                break;
            case 3:
                monthName = 'Mar';
                break;
            case 4:
                monthName = 'Apr';
                break;
            case 5:
                monthName = 'May';
                break;
            case 6:
                monthName = 'Jun';
                break;
            case 7:
                monthName = 'Jul';
                break;
            case 8:
                monthName = 'Aug';
                break;
            case 9:
                monthName = 'Sep';
                break;
            case 10:
                monthName = 'Oct';
                break;
            case 11:
                monthName = 'Nov';
                break;
            case 12:
                monthName = 'Dec';
                break;
            default:
                monthName = '';
                break;
        }
    }
    else if (format === 'MMM') {
        switch (monthNo) {
            case 1:
                monthName = 'January';
                break;
            case 2:
                monthName = 'February';
                break;
            case 3:
                monthName = 'March';
                break;
            case 4:
                monthName = 'April';
                break;
            case 5:
                monthName = 'May';
                break;
            case 6:
                monthName = 'June';
                break;
            case 7:
                monthName = 'July';
                break;
            case 8:
                monthName = 'Augest';
                break;
            case 9:
                monthName = 'September';
                break;
            case 10:
                monthName = 'October';
                break;
            case 11:
                monthName = 'November';
                break;
            case 12:
                monthName = 'December';
                break;
            default:
                monthName = '';
                break;
        }
    }
    return monthName;
}

// Count the tbody trs of a table
function fntableRowCount(tableId, countElementId) {
    var rowCount = $("#" + tableId + " tbody tr").length;
    consoleLog("rowcount: " + rowCount);
    if (rowCount === 1) {
        if (!($("#" + tableId + " tbody tr:first-child").hasClass("empty-row"))) {
            $("#" + countElementId).text(rowCount);
        }
        else {
            $("#" + countElementId).text('0');
        }
    }
    else {
        $("#" + countElementId).text(rowCount);
    }
}

// Remove a row from the table body with the table id and tbody tr row index.
function fnRemoveARowFromTbody(tableId, rowIndex) {
    if ($(tableId + " tbody tr").length > 0) {

        $(tableId + " tbody tr").eq(rowIndex).css("background-color", "#ff6347").fadeOut(500,
            function () {
                $(tableId + " tbody tr").eq(rowIndex).remove();
            });
    }
}

// Fix serial number of table tbody trs
function fnFixTheTbodyRowSerial(tableId, rowIndex) {
    $.each($(tableId + " tbody tr"), function (index, item) {
        if (rowIndex !== index) {
            var td = $(this).find('td:eq(0)');
            var sumSL = (index > rowIndex) ? 0 : 1;
            $(this).find('td:eq(0)').text(index + sumSL);
        }
    });
}

function fnFixTheTbodyRowSerialInDecsOrder(tableId, rowIndex) {
    var tableLen = $(tableId + " tbody tr").length;
    if (tableLen !== 0) {
        var count = 1;
        $.each($(tableId + " tbody tr"), function (index, item) {
            if (rowIndex !== index) {
                $(this).find('td:eq(0)').text(tableLen - count);
                count++;
            }
        });
    }
}

// Search By Anything on the table
function fnSearchByAnything(searchElementId, tableId) {
    var value = $(searchElementId).val().toLowerCase();
    $("#" + tableId + " tbody tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
    });
}

// Disable Element
function disable(elementId) {
    $(elementId).attr("disabled", true);
}
// Enable Element
function enable(elementId) {
    $(elementId).attr("disabled", false);
}

function message(element, modalId) {
    if (element !== null) {
        $(element).show(200).fadeIn('slow', function () {
            $(element).delay(500).fadeOut('slow', function () {
                if (modalId !== "" && modalId !== undefined) {
                    hideModal(modalId, reloadPage());
                }
            });
        });
    }
    else {
        if (modalId !== "" && modalId !== undefined) {
            hideModal(modalId, reloadPage());
        }
    }
}

// modal hide
function hideModal(elementId, callback) {
    setTimeout(function () {
        $(elementId).modal('toggle');
    }, 300);
    callback;
}

function reloadPage() {
    location.reload();
}

// client side table filtering
function tableFilter(tableId, senderObj) {
    //consoleLog('KeyUp');
    var value = $(senderObj).val().toLowerCase();
    $(tableId).filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
    });
}

// Convert EN Numeric To BN Numeric
function fnConvertEnNumToBnNum(input) {
    var numbers = {
        '0': '০',
        '1': '১',
        '2': '২',
        '3': '৩',
        '4': '৪',
        '5': '৫',
        '6': '৬',
        '7': '৭',
        '8': '৮',
        '9': '৯',
        '.': '.'
    };
    var output = [];
    for (var i = 0; i < input.length; ++i) {
        if (numbers.hasOwnProperty(input[i])) {
            output.push(numbers[input[i]]);
        } else {
            output.push(input[i]);
        }
    }
    return output.join('');
}

// 
function getDateFromJson(jsonVal) {
    var StartDateServer = jsonVal;
    var parsedDate = new Date(parseInt(StartDateServer.substr(6)));
    var day = ("0" + parsedDate.getDate()).slice(-2);
    var month = ("0" + (parsedDate.getMonth() + 1)).slice(-2);
    var month2 = parsedDate.toLocaleString('default', { month: 'long' });
     //return parsedDate.getFullYear() + "-" + month + "-" + day;
    return day + "-" + month2 + "-" + parsedDate.getFullYear();
}

function clearDropdown(eletementId) {
    $('#' + eletementId + ' option:not(:first)').remove();
}

function ajaxValueChecker(data, url, token) {
    var returnVal = false;
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: url,
        async: false,
        data: data,
        headers: token,
        success: function (result) {
            consoleLog(result);
            returnVal = result;
        },
        error: function (err) {
            consoleLog(err);
        }
    });
    return returnVal;
}

function ajaxValueChecker2(data, url, token) {
    var returnVal = {};
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: url,
        async: false,
        data: data,
        headers: token,
        success: function (result) {
            consoleLog(result);
            returnVal = result;
        },
        error: function (err) {
            consoleLog(err);
        }
    });
    return returnVal;
}


function consoleLog(log) {
    console.log(log);
}

function alerting(alt) {
    alert(alt);
}

function readImg(file, elementId) {
    consoleLog(file.id);
    if (file.files && file.files[0] && file.files[0].name !== '') {
        var reader = new FileReader();
        reader.onload = function (e) {
            elementId.attr("src", e.target.result);
        };
        reader.readAsDataURL(file.files[0]);
    }
    else {
        elementId.attr("src", "/Images/no_image.png");
    }
}


function postReqWithFile(dataType, type, url, data, token) {
    return $.ajax({
        dataType: dataType,
        method: type,
        url: url,
        data: data,
        processData: false,
        contentType: false,
        headers: token,
        success: function (result) {
            consoleLog(result);
        },
        error: function (result) {
            consoleLog(result);
        }
    });
}

function dropDownSelectedText(idName) {
    return $("#" + idName + " option:selected").text();
}

function getTblCells(row) {
   return $(row).parent().parents('tr').children('td');
}
function clearDropdown2(eletementId) {
    $('#' + eletementId + ' option').remove();
}

