$('.close-alert').click(() => {
    $('.alert').hide('hide')
});

setTimeout(function () {
    $('.timer').hide('hide');
}, 5000)

//$(function () {
//    $('input[type=datetime]').datepicker({
//        dateFormat: 'dd/mm/yy',
//        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
//        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
//        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
//        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
//        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
//        nextText: 'Proximo',
//        prevText: 'Anterior',
//        showOn: "button"
//    }).css("display", "inline-block")
//        .next("button").button({
//            icons: { primary: "ui-icon-calendar" },
//            label: "Selecione uma data",
//            text: false
//        });
//});    