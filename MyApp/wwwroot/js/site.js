// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$('#smt').click(function (e) {
    $.ajax({
        url: '/api/API/testapi',
        type: 'POST',
        data: JSON.stringify($('#data').val()),
        contentType: "application/json; charset=utf-8",
        dataType: 'text'
    }).done(function (r, t, e) {
        $('.card-footer h2').text(r);
    });
});
