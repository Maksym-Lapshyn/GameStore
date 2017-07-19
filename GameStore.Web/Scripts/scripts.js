function reloadPage() {
	var isValid = "@Html.Raw(Json.Encode(ViewData.ModelState.IsValid))";
	if (isValid === "true") {
		location.reload();
	}
}

function addReply(input) {
	var parentName = $(input).data("name");
	var replyContent = "<p><b>Reply to:<b/>" + parentName + "</p>";
	var reply = $("#replyTo");
	reply.empty();
	reply.append(replyContent);
	var parentId = $(input).attr("id");
	$("#ParentCommentId").remove();
	var hiddenInput = $("<input>").attr({
		type: "hidden",
		id: "ParentCommentId",
		name: "ParentCommentId"
	});

	hiddenInput.val(parentId);
	hiddenInput.appendTo("#form");
}

function previousPage() {
	document.getElementById("CurrentPage").value--;
}

function nextPage() {
	document.getElementById("CurrentPage").value++;
}

function submit() {
	document.getElementById("GameFilter").submit();
}

function changeFilter() {
	document.getElementById("FilterIsChanged").value = true;
}