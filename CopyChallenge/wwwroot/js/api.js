function apicall(controller, action, parameter, methode) {
    $.ajax({
        url: '@(Url.Action("Post", "sampleorders"))?values=' + JSON.stringify(datab),
        type: "POST",
        contentType: "application/json",
        success: function (response) {
            // Code to handle the successful response
            
            console.log(response);
            return response
        },
        error: function (xhr, textStatus, errorThrown) {
            // Code to handle the error
            console.error(xhr, textStatus, errorThrown);
        }
    });
}