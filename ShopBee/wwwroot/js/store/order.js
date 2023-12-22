
$(document).on('ready', function () {
    // ONLY DEV
    // =======================================================

    if (window.localStorage.getItem('hs-builder-popover') === null) {
    $('#builderPopover').popover('show')
        .on('shown.bs.popover', function () {
            $('.popover').last().addClass('popover-dark')
        });

$(document).on('click', '#closeBuilderPopover', function () {
    window.localStorage.setItem('hs-builder-popover', true);
$('#builderPopover').popover('dispose');
        });
    } else {
    $('#builderPopover').on('show.bs.popover', function () {
        return false
    });
    }

// END ONLY DEV
// =======================================================


// BUILDER TOGGLE INVOKER
// =======================================================
$('.js-navbar-vertical-aside-toggle-invoker').click(function () {
    $('.js-navbar-vertical-aside-toggle-invoker i').tooltip('hide');
    });


// INITIALIZATION OF MEGA MENU
// =======================================================
var megaMenu = new HSMegaMenu($('.js-mega-menu'), {
    desktop: {
    position: 'left'
        }
    }).init();



// INITIALIZATION OF NAVBAR VERTICAL NAVIGATION
// =======================================================
var sidebar = $('.js-navbar-vertical-aside').hsSideNav();


// INITIALIZATION OF TOOLTIP IN NAVBAR VERTICAL MENU
// =======================================================
$('.js-nav-tooltip-link').tooltip({boundary: 'window' })

$(".js-nav-tooltip-link").on("show.bs.tooltip", function (e) {
        if (!$("body").hasClass("navbar-vertical-aside-mini-mode")) {
            return false;
        }
    });


// INITIALIZATION OF UNFOLD
// =======================================================
$('.js-hs-unfold-invoker').each(function () {
        var unfold = new HSUnfold($(this)).init();
    });


// INITIALIZATION OF FORM SEARCH
// =======================================================
$('.js-form-search').each(function () {
    new HSFormSearch($(this)).init()
});


// INITIALIZATION OF NAV SCROLLER
// =======================================================
$('.js-nav-scroller').each(function () {
    new HsNavScroller($(this)).init()
});


// INITIALIZATION OF SELECT2
// =======================================================
$('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init($(this));
    });


// INITIALIZATION OF DATATABLES
// =======================================================
var datatable = $.HSCore.components.HSDatatables.init($('#datatable'), {
    dom: 'Bfrtip',
buttons: [
{
    extend: 'copy',
className: 'd-none'
            },
{
    extend: 'excel',
className: 'd-none'
            },
{
    extend: 'csv',
className: 'd-none'
            },
{
    extend: 'pdf',
className: 'd-none'
            },
{
    extend: 'print',
className: 'd-none'
            },
],
select: {
    style: 'multi',
selector: 'td:first-child input[type="checkbox"]',
classMap: {
    checkAll: '#datatableCheckAll',
counter: '#datatableCounter',
counterInfo: '#datatableCounterInfo'
            }
        },
language: {
    zeroRecords: '<div class="text-center p-4">' +
        '<img class="mb-3" src="./assets/svg/illustrations/sorry.svg" alt="Image Description" style="width: 7rem;">' +
            '<p class="mb-0">No data to show</p>' +
        '</div>'
        }
    });

$('#export-copy').click(function () {
    datatable.button('.buttons-copy').trigger()
});

$('#export-excel').click(function () {
    datatable.button('.buttons-excel').trigger()
});

$('#export-csv').click(function () {
    datatable.button('.buttons-csv').trigger()
});

$('#export-pdf').click(function () {
    datatable.button('.buttons-pdf').trigger()
});

$('#export-print').click(function () {
    datatable.button('.buttons-print').trigger()
});

$('#datatableSearch').on('mouseup', function (e) {
        var $input = $(this),
oldValue = $input.val();

if (oldValue == "") return;

setTimeout(function () {
            var newValue = $input.val();

if (newValue == "") {
    // Gotcha
    datatable.search('').draw();
            }
        }, 1);
    });

$('#toggleColumn_order').change(function (e) {
    datatable.columns(1).visible(e.target.checked)
})

$('#toggleColumn_date').change(function (e) {
    datatable.columns(2).visible(e.target.checked)
})

$('#toggleColumn_customer').change(function (e) {
    datatable.columns(3).visible(e.target.checked)
})

$('#toggleColumn_payment_status').change(function (e) {
    datatable.columns(4).visible(e.target.checked)
})

datatable.columns(5).visible(false)

$('#toggleColumn_fulfillment_status').change(function (e) {
    datatable.columns(5).visible(e.target.checked)
})

$('#toggleColumn_payment_method').change(function (e) {
    datatable.columns(6).visible(e.target.checked)
})

$('#toggleColumn_total').change(function (e) {
    datatable.columns(7).visible(e.target.checked)
})

$('#toggleColumn_actions').change(function (e) {
    datatable.columns(8).visible(e.target.checked)
})


// INITIALIZATION OF TAGIFY
// =======================================================
$('.js-tagify').each(function () {
        var tagify = $.HSCore.components.HSTagify.init($(this));
    });
});
