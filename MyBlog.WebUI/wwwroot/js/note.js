

    var noteid = -1;
    $(function () {
        $('#showNoteDetails').on('show.bs.modal', function (e) {
           
            console.log("deneme");
            var btn = $(e.relatedTarget);
            $('#modal-notedetails-body').load("/Note/GetNoteDetail/" + noteid)
        });
    });


