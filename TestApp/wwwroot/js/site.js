// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $("#BtnText1").click(function (e) {
        e.preventDefault();

        var t = $("#Text1").val();
        if (t.length > 0) {
            try {
                var a = t.replace(',', '.').split(' ').map(item => parseInt(item, 10)).filter(Boolean);
                $("#Text1").val(a.join(' '));

                $.ajax({
                    url: "/Home/Sumarray",
                    dataType: "json",
                    method: "POST",
                    data: { "arr": a },
                    success: function (data) {
                        if (data != null) {
                            if (data.code == -1)
                                $("#ResultText1").text(data.errormessage)
                            else
                                $("#ResultText1").text('Сумма = ' + data.result);
                        }
                    }
                });
            }
            catch{
                $("#ResultText1").text("Ошибка конвертации массива");
            }
        }
        else
            $("#ResultText1").text("Введите массив");
    });

    $("#BtnText2").click(function (e) {
        e.preventDefault();

        var t = $("#Text2").val();
        if (t.length > 0) {
            try {
                var a = t.replace(',', '.').split(' ').map(item => parseInt(item, 10)).filter(Boolean);
                if (a.length >= 2) {
                    a.length = 2;
                    $("#Text2").val(a.join(' '));

                    $.ajax({
                        url: "/Home/Sumlists",
                        dataType: "json",
                        method: "POST",
                        data: { "a": a[0], "b": a[1] },
                        success: function (data) {
                            if (data != null) {
                                if (data.code == -1)
                                    $("#ResultText2").text(data.errormessage)
                                else
                                    $("#ResultText2").text('Сумма = ' + data.result.reverse().join(''));
                            }
                        }
                    });
                }
                else
                    $("#ResultText2").text("Введите числа");
            }
            catch{
                $("#ResultText2").text("Ошибка конвертации");
            }
        }
        else
            $("#ResultText2").text("Введите числа");
    });

    $("#BtnText3").click(function (e) {
        e.preventDefault();

        $.ajax({
            url: "/Home/Pal",
            dataType: "json",
            method: "POST",
            data: { "text": $("#Text3").val() },
            success: function (data) {
                if (data != null) {
                    if (data.code == -1)
                        $("#ResultText3").text(data.errormessage)
                    else
                        $("#ResultText3").text(data.result);
                }
            }
        });
    });
});


