$(document).ready(function () {
	var defaultForm = $("#gameForm").serialize();

	$(".changePage").on("click", function () {
		$("#FilterIsChanged").val("false");
		$("#CurrentPage").val($(this).data("page"));
		submitForm();
	});

	$("#applyFilter").on("click", function () {
		var currentForm = $("#gameForm").serialize();

		if (currentForm !== defaultForm) {
			$("#FilterIsChanged").val("true");
			submitForm();
		}
	});

	function submitForm() {
		$("#gameForm").submit();
	}
});