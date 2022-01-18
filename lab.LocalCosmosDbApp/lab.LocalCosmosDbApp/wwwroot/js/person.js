
var dataTablePersonCardView;

var dataTablePersonListView;

var Person = function () {

    var htmlTemplateAction = function (row) {
        if (row) {
            var html = "";
            if (App.IsDataTableActionRoleMember(row.role)) {
                html += "<button data-id='" + row.id + "' title='Details' class='btn btn-sm btn-green ml-2 btnDetailPerson'>Details</button>";
            }
            if (App.IsDataTableActionRoleAdmin(row.role)) {
                html += "<button data-id='" + row.id + "' title='Edit' class='btn btn-sm btn-dark ml-2 btnEditPerson'>Edit</button>";
                html += "<button data-id='" + row.id + "' title='Delete' class='btn btn-sm btn-danger ml-2 btnDeletePerson'>Delete</button>";
            }
            return html;
        }
    };

    var htmlTemplateData = function (row) {
        if (row) {

            var cardViewStyle = "";
            if (row.isCardView != undefined || row.isCardView != null) {
                if (row.isCardView === 'true') {
                    cardViewStyle = "style=''";
                }
                else {
                    cardViewStyle = "style='display: none;'";
                }
            }

            //Start - DataTables Card View
            var htmlDataTablesCardView = "<div class='row dataTablesCardView'" + cardViewStyle + ">";
            if (row.personName === null) {
                row.personName = "";
            }
            htmlDataTablesCardView += "<div class='col-6 dataTablesCardViewCol'>";

            htmlDataTablesCardView += "<div class='row mb-1'>";
            htmlDataTablesCardView += "<div class='col-4'><strong>Person Name: </strong></div>";
            htmlDataTablesCardView += "<div class='col-8'>" + row.personName + "</div>";
            htmlDataTablesCardView += "</div>";

            htmlDataTablesCardView += "</div>";

            htmlDataTablesCardView += "<div class='col-6 dataTablesCardViewCol'>";
            
            htmlDataTablesCardView += "<div class='row mb-1'>";
            htmlDataTablesCardView += "<div class='col-4'><strong>Email Address: </strong></div>";
            htmlDataTablesCardView += "<div class='col-8'>" + row.emailAddress + "</div>";
            htmlDataTablesCardView += "</div>";

            htmlDataTablesCardView += "</div>";

            htmlDataTablesCardView += "</div>";
            //End - DataTables Card View

            var listViewStyle = "";
            if (row.isListView != undefined || row.isListView != null) {
                if (row.isListView === 'true') {
                    listViewStyle = "style=''";
                }
                else
                {
                    listViewStyle = "style='display: none;'";
                }
            }

            //Start - DataTables List View
            var htmlDataTablesListView = "<div class='row dataTablesListView'" + listViewStyle + ">";
            if (row.personName === null) {
                row.personName = "";
            }
            htmlDataTablesListView += "<div class='col-12 dataTablesListViewCol'>";

            htmlDataTablesListView += "<div class='css-table-body-row'>";
            htmlDataTablesListView += "<div class='css-table-body-cell'>" + row.personName + "</div>";
            htmlDataTablesListView += "<div class='css-table-body-cell'>" + row.emailAddress + "</div>";
            htmlDataTablesListView += "</div>";

            htmlDataTablesListView += "</div>";

            htmlDataTablesListView += "</div>";
            //End - DataTables List View

            html = "";
            html += htmlDataTablesCardView;
            html += htmlDataTablesListView;

            return html;
        }
    };

    var loadDataTablesCardView = function (dataTableId, iDisplayLength, sAjaxSourceUrl) {

        //$.fn.dataTable.ext.errMode = () => alert('We are facing some problem while processing the current request. Please try again later.');

        dataTablePersonCardView = $('#' + dataTableId).DataTable({
            "bJQueryUI": true,
            "bAutoWidth": true,
            "sPaginationType": "full_numbers",
            "bPaginate": true,
            "iDisplayLength": iDisplayLength,
            "bSort": false,
            "bFilter": false,
            "bSortClasses": false,
            "lengthChange": false,
            "stateSave": true,
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
                App.SetDataTableSearch(dataTableId, false, true, false);
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
                }
                ,{
                    name: 'Id',
                    data: "id",
                    title: "",
                    sortable: false,
                    searchable: false,
                    "mRender": function (data, type, row) {
                        return htmlTemplateData(row);
                    }
                }
                ,{
                    name: 'PersonName',
                    data: 'personName',
                    title: "Person Name",
                    sortable: false,
                    searchable: false,
                    visible: false
                }
                ,{
                    name: 'EmailAddress',
                    data: 'emailAddress',
                    title: "Email Address",
                    sortable: false,
                    searchable: false,
                    visible: false
                }
                ,{
                    name: 'Id',
                    data: "id",
                    title: "Actions",
                    sortable: false,
                    searchable: false,
                    className: "w-30 text-center",
                    "mRender": function (data, type, row) {
                        return htmlTemplateAction(row);
                    }
                }
            ]

        });

    };

    var loadDataTablesListView = function (dataTableId, iDisplayLength, sAjaxSourceUrl) {

        //$.fn.dataTable.ext.errMode = () => alert('We are facing some problem while processing the current request. Please try again later.');

        dataTablePersonListView = $('#' + dataTableId).DataTable({
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
                App.SetDataTableSearch(dataTableId, true, false, true);
            },
            "drawCallback": function (settings) {
            },
            "scrollX": true,
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
                }
                ,{
                    name: 'PersonName',
                    data: 'personName',
                    title: "Person Name",
                    sortable: false,
                    searchable: false
                }
                ,{
                    name: 'EmailAddress',
                    data: 'emailAddress',
                    title: "Email Address",
                    sortable: false,
                    searchable: false
                }
                ,{
                    name: 'Id',
                    data: "id",
                    title: "Actions",
                    sortable: false,
                    searchable: false,
                    className: "w-30 text-center",
                    "mRender": function (data, type, row) {
                        return htmlTemplateAction(row);
                    }
                }
            ]

        });

    };

    //var loadDataTablesAction = function (dataTableId) {

    //    var dataTablePersonId = '#' + dataTableId;
    //    $('body').on('click', '.btnDataTablesCardViewPerson', function () {
    //        var dataTablesCardViewClass = dataTablePersonId + ' .dataTablesCardView';
    //        var dataTablesListViewClass = dataTablePersonId + ' .dataTablesListView';
    //        $(dataTablesListViewClass).hide();
    //        $('.btnDataTablesListViewPerson').removeClass('active');
    //        $(dataTablesCardViewClass).show();
    //        $('.btnDataTablesCardViewPerson').addClass('active');

    //        App.LoaderHide();
    //        return false;
    //    });

    //    $('body').on('click', '.btnDataTablesListViewPerson', function () {
    //        var dataTablesCardViewClass = dataTablePersonId + ' .dataTablesCardView';
    //        var dataTablesListViewClass = dataTablePersonId + ' .dataTablesListView';
    //        $(dataTablesCardViewClass).hide();
    //        $('.btnDataTablesCardViewPerson').removeClass('active');
    //        $(dataTablesListViewClass).show();
    //        $('.btnDataTablesListViewPerson').addClass('active');

    //        App.LoaderHide();
    //        return false;
    //    });

    //};

    var loadDataTablesAction = function () {

        $('body').on('click', '.btnDataTablesCardViewPerson', function () {
            hideDataTableColumns();
            $('.btnDataTablesListViewPerson').removeClass('active');
            showDataTableColumns([1, 4]);
            $('.btnDataTablesCardViewPerson').addClass('active');

            App.LoaderHide();
            return false;
        });

        $('body').on('click', '.btnDataTablesListViewPerson', function () {
            hideDataTableColumns();
            $('.btnDataTablesCardViewPerson').removeClass('active');
            showDataTableColumns([2, 3, 4]);
            $('.btnDataTablesListViewPerson').addClass('active');

            App.LoaderHide();
            return false;
        });

    };

    //var loadDataTablesAction = function (dataTableCardViewId, dataTableListViewId) {

    //    var dataTableCardViewPersonWrapperId = '#' + dataTableCardViewId + '_wrapper';
    //    var dataTableListViewPersonWrapperId = '#' + dataTableListViewId + '_wrapper';
        
    //    $('body').on('click', '.btnDataTablesCardViewPerson', function () {
    //        $(dataTableCardViewPersonWrapperId).attr("style", "display: block;");
    //        $(dataTableListViewPersonWrapperId).attr("style", "display: none;");

    //        $('.btnDataTablesListViewPerson').removeClass('active');
    //        $('.btnDataTablesCardViewPerson').addClass('active');

    //        App.LoaderHide();
    //        return false;
    //    });

    //    $('body').on('click', '.btnDataTablesListViewPerson', function () {
    //        $(dataTableCardViewPersonWrapperId).attr("style", "display: none;");
    //        $(dataTableListViewPersonWrapperId).attr("style", "display: block;");

    //        $('.btnDataTablesCardViewPerson').removeClass('active');
    //        $('.btnDataTablesListViewPerson').addClass('active');

    //        App.LoaderHide();
    //        return false;
    //    });

    //};

    function hideDataTableColumns() {
        if (dataTablePersonCardView != null && dataTablePersonCardView != null) {
            const dataTableColumns = [0, 1, 2, 3, 4];
            for (const column of dataTableColumns) {
                console.log(column);
                dataTablePersonCardView.column(column).visible(false);
            }
        }
        if (dataTablePersonListView != null && dataTablePersonListView != null) {
            const dataTableColumns = [0, 1, 2, 3, 4];
            for (const column of dataTableColumns) {
                console.log(column);
                dataTablePersonListView.column(column).visible(false);
            }
        }
    };

    function showDataTableColumns(dataTableColumns) {
        if (dataTablePersonCardView != null && dataTablePersonCardView != null) {
            for (const column of dataTableColumns) {
                console.log(column);
                dataTablePersonCardView.column(column).visible(true);
            }
        }
        if (dataTablePersonListView != null && dataTablePersonListView != null) {
            for (const column of dataTableColumns) {
                console.log(column);
                dataTablePersonListView.column(column).visible(true);
            }
        }
    };

    function reloadDataTable() {
        if (dataTablePersonCardView != null && dataTablePersonCardView != null) {
            //dataTablePersonCardView.draw();
            dataTablePersonCardView.draw('page');
        }
        if (dataTablePersonListView != null && dataTablePersonListView != null) {
            //dataTablePersonListView.draw();
            dataTablePersonListView.draw('page');
        }
    };

    var modalPersonEventBind = function () {
        $('body').on('input propertychange paste', '#Email', function () {
            if ($('#Email').val()) {
                var email = $('#Email').val();
                $('#PersonName').val('');
                $('#PersonName').val(email)
            }
        });
    };

    var modalPersonBegin = function () {
        App.LoaderShow();
    }

    var modalPersonComplete = function () {

        App.LoaderHide();

    }

    var modalPersonSuccess = function (response) {

        if (response != undefined || response != null) {

            if (response.success == true) {

                App.ToastrNotifierSuccess(response.message);

                //App.AjaxFormReset('FormPerson');

                reloadDataTable();

                //close bootstrap modal
                $('#modalPersonAddOrEdit').modal('hide');
                $("#modalContainer").html('');
            }
            else {

                App.ToastrNotifierError(response.message);
            }
        }
        //check null

        App.LoaderHide();
    }

    var modalPersonFailure = function () {

        App.ToastrNotifierError(appMessage.Error);
    }

    function addPersonModal() {

        $.ajax({
            type: "GET",
            url: "/Person/AddAjax",
            beforeSend: function () {
                App.LoaderShow();
            },
            success: function (result) {

                //console.log(response);
                $("#modalContainer").html('');
                $("#modalContainer").html(result);
                $('#modalPersonDetail').modal('hide');
                $('#modalPersonAddOrEdit .modal-title').html('Create Person');
                $('#modalPersonAddOrEdit').modal('show');

                modalPersonEventBind();
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

    function editPersonModal(id) {

        var id = id != null ? id : 0;
        var dataParam = {
            id: id
        };

        $.ajax({
            type: "GET",
            url: "/Person/EditAjax",
            data: dataParam,
            beforeSend: function () {
                App.LoaderShow();
            },
            success: function (result) {

                //console.log(response);
                $("#modalContainer").html('');
                $("#modalContainer").html(result);
                $('#modalPersonDetail').modal('hide');
                $('#modalPersonAddOrEdit .modal-title').html('Edit Person');
                $('#modalPersonAddOrEdit').modal('show');

                modalPersonEventBind();
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

    function detailPersonModal(id) {

        var id = id != null ? id : 0;
        var dataParam = {
            id: id
        };

        $.ajax({
            type: "GET",
            url: "/Person/DetailsAjax",
            data: dataParam,
            beforeSend: function () {
                App.LoaderShow();
            },
            success: function (result) {

                //console.log(response);
                $("#modalContainer").html('');
                $("#modalContainer").html(result);
                $('#modalPersonAddOrEdit').modal('hide');
                $('#modalPersonDetail').modal('show');

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

    var initPerson = function () {

        $('body').on('click', '#btnAddPerson', function () {
            //Open modal dialog for add
            addPersonModal();

            return false;
        });

        $('body').on('click', '.btnEditPerson', function () {
            var id = $(this).attr("data-id");
            //Open modal dialog for edit
            editPersonModal(id);

            return false;
        });

        $('body').on('click', '.btnDetailPerson', function () {
            var id = $(this).attr("data-id");
            //Open modal dialog for details
            detailPersonModal(id);

            return false;
        });

        $('body').on('click', '.btnDeletePerson', function () {
            var id = $(this).attr("data-id");
            if (id != null && confirm('Are you sure you want to delete this item?')) {
                $.ajax({
                    type: "POST",
                    url: '/Person/DeleteAjax',
                    data: { 'id': id },
                    success: function (response) {

                        if (response != undefined || response != null) {

                            if (response.success == true) {

                                App.ToastrNotifierSuccess(response.message);

                                reloadDataTable();

                            }
                            else {

                                App.ToastrNotifierError(response.message);
                            }
                        }
                        //check null

                        App.LoaderHide();

                    },
                    error: function () {
                        alert('Failed');
                    }
                })
            }
        });

    };

    return {
        LoadDataTablesCardView: loadDataTablesCardView,
        LoadDataTablesListView: loadDataTablesListView,
        LoadDataTablesAction: loadDataTablesAction,
        ModalPersonEventBind: modalPersonEventBind,
        ModalPersonBegin: modalPersonBegin,
        ModalPersonComplete: modalPersonComplete,
        ModalPersonSuccess: modalPersonSuccess,
        ModalPersonFailure: modalPersonFailure,
        InitPerson: initPerson
    };
}();