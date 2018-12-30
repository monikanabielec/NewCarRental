function uploadFile() {
    $('#uploadFileForm').submit();  
}
$("#uploadFileForm").submit(function (e) {
    e.preventDefault();
    var formData = new FormData($(this)[0]);

    $.ajax({
        type: "POST",
        url: "/Cars/Upload",
        data: formData,
        async: false,
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {
            console.log(data);
        }
    });
    return false;
})
function submitCreateCar() {   
    $("form[name='createCarForm']").submit();
}