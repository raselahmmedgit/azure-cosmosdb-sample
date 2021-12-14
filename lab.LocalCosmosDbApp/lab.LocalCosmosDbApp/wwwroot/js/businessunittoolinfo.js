
var dataTableBusinessUnitToolInfo;

var BusinessUnitToolInfo = function () {

    var htmlTemplateAction = function (data) {
        if (data) {
            var html = "<button data-id='" + data + "' title='Details' class='btn btn-sm btn-primary ml-2 btnDetailBusinessUnitToolInfo'>Details</button>";
            html += "<button data-id='" + data + "' title='Edit' class='btn btn-sm btn-secondary ml-2 btnEditBusinessUnitToolInfo'>Edit</button>";
            html += "<button data-id='" + data + "' title='Delete' class='btn btn-sm btn-danger ml-2 btnDeleteBusinessUnitToolInfo'>Delete</button>";
            return html;
        }
    };

    var htmlTemplateData = function (row) {
        if (row) {
            var html = "<div class='row dataTablesCard'>";
            if (row.bu === null) {
                row.bu = "";
            }
            html += "<div class='col-6'>";

            html += "<div class='row mb-1'>";
            html += "<div class='col-4'><strong>BU: </strong></div>";
            html += "<div class='col-8'>" + row.bu + "</div>";
            html += "</div>";

            html += "</div>";

            html += "<div class='col-6'>";
            
            html += "<div class='row mb-1'>";
            html += "<div class='col-4'><strong>Tool: </strong></div>";
            html += "<div class='col-8'>" + row.tool + "</div>";
            html += "</div>";

            html += "</div>";

            html += "</div>";

            return html;
        }
    };

    var loadDataTables = function (dataTableId, iDisplayLength, sAjaxSourceUrl) {

        //$.fn.dataTable.ext.errMode = () => alert('We are facing some problem while processing the current request. Please try again later.');

        dataTableBusinessUnitToolInfo = $('#' + dataTableId).DataTable({
            "bJQueryUI": true,
            "bAutoWidth": true,
            "sPaginationType": "full_numbers",
            "bPaginate": true,
            "iDisplayLength": iDisplayLength,
            "bSort": false,
            "bFilter": true,
            "bSortClasses": false,
            "lengthChange": false,
            "oLanguage": {
                "sLengthMenu": "Display _MENU_ records per page",
                "sZeroRecords": "Data not found.",
                "sInfo": "Page _START_ to _END_ (Total _TOTAL_)",
                "sInfoEmpty": "Page 0 to 0 (Total 0)",
                "sInfoFiltered": "",
                "sProcessing": "<div class='row'><div class='col-12 dataTables_processing'>Loading...</div></div>"
            },
            "bProcessing": true,
            "bServerSide": true,
            "initComplete": function (settings, json) {
                App.SetDataTableSearch(dataTableId);
            },
            "drawCallback": function (settings) {
            },
            "scrollX": false,
            ajax: {
                url: sAjaxSourceUrl,
                cache: false,
                type: 'GET',
                beforeSend: function () {
                    App.LoaderShow();
                },
                error: function (jqXHR, ajaxOptions, thrownError) {
                    //alert(thrownError + "\r\n" + jqXHR.statusText + "\r\n" + jqXHR.responseText + "\r\n" + ajaxOptions.responseText);
                    var respText = jqXHR.responseText;
                    var messageText = respText;
                    console.log(messageText);
                    App.ToastrNotifierError(messageText);
                    App.LoaderHide();
                },
                complete: function () {
                    App.LoaderHide();
                }
            },
            /*ajax: sAjaxSourceUrl,*/
            columns: [
                {
                    name: 'Id',
                    data: 'id',
                    title: "Id",
                    sortable: false,
                    searchable: false,
                    visible: false
                },
                {
                    name: 'BusinessUnitToolInfoId',
                    data: 'businessUnitToolInfoId',
                    title: "BusinessUnitToolInfoId",
                    sortable: false,
                    searchable: false,
                    visible: false
                },
                {
                    name: 'BU',
                    data: 'bu',
                    title: "BU",
                    sortable: false,
                    searchable: false
                },
                {
                    name: 'FiscalQuarter',
                    data: 'fiscalQuarter',
                    title: "Fiscal Quarter",
                    sortable: false,
                    searchable: false
                },
                {
                    name: 'ToolIndex',
                    data: 'toolIndex',
                    title: "Tool Index",
                    sortable: false,
                    searchable: false
                },
                {
                    name: 'Tool',
                    data: 'tool',
                    title: "Tool",
                    sortable: false,
                    searchable: false
                }
                ,{
                    name: 'Id',
                    data: "id",
                    title: "Actions",
                    sortable: false,
                    searchable: false,
                    "mRender": function (data, type, row) {
                        return htmlTemplateAction(data);
                    }
                }
            ]

        });

    };

    function reloadDataTable() {
        if (dataTableBusinessUnitToolInfo != null && dataTableBusinessUnitToolInfo != null) {
            dataTableBusinessUnitToolInfo.draw()
        }
    };

    var modalBusinessUnitToolInfoBegin = function () {

        App.LoaderShow();
    }

    var modalBusinessUnitToolInfoComplete = function () {

        App.LoaderHide();

    }

    var modalBusinessUnitToolInfoSuccess = function (response) {

        if (response != undefined || response != null) {

            if (response.success == true) {

                App.ToastrNotifierSuccess(response.message);

                //App.AjaxFormReset('FormBusinessUnitToolInfo');

                reloadDataTable();

                //close bootstrap modal
                $('#modalBusinessUnitToolInfoAddOrEdit').modal('hide');
                $("#modalContainer").html('');
            }
            else {

                App.ToastrNotifierError(response.message);
            }
        }
        //check null

        App.LoaderHide();
    }

    var modalBusinessUnitToolInfoFailure = function () {

        App.ToastrNotifierError(appMessage.Error);
    }

    function addBusinessUnitToolInfoModal() {

        $.ajax({
            type: "GET",
            url: "/BusinessUnitToolInfo/AddAjax",
            beforeSend: function () {
                App.LoaderShow();
            },
            success: function (result) {

                //console.log(response);
                $("#modalContainer").html('');
                $("#modalContainer").html(result);
                $('#modalBusinessUnitToolInfoDetail').modal('hide');
                $('#modalBusinessUnitToolInfoAddOrEdit .modal-title').html('BusinessUnitToolInfo Add');
                $('#modalBusinessUnitToolInfoAddOrEdit').modal('show');

                App.LoaderHide();
            },
            error: function (objAjaxRequest, strError) {
                var respText = objAjaxRequest.responseText;
                var messageText = respText;
                console.log(messageText);
                App.ToastrNotifierError(messageText);
                App.LoaderHide();
            },
            complete: function () {
                App.LoaderHide();
            }
        });

    };

    function editBusinessUnitToolInfoModal(id) {

        var id = id != null ? id : 0;
        var dataParam = {
            id: id
        };

        $.ajax({
            type: "GET",
            url: "/BusinessUnitToolInfo/EditAjax",
            data: dataParam,
            beforeSend: function () {
                App.LoaderShow();
            },
            success: function (result) {

                //console.log(response);
                $("#modalContainer").html('');
                $("#modalContainer").html(result);
                $('#modalBusinessUnitToolInfoDetail').modal('hide');
                $('#modalBusinessUnitToolInfoAddOrEdit .modal-title').html('BusinessUnitToolInfo Edit');
                $('#modalBusinessUnitToolInfoAddOrEdit').modal('show');

                App.LoaderHide();
            },
            error: function (objAjaxRequest, strError) {
                var respText = objAjaxRequest.responseText;
                var messageText = respText;
                console.log(messageText);
                App.ToastrNotifierError(messageText);
                App.LoaderHide();
            },
            complete: function () {
                App.LoaderHide();
            }
        });

    };

    function detailBusinessUnitToolInfoModal(id) {

        var id = id != null ? id : 0;
        var dataParam = {
            id: id
        };

        $.ajax({
            type: "GET",
            url: "/BusinessUnitToolInfo/DetailsAjax",
            data: dataParam,
            beforeSend: function () {
                App.LoaderShow();
            },
            success: function (result) {

                //console.log(response);
                $("#modalContainer").html('');
                $("#modalContainer").html(result);
                $('#modalBusinessUnitToolInfoAddOrEdit').modal('hide');
                $('#modalBusinessUnitToolInfoDetail').modal('show');

                App.LoaderHide();
            },
            error: function (objAjaxRequest, strError) {
                var respText = objAjaxRequest.responseText;
                var messageText = respText;
                console.log(messageText);
                App.ToastrNotifierError(messageText);
                App.LoaderHide();
            },
            complete: function () {
                App.LoaderHide();
            }
        });

    };

    var initBusinessUnitToolInfo = function () {

        $('body').on('click', '#btnAddBusinessUnitToolInfo', function () {
            //Open modal dialog for add
            addBusinessUnitToolInfoModal();

            return false;
        });

        $('body').on('click', '.btnEditBusinessUnitToolInfo', function () {
            var id = $(this).attr("data-id");
            //Open modal dialog for edit
            editBusinessUnitToolInfoModal(id);

            return false;
        });

        $('body').on('click', '.btnDetailBusinessUnitToolInfo', function () {
            var id = $(this).attr("data-id");
            //Open modal dialog for details
            detailBusinessUnitToolInfoModal(id);

            return false;
        });

        $('body').on('click', '.btnDeleteBusinessUnitToolInfo', function () {
            var id = $(this).attr("data-id");
            if (id != null && confirm('Are you sure you want to delete this item?')) {
                $.ajax({
                    type: "POST",
                    url: '/BusinessUnitToolInfo/DeleteAjax',
                    data: { 'id': id },
                    success: function (data) {
                        if (data.status) {
                            //Refresh DataTable
                            reloadDataTable();
                            $("#modalContainer").html('');
                            $('#modalBusinessUnitToolInfoAddOrEdit').modal('hide');
                            $('#modalBusinessUnitToolInfoDetail').modal('hide');
                        }
                    },
                    error: function () {
                        alert('Failed');
                    }
                })
            }
        });

    };

    return {
        LoadDataTables: loadDataTables,
        ModalBusinessUnitToolInfoBegin: modalBusinessUnitToolInfoBegin,
        ModalBusinessUnitToolInfoComplete: modalBusinessUnitToolInfoComplete,
        ModalBusinessUnitToolInfoSuccess: modalBusinessUnitToolInfoSuccess,
        ModalBusinessUnitToolInfoFailure: modalBusinessUnitToolInfoFailure,
        InitBusinessUnitToolInfo: initBusinessUnitToolInfo
    };
}();