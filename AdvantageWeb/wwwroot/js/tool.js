$(() => {
    $('#Columns').attr('hidden', 'hidden');
    $('input[type=datetime-local]').attr('type', 'date').val((new Date()).toISOString().substring(0, 10));
    $('label[for=Columns]').css('display', 'none');
    $('#OrderStatus').attr('placeholder', 'A for all. O for open').attr('required', '');
    $('#OrderNumbers').attr('placeholder', 'Comma separated list');
    $('#IncludeTV').after('<p>⬆ At least one meida option must be selected</p>');
    $('input[type=radio][data-name=fieldstoggle]').change(function () {
        if (this.value == 'fieldsall') {
            $('#columnSelectsDiv').css('display', 'none');
            $('#fieldSearch').css('display', 'none');
            $('.columninput').prop('checked', false);
            $('#Columns_input').val(null);
        }
        else if (this.value == 'fieldsselect') {
            $('#columnSelectsDiv').css('display', 'flex');
            $('#fieldSearch').css('display', 'block');
        }
    });
    $('#fieldSearch').on('input', function (e) {
        if (e.target.value) {
            $('.columndiv').not('[id*=' + e.target.value.toLowerCase() + ']').css('display', 'none');
        }
        else {
            $('.columndiv').css('display', '');
        }

    });
    $('.columninput').prop('checked', false);
    $('.columninput').on('change', () => {
        let selected = [];
        $('.columninput:checked').each(function () {
            selected.push($(this).attr('value'));
        });
        console.log(selected);
        $('#Columns_input').val(selected.join());
    });
    //$('#mo').append($('#inps input'))
});