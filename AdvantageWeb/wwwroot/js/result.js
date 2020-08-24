$(async () => {
    var result;
    var checker;
    var count = 0;
    let checkStatus = async () => {
        count++;
        result = await $.ajax({
            type: "GET",
            url: 'CheckStatus',
            data: {
                id: window.location.search.split('=')[1]
            }
        });
        if (result == 'True') {
            clearInterval(checker);
            $('#msg').html('Complete').css('font-size', '2rem');
            $('#link').html('Download').attr('href', '/fileResults/' + window.location.search.split('=')[1]);
            $('#status').html("");
        } else {
            $('#status').html(result);
        }
        if (count == 180) {
            alert("Request is taking a long time. Consider running the request again");
        }
        if (count == 300) {
            alert("Request is taking a very long time. Consider running the request again");
        }
    }
    checker = setInterval(checkStatus, 2500)
});