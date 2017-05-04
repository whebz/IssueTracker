﻿var selectedContactUid;

$(document).ready(function () {
    // Attach handler for the new contact button click
    $('.new-Contact').on('click', function (e) {
        var listView = $("#mainWidget").data("kendoListView");
        if (!listView) {
            window.location.href = baseUrl + '/Contacts';
        } else {
            listView.add();
        }
    });

    // Attach contact search handler
    $('.search-textbox').on('keyup', function (e) {
        var text = $(e.target).val().toLowerCase();
        var listView = $("#mainWidget").data("kendoListView");

        var dataInView = listView.dataSource.view();
        dataInView.forEach(function (item) {
            var row = $('div[data-uid="' + item.uid + '"]');

            if (item.Name.toLowerCase().indexOf(text) > -1) {
                row.show();
            } else {
                row.hide();
            }
        });
    });
});

// Exporting the ListView of contacts using kendo.drawing
function exportPeople() {
    kendo.drawing.drawDOM($(".inner-section .k-listview"))
        .then(function (group) {
            return kendo.drawing.exportPDF(group, {
                paperSize: "auto",
                margin: { left: "1cm", top: "1cm", right: "1cm", bottom: "1cm" }
            });
        })
        .done(function (data) {
            kendo.saveAs({
                dataURI: data,
                fileName: "People.pdf"
            });
        });
}

// Get the number of contacts in each category and the total number of contacts (All)
function getInitialNumberOfItems(listViewData) {
    var numbers = { Favorites: 0, Friends: 0, Work: 0 };
    for (var i = 0; i < listViewData.length; i++) {
        var currentItemCategory = listViewData[i].Category;
        numbers[currentItemCategory] += 1;
    }

    numbers.All = numbers.Favorites + numbers.Friends + numbers.Work;

    return numbers;
}

// ListView event handlers
function onListViewDataBound(e) {
    var listView = e.sender;
    attachButtonHandlers();
}

function onListViewEdit(e) {

}

function onListViewChangeCancel(e) {
    var cancelEditContactUid = e.model.uid;

    setTimeout(function () {
        var currectContactElement = $('.contact-view[uid="' + cancelEditContactUid + '"]');
        e.sender.select(currectContactElement);
    }, 0);
}

function onListViewChangeSave(e) {
    var image = $('img.image-preview');
  

    selectedContactUid = e.model.uid;
}

// Select category handler in the sidebar navigation
function selectCategory(e) {
    var dataItem = this.dataItem(e.node);
    var selectedText = e.sender.dataItem(e.node).value;
    Cookies.set('selected', kendo.stringify(dataItem.index));
    selected = Cookies.get('selected');

    $(".search-textbox").val('');

    var contactsListView = $("#mainWidget").data("kendoListView");

    if (!contactsListView) {
        window.location.href = baseUrl + '/Contacts/Index';
    } else if (selectedText === "All") {
        contactsListView.dataSource.filter({});
    } else {
        contactsListView.dataSource.filter({ field: "Category", operator: "contains", value: selectedText });
    }
}

// Restore previously selected item after ListView DataBound
function restoreSlection(listView) {
    if (selectedContactUid) {
        var currectContactElement = $('.contact-view[uid="' + selectedContactUid + '"]');
        selectedContactUid = null;
        listView.select(currectContactElement);
    }
}

// Attach handlers for each contact buttons
function attachButtonHandlers() {
    $('.k-single-email-button').on('click', singleCreateNewMail);
    $('.k-single-edit-button').on('click', singleEditClick);
    $('.k-single-delete-button').on('click', singleDeleteClick);
}

function singleCreateNewMail(e) {
    var target = $(e.target);
    var singleItem = target.parents('.contact-view');
    var email = singleItem.find('.hidden-email').text();

    $(".main-section").load(baseUrl + '/Home/NewMail?mailTo=' + email);
}

function singleEditClick(e) {
    var uid = getItemUid(e);
    var listView = $("#mainWidget").data("kendoListView");
    listView.edit(listView.element.find('[data-uid="' + uid + '"]'));
    $(".form-title .action").html("Edit");
}

function singleDeleteClick(e) {
    var uid = getItemUid(e);
    var listView = $("#mainWidget").data("kendoListView");
    if (listView.element.find('[data-uid="' + uid + '"]').length != 0) {
        listView.remove(listView.element.find('[data-uid="' + uid + '"]'));
    }
}

function getItemUid(e) {
    var target = $(e.target);
    var singleItem = target.parents('.contact-view');

    return singleItem.attr('uid');
}

// Create preview of the newly selected image
function onImageSelect(e) {
    var fileInfo = e.files[0];

    if (fileInfo.validationErrors && fileInfo.validationErrors.length > 0) {
        return;
    }

    setTimeout(function () {
        addPreview(fileInfo);
    });
}

function addPreview(file) {
    var raw = file.rawFile;
    var reader = new FileReader();

    if (raw) {
        reader.onloadend = function () {
            var image = $('<img class="image-preview">').attr('src', this.result);
            $('.upload-image-wrapper').html(image);
        };

        reader.readAsDataURL(raw);
    }
}
