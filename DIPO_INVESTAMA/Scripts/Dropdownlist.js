function DisableDropDown(dropDownId) {
    $(dropDownId).attr("disabled", "disabled");
    $(dropDownId).empty().append('<option selected="selected" value="0">Please select</option>');
}

function PopulateDropDown(dropDownId, list) {
    if (list != null && list.length > 0) {
        $(dropDownId).removeAttr("disabled");
        $.each(list, function () {
            $(dropDownId).append($("<option></option>").val(this['Value']).html(this['Text']));
        });
    }
}