function myFunctionPriview() {

    var PublishDate = document.getElementById("PublishDate");
    document.getElementById("PPublishDate").value = PublishDate.value;

    var EndDate = document.getElementById("EndDate");
    document.getElementById("PEndDate").value = EndDate.value;

    var ContentE = document.getElementById("ContentE");
    document.getElementById("PContentE").value = ContentE.value;

    var ContentM = document.getElementById("ContentM");
    document.getElementById("PContentM").value = ContentM.value;

    var TitleE = document.getElementById("TitleE");
    document.getElementById("PTitleE").value = TitleE.value;

    var TitleM = document.getElementById("TitleM");
    document.getElementById("PTitleM").value = TitleM.value;


    var valuechecked = $("input:radio[name=Layer]:checked").val();
    document.getElementById("PLayer").value = valuechecked;
    }
    