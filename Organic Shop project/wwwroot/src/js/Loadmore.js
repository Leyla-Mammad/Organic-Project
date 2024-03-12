$(document).ready(() => {

    $('#Loadmore').click(() => {
        $.ajax({
            method: "GET",
            url: "/shop/loadmore",
            success: (result) => {
                console.log(result)

                $("#productcomponent").append(result)
                


            }
        })
    })
})