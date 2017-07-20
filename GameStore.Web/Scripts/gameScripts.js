$(document).ready(function() {
	$(".changePage").on("click",
		function() {
			$("#CurrentPage").val($(this).data("page"));
			submitForm();
		});

	$("#applyFilter").on("click",
		function() {
			changeFilter();
			submitForm();
		});

	function submitForm() {
		$("#gameForm").submit();
	}

	function changeFilter() {
		$("#FilterIsChanged").val("true");
	}
});