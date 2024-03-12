$(document).ready(function () {
    $(".addBasketBtn").click(function () {
        let id = $(this).data("id");


        $.ajax({
                method: "POST",
                url: "/basket/add",
            data: {
                id:id
            },
            success: () => { console.log("Ok") }
         })
    })


});
