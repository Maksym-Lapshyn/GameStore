$(document).ready(function () {
	$('.reply').on('click', function () {
		var parentName = $(this).data('name');
		var replyContent = "<p><b>Reply to:<b/>" + parentName + "</p>";
		var reply = $('#replyTo');
		reply.empty();
		reply.append(replyContent);
		var parentId = $(this).data('id');
		$('#ParentCommentId').remove();
		var hiddenInput = $("<input>").attr({
			type: 'hidden',
			id: 'ParentCommentId',
			name: 'ParentCommentId'
		});
		hiddenInput.val(parentId);
		hiddenInput.appendTo('#CommentForm');
	});
});

function reloadPage() {
	if (!$('.field-validation-error').length) {
		location.reload();
	}
}