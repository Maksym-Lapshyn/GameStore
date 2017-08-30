$(document).ready(function () {
	$('.reply').on('click', function () {
		var parentName = $(this).data('name');
		var parentId = $(this).data('id');
		var replyContent = "<p><b>" + parentName + ",<b/></p>";
		var replyDiv = $('#replyTo');
		replyDiv.empty();
		replyDiv.append(replyContent);
		$('#NewComment_ParentCommentId').val(parentId);
		$('#NewComment_ParentCommentName').val(parentName);
	});
});