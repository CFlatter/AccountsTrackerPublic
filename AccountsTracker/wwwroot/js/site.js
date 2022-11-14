$(".showEditPersonForm").click(function () {
    var data = { personId: $(this).data('id') };
        $.ajax({
            url: '/Person/AddEditPerson',
            type: 'POST',
            data: data,
            success: function (result) {                
                $('#addEditPersonForm').show();                
                $('#addEditPersonForm').html(result);
                $('#closeEditPersonForm').click(function () {
                    $('#addEditPersonForm').hide();
                });
            },
            error: function (xhr, status, error) {
                var err = xhr.responseText;
            }
        });
});

$(".showEditOutgoingForm").click(function () {
    var data = {
        personId: $('#addEditOutgoingsForm').data('personid'),
        id: $(this).data('id')
    };
    $.ajax({
        url: '/PersonalOutgoings/AddEditPersonalOutgoing',
        type: 'POST',
        data: data,
        success: function (result) {
            $('#addEditOutgoingsForm').show();
            $('#addEditOutgoingsForm').html(result);
            $('#closeEditOutgoingForm').click(function () {
                $('#addEditOutgoingsForm').hide();
            });
        },
        error: function (xhr, status, error) {
            var err = xhr.responseText;
        }
    });
});

