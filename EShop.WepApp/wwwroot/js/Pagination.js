﻿var numberOfItems = $("#loop .list-group").length;
var limitPerPage = 6;

$(`#loop .list-group:gt(${limitPerPage - 1})`).hide();
var totalPages = Math.round(numberOfItems / limitPerPage);

//inserted first item
$('.pagination').append(`<li class="page-item active"><a class="page-link" href="javascript:void(0)">1</a></li>`);
//loop throught other items
for (var i = 2; i <= totalPages; i++) {
    $('.pagination').append(`<li class="page-item"><a class="page-link" href="javascript:void(0)">${i}</a></li>`);
}
//insert right arrow
$('.pagination').append(`<li id='next-page' class="page-item-arrow"><a class="page-link" href="javascript:void(0)" aria-label="Next">    <span aria-hidden="true">&raquo;</span></a></li>`);

$('.pagination li.page-item').on("click", function () {
    if ($(this).hasClass("active"))
        return false;
    else {
        var currentPage = $(this).index();
        $('.pagination li').removeClass("active");
        $(this).addClass("active")
        $(`#loop .list-group`).hide();
        var grandTotal = limitPerPage * currentPage;
        //lopp.listgroup.append
        for (var i = grandTotal - limitPerPage; i < grandTotal; i++) {
            $(`#loop .list-group:eq(${i})`).show();
        }
    }
});

$('#next-page').on("click", function () {
    var currentPage = $('.pagination li.active').index();
    if (currentPage === totalPages)
        return false;
    else {
        currentPage++;
        $('.pagination li').removeClass("active");
        $(`#loop .list-group`).hide();

        var grandTotal = limitPerPage * currentPage;

        for (var i = grandTotal - limitPerPage; i < grandTotal; i++) {
            $(`#loop .list-group:eq(${i})`).show();
        }

        $(`.pagination li.page-item:eq(${currentPage - 1})`).addClass("active");
    }
});

$('#previous-page').on("click", function () {
    var currentPage = $('.pagination li.active').index();
    if (currentPage === 1)
        return false;
    else {
        currentPage--;
        $('.pagination li').removeClass("active");
        $(`#loop .list-group`).hide();

        var grandTotal = limitPerPage * currentPage;
        //sa servera povlaciti ! 
        for (var i = grandTotal - limitPerPage; i < grandTotal; i++) {
            $(`#loop .list-group:eq(${i})`).show();
        }

        $(`.pagination li.page-item:eq(${currentPage - 1})`).addClass("active");
    }
});