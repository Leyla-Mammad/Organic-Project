$(document).ready(() => {

    $('#Loadmore').click(() => {
        $.ajax({
            method: "GET",
            url: "/shop/loadmore",
            success: (result) => {
               

                $("#productcomponent").append(result)
                }
               

            })
        })
    })
